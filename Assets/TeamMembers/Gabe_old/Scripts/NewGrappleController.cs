using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGrappleController : MonoBehaviour
{
    // [SerializeField]
    public float grappleDistance = 100f;
    // [SerializeField]
    public float swingForce = 15f;
    // [SerializeField]
    public float tension = 0.5f; // Adjust this value to control the tension effect
    
    [SerializeField]
    public LayerMask grappleableLayer;
    private Rigidbody2D rb;

    private Vector2 grapplePoint;
    private Vector2 grappleDir;
    private Vector2 mouseDir;
    private Vector2 swingForceDir;
    
    private bool isGrappling = false;

    private const int toungeInputButton = 0;
    private const int reelInputButton = 1;
    

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
            if (Input.GetMouseButton(reelInputButton))
                Reel();
        }
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
        }
    }

    private void StopGrapple()
    {
        isGrappling = false;
        rb.gravityScale = 1f;
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
        // Calculate the swing force direction based on the relative direction from rigidbody to mouse click position
        Vector2 swingForceDir = grappleDir + (mouseDir - grappleDir) * tension;
        rb.AddForce(swingForceDir * swingForce, ForceMode2D.Force);
    }

    



}

