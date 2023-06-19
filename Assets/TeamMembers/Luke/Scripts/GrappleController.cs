using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleController : MonoBehaviour
{
    public float maxGrappleDistance;
    public float grappleSpeed = 20f;
    public float reelForce = 1f;
    public float swingForce;
    public float tension = 0f;
    public LayerMask grappleableLayer;

    private bool isGrappling = false;
    private Rigidbody2D rb;
    private Vector2 grapplePoint;
    private Vector2 grappleDir;
    private Vector2 mouseDir;
    private Vector2 fixedMousePos;
    private Vector2 currentMousPos; 
    private Vector2 onClickMousePos;
    private DistanceJoint2D dj;
    
    private const int toungeInputButton = 0;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        dj = GetComponent<DistanceJoint2D>();
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
    }

    //This will need to be more of an extension of the tongue hitting the collider rather than an instant point and the tongue should have a collidable rigidbody that the length of the tongue can interact with
    private void StartGrapple()
    {
        //visual representation (Need to save fixedMousePos for direction of swing visual)
        fixedMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, fixedMousePos - (Vector2)transform.position, maxGrappleDistance, grappleableLayer);

        if (hit.collider != null)
        {
            isGrappling = true;
            //Raycast tongue collision point
            grapplePoint = hit.point;
            //distanceJoint2D enabling, anchor point and distance set
            dj.connectedAnchor = grapplePoint;
            dj.distance = hit.distance;
            dj.enabled = true;
            //To get mousepos on click
            onClickMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //copilot did this
            // stick a little cursor where the tounge is attached (for ease of showing vector of mouse displacement relative to tounge grapple point)
            //save the current tounge length - it shouldn't change unless the mouse moves (give or take a little springyness)
        }
    }    

    private void StopGrapple()
    {
        isGrappling = false;
        dj.distance = 0f;
        dj.enabled = false;
    }

    private void Swing()
    {
        grappleDir = (grapplePoint - (Vector2)transform.position).normalized;
        mouseDir = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
        //save current mouse screen-space position 
        currentMousPos = Input.mousePosition;
        //Getting the direction of swing relative to fixedMousePos and the currentMousePos
        Vector2 swingDir = currentMousPos - fixedMousePos;
        //Mouse input direction of force needs adjustment as it dependson mousePostion instead of on click mouse position
        rb.AddForce(swingDir * swingForce);           

        //limiting our ridigbody moving past the tongue max grapple distance, will need to change it so the added max length of the tongue adds a reverse force for tension instead of making rb.force zero
        //if (dj.distance >= maxGrappleDistance)
        //{
        //    //rb.velocity = Vector2.zero;
        //    //rb.angularVelocity = 0f;
        //}

        // Draw a debug line to visualize the grapple direction and length
        Debug.DrawLine(transform.position, grapplePoint, Color.yellow);
        //Visual debug of mousePositions
        Debug.DrawLine(Camera.main.ScreenToWorldPoint(currentMousPos), onClickMousePos, Color.red);
    }

    /// <summary>
    /// I would suggest having this as a reel up the tongue only in the tongue direction and have the jump code separate
    /// </summary>
    private void Reel()
    {
        if (fixedMousePos != currentMousPos) // should drop decimals for rounding, but whatever
        {
            fixedMousePos = Input.mousePosition;
            //Debug.Log($"Mouse position difference vector: {lastScreenSpaceMousePoint - screenSpaceGrapplePoint}");             
        }

        // Debug.DrawLine(screenSpaceGrapplePoint, lastScreenSpaceMousePoint, Color.blue); // will need to use something other than debug.drawline
        
        //force achor to not be able to reel past the maxGrappleDistance
        if (dj.distance <= maxGrappleDistance)
        {
            //swingForceDir = (screenSpaceGrapplePoint - lastScreenSpaceMousePoint).normalized;
            
        }
    }

    //apply tension force opposite to grappledirection possibly using the other distancejoint2d connected from body to tongue
    //private void Tension()
    //{
    //    if(currentGrappleDistance > maxGrappleDistance)
    //    {

    //    }
    //}
}