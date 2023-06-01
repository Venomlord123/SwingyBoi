using System.Security.Principal;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swing : MonoBehaviour
{
    public Transform tongueTip;  // The tip of the frog's tongue
    public float tongueMaxLength = 2f;  // The maximum length the tongue can stretch
    public float tongueSpeed = 5f;  // The speed at which the tongue extends and retracts
    public float moveSpeed = 5f;  // The speed at which the frog moves up and down the tongue
    public Rigidbody2D rb;  // Reference to the frog's rigidbody

    private bool isSwinging = false;  // Flag to check if the frog is currently swinging
    private bool isMovingUp = false;  // Flag to check if the frog is moving up the tongue
    private DistanceJoint2D tongueJoint;  // Reference to the DistanceJoint2D component

    private void Start()
    {
    // tongueJoint = GetComponent<DistanceJoint2D>();
    tongueJoint = GetComponentInChildren<DistanceJoint2D>();

    // Set up the initial tongue configuration
    tongueJoint.enabled = false;
    tongueJoint.connectedBody = null;
    tongueJoint.distance = 0f;
    }

    private void Update()
    {
    HandleInput();
    }

    private void HandleInput()
    {
    if (Input.GetMouseButtonDown(0))
    {
        // Start swinging the tongue
        StartSwinging();
    }
    else if (Input.GetMouseButtonUp(0))
    {
        // Stop swinging the tongue
        StopSwinging();
    }

    if (isSwinging)
    {
        // Check if the frog is moving up or down the tongue
        float moveDirection = Input.GetAxis("Vertical");
        float moveDistance = moveSpeed * moveDirection * Time.deltaTime;
        rb.MovePosition(rb.position + new Vector2(0f, moveDistance));
    }
    }

    private void StartSwinging()
    {
    // Enable the tongue joint and connect it to the tongue tip
    tongueJoint.enabled = true;
    tongueJoint.connectedBody = tongueTip.GetComponent<Rigidbody2D>();

    isSwinging = true;
    }

    private void StopSwinging()
    {
    // Disable the tongue joint and disconnect it
    tongueJoint.enabled = false;
    tongueJoint.connectedBody = null;

    isSwinging = false;
    }

}
