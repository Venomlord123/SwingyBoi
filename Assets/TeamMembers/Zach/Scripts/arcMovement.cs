using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class arcMovement : MonoBehaviour
{

    //All public variables
    public int layerNumber;
    //Jump strength modifer
    public float jSM;
    public float jumpStrength;

    //testing
    public bool isGrounded;

    //Private Variables
    private Transform player;
    private Rigidbody2D body;
    private new CapsuleCollider2D collider;
    private Vector3 camPosition;
    private Vector2 mouseDir;
    private Vector2 jump;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Transform>();
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        camPosition = (Camera.main.ScreenToWorldPoint(Input.mousePosition));
        mouseDir = camPosition - transform.position;
        arc();
    }

    void arc()
    {
        if(isGrounded)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                jump = new Vector2(mouseDir.x, mouseDir.y);
                jump = jump * jumpStrength;
                body.AddForce(jump * jSM);
                isGrounded = false;
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == layerNumber)
        {
            isGrounded = true;
            Text.print("Colliding");
        }
    }
}
