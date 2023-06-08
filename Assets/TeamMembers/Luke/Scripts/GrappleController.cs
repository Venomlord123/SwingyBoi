using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleController : MonoBehaviour
{
    public float grappleDistance = 10f;
    public float swingForce = 5f;
    public float tension = 0.5f; // Adjust this value to control the tension effect
    public LayerMask grappleableLayer;

    private bool isGrappling = false;
    private Vector2 grapplePoint;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
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

        if (isGrappling && Input.GetMouseButtonUp(0))
        {
            StopGrapple();
        }

        if (isGrappling)
        {
            Swing();
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
        Vector2 grappleDir = (grapplePoint - (Vector2)transform.position).normalized;
        Vector2 mouseDir = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;

        // Calculate the swing force direction based on the relative direction from rigidbody to mouse click position
        Vector2 swingForceDir = grappleDir + (mouseDir - grappleDir) * tension;

        rb.AddForce(swingForceDir * swingForce, ForceMode2D.Force);

        // Draw a debug line to visualize the grapple direction and length
        Debug.DrawLine(transform.position, grapplePoint, Color.red);
    }
}

