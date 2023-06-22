using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jono
{
	public class MouseJump : MonoBehaviour
	{
		public Vector3 mousePos;
		public Camera cam;

		public float jumpForce = 0f;
		public float maxJump = 25f;
		public float jumpTime = 5.5f;

		private Rigidbody rb;

		private void Awake()
		{
			rb = GetComponent<Rigidbody>();
		}

		// Update is called once per frame
		void Update()
		{
			if (Input.GetMouseButton(0))
			{
				Debug.Log("Mouse is held");
				ChargeJump();
			}
			mousePos = Input.mousePosition;
			//start the jump once the mouse button is released
			if (Input.GetMouseButtonUp(0))
			{
				ReleaseJump();
				Debug.Log("Mouse Up");
			}
		}

		void ChargeJump()
		{
			//charge up over time to a max value
			jumpForce += jumpTime * Time.deltaTime;
			//clamp the jump force to a max value
			if (jumpForce >= maxJump)
			{
				jumpForce = maxJump;
			}
		}

		void ReleaseJump()
		{
			Debug.DrawLine(transform.position, cam.ScreenToWorldPoint(mousePos), Color.blue);

			Debug.Log("The jump force is " + jumpForce);
			//check the direction of the jump
			Vector3 directionCalc = cam.ScreenToWorldPoint(mousePos) - transform.position;
			//add the jump force to the direction of the jump
			directionCalc = directionCalc.normalized * jumpForce;
			//bingo boingo jumpo
			rb.AddForce(directionCalc, ForceMode.Impulse);
			//reset the jump force
			jumpForce = 0;
		}
	}
}


