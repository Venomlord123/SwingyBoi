using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    public float zoomSpeed = 1f;
    public float minZoom = 5f;
    public float maxZoom = 10f;

    private Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        if (target == null)
            return;

        // Move the camera smoothly to the target position
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // Adjust camera zoom based on player's vertical velocity
        float targetZoom = Mathf.Lerp(maxZoom, minZoom, target.GetComponent<Rigidbody2D>().velocity.magnitude / 10f);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoom, zoomSpeed * Time.deltaTime);
    }
}
