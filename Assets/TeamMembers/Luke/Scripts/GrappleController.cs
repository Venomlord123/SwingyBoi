using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleController : MonoBehaviour
{
    // [SerializeField]
    private float grappleDistance = 15f;
    // [SerializeField]
    public float swingForce = 1f;
    // [SerializeField]
    public float tension = 0.1f; // Adjust this value to control the tension effect

    private float jumpCharge = 0f;

    // [SerializeField]
    public LayerMask grappleableLayer;
    private Rigidbody2D rb;

    // public Colour chargeColour;
    // private SpriteRenderer sr;

    private Vector2 grapplePoint;
    private Vector2 grappleDir;
    private Vector2 mouseDir;
    private Vector2 swingForceDir;
    private Vector3 screenSpaceGrapplePoint;   
    private Vector3 lastScreenSpaceMousePoint;   
    

    private bool isGrappling = false;
    private bool jumpIsCharging = false;

    private const int toungeInputButton = 0;
    private const int jumpInputButton = 1;
    

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(toungeInputButton))
        {
            if (!isGrappling)
            {
                StartGrapple();
            }
            else
            {
                StopGrapple();
            }
        }

        if (isGrappling && Input.GetMouseButtonUp(toungeInputButton))
        {
            StopGrapple();
        }

        if (isGrappling)
        {
            Swing();
            Reel();
        }

        if (Input.GetMouseButton(jumpInputButton))
            Jump();

        if (jumpIsCharging && Input.GetMouseButtonUp(jumpInputButton))
            ReleaseJump();

    }

    private void StartGrapple()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, mousePos - (Vector2)transform.position, grappleDistance, grappleableLayer);

        if (hit.collider != null)
        {
            isGrappling = true;
            grapplePoint = hit.point;
            rb.gravityScale = 0f;
            rb.velocity = Vector2.zero;

            //save current mouse screen-space position 
            // Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //copilot did this
            // stick a little cursor where the tounge is attached (for ease of showing vector of mouse displacement relative to tounge grapple point)
            //save the current tounge length - it shouldn't change unless the mouse moves (give or take a little springyness)


        }

        screenSpaceGrapplePoint = Input.mousePosition;
        lastScreenSpaceMousePoint = screenSpaceGrapplePoint;
        Debug.Log($"Grapple initated: {Input.mousePosition}");

    }

    private void StopGrapple()
    {
        isGrappling = false;
        rb.gravityScale = 1f;
        // Debug.Log(Input.mousePosition);
    }

    private void Swing()
    {
        grappleDir = (grapplePoint - (Vector2)transform.position).normalized;
        mouseDir = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;

        // Draw a debug line to visualize the grapple direction and length
        Debug.DrawLine(transform.position, grapplePoint, Color.red);
    }

    private void Reel()
    {
        if (lastScreenSpaceMousePoint != Input.mousePosition) // should drop decimals for rounding, but whatever
            {
                lastScreenSpaceMousePoint = Input.mousePosition;
                Debug.Log($"Mouse position difference vector: {lastScreenSpaceMousePoint - screenSpaceGrapplePoint}");             
            }

        // Debug.DrawLine(screenSpaceGrapplePoint, lastScreenSpaceMousePoint, Color.blue); // will need to use something other than debug.drawline

        Vector2 swingForceDir = (screenSpaceGrapplePoint - lastScreenSpaceMousePoint).normalized;
        rb.AddForce(swingForceDir * swingForce, ForceMode2D.Force);

    }

    private void Jump()
    {
        if (!jumpIsCharging)
        {
            jumpIsCharging = true;
            Debug.Log("Jump charging started");
        }

        if (jumpCharge < 10f)
        {
            jumpCharge += Time.deltaTime;
            Debug.Log($"Jump charge: {jumpCharge}");
        }
        else 
        {
            Debug.Log("Jump charge maxed!");
        }
    }


    private void ReleaseJump()
    {
        mouseDir = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
        jumpIsCharging = false;
        rb.AddForce(mouseDir * jumpCharge * 1000f, ForceMode2D.Force);
        Debug.Log($"Jump Released. Force: {jumpCharge}");
        jumpCharge = 0f;
    }
}