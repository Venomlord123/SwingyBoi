using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TongueCollisionHandler : MonoBehaviour
{
    private bool isTongueStuck = false; // Flag to track if the tongue is currently stuck to a collider

    public bool IsTongueStuck
    {
        get { return isTongueStuck; }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("StickySurface"))
        {
            isTongueStuck = true; // Set the flag to indicate the tongue is stuck
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("StickySurface"))
        {
            isTongueStuck = false; // Reset the flag when the tongue stops colliding with the sticky surface
        }
    }
}

