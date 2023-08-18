using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Luke
{
    public class TongueVisual : MonoBehaviour
    {
        public float extensionSpeed = 100;
        public GrappleController grappleController; //Attach GrappleController script
        public DistanceJoint2D dj2D; //DistanceJoint2D on player Ref

        private Vector2 initialScale; //Origin scale of sprite
        private Vector2 initialPos; //Origin position of sprite relative to player

        private void Start()
        {
            initialScale = transform.localScale;
            initialPos = transform.localPosition;
        }
        private void Update()
        {
            TongueExtendSprite();
        }

        private void TongueExtendSprite()
        {
            if (grappleController.isGrappling)
            {
                // Calculate the direction from tongue origin to the desired location
                Vector2 direction = grappleController.hit.point - (Vector2)transform.localPosition;

                // Calculate the distance between the tongue origin and the desired location
                float distance = dj2D.distance;

                // Calculate the new scale and position for the tongue sprite
                Vector2 newScale = new Vector2(initialScale.x + distance, initialScale.y);
                Vector2 newPosition = initialPos + direction * 0.5f; // Set position at the middle of the extension

                // Smoothly extend the tongue towards the desired location
                transform.localScale = Vector2.Lerp(transform.localScale, newScale, Time.deltaTime * extensionSpeed);
                transform.localPosition = Vector2.Lerp(transform.localPosition, newPosition, Time.deltaTime * extensionSpeed);
            }
            
            if(grappleController.isGrappling == false)
            {
                transform.localScale = initialScale;
                transform.localPosition = initialPos;
            }
        }
    }
}

