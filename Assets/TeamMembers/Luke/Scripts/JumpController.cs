using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpController : MonoBehaviour
{
    public float jumpCharge = 0f;

    private Vector2 mouseDir;
    private bool jumpIsCharging = false;
    private const int jumpInputButton = 1;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //this will need to be moved into a new jump script
        if (Input.GetMouseButton(jumpInputButton))
            Jump();

        if (jumpIsCharging && Input.GetMouseButtonUp(jumpInputButton))
            ReleaseJump();
    }
    private void Jump()
    {
        if (!jumpIsCharging)
        {
            jumpIsCharging = true;
            Debug.Log("Jump charging started");
        }

        if (jumpCharge < 10f)
        {
            jumpCharge += Time.deltaTime;
            Debug.Log($"Jump charge: {jumpCharge}");
        }
        else
        {
            Debug.Log("Jump charge maxed!");
        }
    }


    private void ReleaseJump()
    {
        mouseDir = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
        jumpIsCharging = false;
        rb.AddForce(mouseDir * jumpCharge * 1000f, ForceMode2D.Force);
        Debug.Log($"Jump Released. Force: {jumpCharge}");
        jumpCharge = 0f;
    }
}
