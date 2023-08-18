using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Luke
{
    public class GrappleController : MonoBehaviour
    {
        //Adjustable variables
        [Header ("CONTROLS"), Space]
        [Tooltip ("While inverted true the player will reel up towards the tongue if the current mouse position is above grapple click position and vice versa")]
        public bool invertedReelControls = false;
        [Header("ADJUSTABLE TONGUE VARIABLES"), Space]
        [Tooltip ("Max grapple length the tongue can reach from the tongue's current position")]
        public float maxGrappleDistance;
        [Tooltip("The power of force at which the player can move the mouse whilst grappling to gain momentum")]
        public float swingForce;
        [Tooltip ("The speed at which the player will move up and down the tongue")]
        public float reelForce;
        [Space]
        [Header("FOR VIEWING ONLY")]
        public bool isGrappling = false;
        public bool isReeling = false;
        [Space]
        [Header ("REFRENCES")]
        [Tooltip("The Layer at which the tongue is able to grapple")]
        public LayerMask grappleableLayer;
        public RaycastHit2D hit;

        //In code handeling variables only
        public Vector2 grapplePoint;
        private Vector2 fixedMousePos;
        private Vector2 currentMousePos;
        private Vector2 onClickMousePos;
        private Rigidbody2D rb;
        private DistanceJoint2D dj;

        //Mouse input controls
        private const int toungeInputButton = 0;
        private const int reelInputButton = 1;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            dj = GetComponent<DistanceJoint2D>();
        }

        private void Update()
        {
            //Controls and condition checks
            HandleInput();
        }

        //May need to handle controls in a different way in the future, not sure if this will be the most efficient way to handle input
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

                if (Input.GetMouseButton(reelInputButton))
                {
                    Reel();
                }
                if (Input.GetMouseButtonUp(reelInputButton))
                {
                    ReelingStop();
                }
            }
        }

        //This will need to be more of an extension of the tongue hitting the collider rather than an instant point and the tongue should have a collidable rigidbody that the length of the tongue can interact with
        private void StartGrapple()
        {
            //Need to save this position for Swing() and use for visual representation
            fixedMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            hit = Physics2D.Raycast(transform.position, fixedMousePos - (Vector2)transform.position, maxGrappleDistance, grappleableLayer);

            if (hit.collider != null)
            {
                isGrappling = true;
                //Raycast tongue collision point
                grapplePoint = hit.point;
                //distanceJoint2D enabling, anchor point and distance set
                dj.connectedAnchor = grapplePoint;
                dj.distance = hit.distance;
                dj.enabled = true;
                //To get mousepos on click following the camera
                onClickMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                // stick a little cursor where the onclickpos is attached (for ease of showing vector of mouse displacement relative to tounge grapple point)
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
            //Mouse input direction of force needs adjustment as it depends on mousePostion instead of on click mouse position
            rb.AddForce((swingDir * swingForce) * Time.deltaTime);
                       
            ///////////To adjust the body to the anchor position swinging (I think this can be done better but not sure how yet)            
            if(isReeling && dj.distance > 0f || isReeling && dj.distance < maxGrappleDistance)
            {
                rb.AddForce(-((grapplePoint - (Vector2)transform.position) * reelForce));
            }

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
            isReeling = true;
            //Current mouse position following camera game space
            Vector2 reelMouseposition = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (onClickMousePos != reelMouseposition && dj.distance <= maxGrappleDistance && dj.distance >= 0f)
            {                
                //Non Inverted reel
                if (transform.position.y < onClickMousePos.y)
                {
                    if (onClickMousePos.y > reelMouseposition.y && !invertedReelControls || onClickMousePos.y < reelMouseposition.y && invertedReelControls)
                    {
                        dj.distance += Time.deltaTime * reelForce;
                    }

                    if (onClickMousePos.y >= reelMouseposition.y && invertedReelControls || onClickMousePos.y <= reelMouseposition.y && !invertedReelControls)
                    {
                        dj.distance -= Time.deltaTime * reelForce;
                    }
                }

                //Inverted reel
                else
                {
                    if (onClickMousePos.y >= reelMouseposition.y && !invertedReelControls || onClickMousePos.y <= reelMouseposition.y && invertedReelControls)
                    {
                        dj.distance -= Time.deltaTime * reelForce;
                    }

                    if (onClickMousePos.y > reelMouseposition.y && invertedReelControls || onClickMousePos.y < reelMouseposition.y && !invertedReelControls)
                    {
                        dj.distance += Time.deltaTime * reelForce;
                    }
                }
            }
            //Making sure we keep our positions in correct lengths of tongue
            if (dj.distance > maxGrappleDistance)
            {
                dj.distance = maxGrappleDistance;
            }

            if (dj.distance < 0f)
            {
                dj.distance = 0f;
            }
        }
        private void ReelingStop()
        {
            isReeling = false;

            //Making sure we keep our positions in correct lengths of tongue
            if (dj.distance > maxGrappleDistance)
            {
                dj.distance = maxGrappleDistance;
            }

            if (dj.distance < 0f)
            {
                dj.distance = 0f;
            }
        }
    }
}
