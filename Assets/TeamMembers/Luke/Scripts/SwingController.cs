using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingController : MonoBehaviour
{
    public float maxTongueLength;
    public Transform tongue;
    public Transform tongueTip;
    public Transform mouthPos;

    private DistanceJoint2D distanceJoint;
    private Quaternion initialRotation;
    private TongueCollisionHandler tongueCollisionHandler;

    private void Start()
    {
        distanceJoint = tongue.GetComponent<DistanceJoint2D>();
        initialRotation = tongueTip.localRotation;
        tongueCollisionHandler = tongue.GetComponentInChildren<TongueCollisionHandler>();
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
        {
            
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

        tongue.localPosition = direction;
        tongueTip.localPosition = direction.normalized * distance;
        tongueTip.localRotation = initialRotation;
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







