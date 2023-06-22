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
			if (jumpForce >= maxJump)
			{
				jumpForce = maxJump;
			}
		}

		void ReleaseJump()
		{
			Debug.DrawLine(transform.position, cam.ScreenToWorldPoint(mousePos), Color.blue);
			Debug.Log("The jump force is " + jumpForce);
			Vector3 directionCalc = cam.ScreenToWorldPoint(mousePos) - transform.position;
			directionCalc = directionCalc.normalized * jumpForce;
			rb.AddForce(directionCalc, ForceMode.Impulse);
			jumpForce = 0;
		}
	}
}


