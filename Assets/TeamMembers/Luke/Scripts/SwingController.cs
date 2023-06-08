using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingController : MonoBehaviour
{
    public float maxTongueLength;
    public float tongueMoveSpeed;
    public Transform tongue;
    public Transform tongueTip;
    public Transform mouthPos;

    private DistanceJoint2D distanceJoint;
    private Quaternion initialRotation;
    private TongueCollisionHandler tongueCollisionHandler;

    private void Start()
    {
        tongueCollisionHandler = tongue.GetComponentInChildren<TongueCollisionHandler>();
        distanceJoint = tongue.GetComponent<DistanceJoint2D>();
        initialRotation = tongueTip.localRotation;
    }

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (Input.GetMouseButton(0))
        {
            AdjustDistanceJoint();
            UpdateTonguePosition();

            if (!tongueCollisionHandler.IsTongueStuck)
            {
                ResetTonguePosition();
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            ResetTonguePosition();
        }
    }

    private void UpdateTonguePosition()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - mouthPos.position;
        float distance = direction.magnitude;

        if (distance > maxTongueLength)
        {
            distance = maxTongueLength;
            direction = direction.normalized * maxTongueLength;
        }

         // Calculate the target position based on the direction and tongue move speed
        Vector3 targetPosition = mouthPos.position + (direction.normalized * distance * tongueMoveSpeed * Time.deltaTime);

        // Move the tongue towards the target position
        tongue.position = Vector3.Lerp(tongue.position, targetPosition, tongueMoveSpeed * Time.deltaTime);

        // Calculate the target position for the tongue tip
        Vector3 targetTipPosition = targetPosition.normalized * distance;

        // Move the tongue tip towards the target position
        tongueTip.position = Vector3.Lerp(tongueTip.position, targetTipPosition, tongueMoveSpeed * Time.deltaTime);
        tongueTip.localRotation = initialRotation;

        // Debug linecast to visualize the direction
        Debug.DrawLine(mouthPos.position, targetPosition, Color.green);
    }

    private void AdjustDistanceJoint()
    {
        if (distanceJoint.distance != maxTongueLength)
        {
            distanceJoint.distance = maxTongueLength;
        }
    }

    private void ResetTonguePosition()
    {
        tongue.localPosition = mouthPos.localPosition;
        tongue.localRotation = mouthPos.localRotation;
        tongueTip.localPosition = mouthPos.localPosition;
        tongueTip.localRotation = initialRotation;
        distanceJoint.distance = 0f;
    }
}







