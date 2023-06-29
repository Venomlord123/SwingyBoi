using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Luke
{
	public class JumpController : MonoBehaviour
	{
		public Vector3 mousePos;
		public float currentJumpForce;
		public float maxJump;
		public float jumpTime;
		public float jumpPowerMultiplier;

		private Rigidbody2D rb;
		private GrappleController gc;

		private void Start()
		{
			rb = GetComponent<Rigidbody2D>();
			gc = GetComponent<GrappleController>();
		}

		// Update is called once per frame
		void Update()
        {
            JumpControlHandler();
        }

		/// <summary>
		/// Will need to change up the controller that's not reliant on basic Unity Input
		/// </summary>
        private void JumpControlHandler()
        {
            if (Input.GetMouseButton(1) && !gc.isGrappling)
            {
                Debug.Log("Mouse is held for jump");
                ChargeJump();
            }
            mousePos = Input.mousePosition;
            //start the jump once the mouse button is released
            if (Input.GetMouseButtonUp(1) && !gc.isGrappling)
            {
                ReleaseJump();
                Debug.Log("Mouse up release jump");
            }
        }

        void ChargeJump()
		{
			Debug.DrawLine(transform.position, Camera.main.ScreenToWorldPoint(mousePos), Color.blue);
			//charge up over time to a max value
			currentJumpForce += jumpTime * Time.deltaTime;
			//clamp the jump force to a max value
			if (currentJumpForce >= maxJump)
			{
				currentJumpForce = maxJump;
			}
		}

		void ReleaseJump()
		{
			Debug.Log("The jump force is " + currentJumpForce);
			//check the direction of the jump
			Vector3 directionCalc = Camera.main.ScreenToWorldPoint(mousePos) - transform.position;
			//add the jump force to the direction of the jump
			directionCalc = directionCalc.normalized * (currentJumpForce * jumpPowerMultiplier);
			//bingo boingo jumpo
			rb.AddForce(directionCalc, ForceMode2D.Force);
			//reset the jump force
			currentJumpForce = 0;
		}
	}
}

