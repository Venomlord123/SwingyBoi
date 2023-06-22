using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zach
{


    public class simpleMovement : MonoBehaviour
    {
        public new Rigidbody rigidbody;
        public float forceStrength;
        public float velocity;
        public float velocityMax;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            Movement();

            velocity = rigidbody.velocity.magnitude;

            if (rigidbody.velocity.magnitude > velocityMax)
            {
                rigidbody.AddForce(rigidbody.velocity * -0.8f);
            }
        }

        public void Movement()
        {
            if (Input.GetKey(KeyCode.D))
            {
                rigidbody.AddForce(new Vector3(1, 0) * forceStrength);
            }
            if (Input.GetKey(KeyCode.A))
            {
                rigidbody.AddForce(new Vector3(-1, 0) * forceStrength);
            }
        }


    }
}
