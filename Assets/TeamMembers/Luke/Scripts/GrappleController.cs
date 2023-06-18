using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleController : MonoBehaviour
{
    public float maxGrappleDistance = 15f;
    public float currentGrappleDistance;
    public float swingForce = 1f;
    public float tension = 1f;
    public LayerMask grappleableLayer;

    private float jumpCharge = 0f;
    private Rigidbody2D rb;
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
        HandleInput();
    }

    private void HandleInput()
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

        //this will need to be moved into a new jump script
        if (Input.GetMouseButton(jumpInputButton))
            Jump();

        if (jumpIsCharging && Input.GetMouseButtonUp(jumpInputButton))
            ReleaseJump();
    }

    //This will need to be more of an extension of the tongue hitting the collider rather than an instant point and the tongue should have a collidable rigidbody that the length of the tongue can interact with
    private void StartGrapple()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, mousePos - (Vector2)transform.position, maxGrappleDistance, grappleableLayer);
        
        if (hit.collider != null)
        {
            isGrappling = true;
            grapplePoint = hit.point;
            //rb.gravityScale = 0f;
            //rb.velocity = Vector2.zero;

            //save current mouse screen-space position 
            // Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //copilot did this
            // stick a little cursor where the tounge is attached (for ease of showing vector of mouse displacement relative to tounge grapple point)
            //save the current tounge length - it shouldn't change unless the mouse moves (give or take a little springyness)
        }

        screenSpaceGrapplePoint = Input.mousePosition;
        lastScreenSpaceMousePoint = screenSpaceGrapplePoint;
        //Debug.Log($"Grapple initated: {Input.mousePosition}");

    }

    private void StopGrapple()
    {
        isGrappling = false;
        currentGrappleDistance = 0f;
        //rb.gravityScale = 1f;
        // Debug.Log(Input.mousePosition);
    }

    private void Swing()
    {
        grappleDir = (grapplePoint - (Vector2)transform.position).normalized;
        mouseDir = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
        //Current distance of the grapple
        currentGrappleDistance = transform.position.magnitude - grapplePoint.magnitude;

        //limiting our ridigbody moving past the tongue max grapple distance, will need to change it so the added max length of the tongue adds a reverse force for tension instead of making rb.force zero
        if (currentGrappleDistance >= maxGrappleDistance)
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }

        // Draw a debug line to visualize the grapple direction and length
        Debug.DrawLine(transform.position, grapplePoint, Color.red);
    }

    /// <summary>
    /// I would suggest having this as a reel up the tongue only in the tongue direction and have the jump code separate
    /// </summary>
    private void Reel()
    {
        if (lastScreenSpaceMousePoint != Input.mousePosition) // should drop decimals for rounding, but whatever
            {
                lastScreenSpaceMousePoint = Input.mousePosition;
                //Debug.Log($"Mouse position difference vector: {lastScreenSpaceMousePoint - screenSpaceGrapplePoint}");             
            }

        // Debug.DrawLine(screenSpaceGrapplePoint, lastScreenSpaceMousePoint, Color.blue); // will need to use something other than debug.drawline
        
        if (currentGrappleDistance <= maxGrappleDistance)
        {
            swingForceDir = (screenSpaceGrapplePoint - lastScreenSpaceMousePoint).normalized;
            rb.AddForce(swingForceDir * swingForce, ForceMode2D.Force);
        }
    }

    //apply tension force opposite to grappledirection
    //private void Tension()
    //{
    //    if(currentGrappleDistance > maxGrappleDistance)
    //    {

    //    }
    //}

    /// <summary>
    /// This jump code should be separated into another script
    /// </summary>
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