using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleController : MonoBehaviour
{
    public float maxGrappleDistance;
    public float reelForce;
    public float swingForce;    
    public LayerMask grappleableLayer;

    private bool isGrappling = false;    
    private Vector2 grapplePoint;
    private Vector2 fixedMousePos;
    private Vector2 currentMousePos; 
    private Vector2 onClickMousePos;
    private Rigidbody2D rb;
    private DistanceJoint2D dj;

    //Mouse input
    private const int toungeInputButton = 0;
    private const int reelInputButton = 1;
    
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

            if(Input.GetMouseButton(reelInputButton))
            {
                Reel();
            }
        }
    }

    //This will need to be more of an extension of the tongue hitting the collider rather than an instant point and the tongue should have a collidable rigidbody that the length of the tongue can interact with
    private void StartGrapple()
    {
        //Need to save this position for Swing() and visual representation
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

            // stick a little cursor where the tounge is attached (for ease of showing vector of mouse displacement relative to tounge grapple point)
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
        //save current mouse screen-space position 
        currentMousePos = Input.mousePosition;
        //Getting the direction of swing relative to fixedMousePos and the currentMousePos
        Vector2 swingDir = currentMousePos - fixedMousePos;
        //Mouse input direction of force needs adjustment as it dependson mousePostion instead of on click mouse position
        rb.AddForce(swingDir * swingForce);

        //This is for the swing to work properly in the correct direction a little bit weird but works
        if (fixedMousePos != currentMousePos)
        {
            fixedMousePos = currentMousePos;             
        }

        // Draw a debug line to visualize the grapple direction and length
        Debug.DrawLine(transform.position, grapplePoint, Color.yellow);
        //Visual debug of mousePositions
        Debug.DrawLine(Camera.main.ScreenToWorldPoint(currentMousePos), onClickMousePos, Color.red);
    }

    /// <summary>
    /// We need to have the body line up with the anchor poistion on update so the player can reel faster
    /// </summary>
    private void Reel()
    {
        if(onClickMousePos != (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition))
        {
            //force achor to not be able to reel past the maxGrappleDistance
            if (onClickMousePos.y > Camera.main.ScreenToWorldPoint(Input.mousePosition).y && dj.distance <= maxGrappleDistance)
            {
                dj.distance += Time.deltaTime * reelForce;
                Debug.Log(onClickMousePos.y);
            }
            //no more reel force is applied if the anchor points are too close
            if (onClickMousePos.y < Camera.main.ScreenToWorldPoint(Input.mousePosition).y && dj.distance >= .1f)
            {
                dj.distance -= Time.deltaTime * reelForce;
                Debug.Log(onClickMousePos.y);
            }
        }
    }

    //apply tension force opposite to grappledirection possibly using the other distancejoint2d connected from body to tongue
    //private void Tension()
    //{
    //    if(currentGrappleDistance > tensionLength)
    //    {

    //    }
    //}
}