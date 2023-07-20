using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jono;
using UnityEngine.UI;

namespace Jono
{
	[RequireComponent(typeof(MouseJump))]
	public class JumpVisual : MonoBehaviour
	{
		public MouseJump mouseJump;
		public GameObject sliderGraphic;
		public Transform pivotPoint;
		public Slider jumpSlider;



		// Start is called before the first frame update
		void Awake()
		{
			mouseJump = GetComponent<MouseJump>();
			jumpSlider.maxValue = mouseJump.maxJump;

		}

		// Update is called once per frame
		void Update()
		{
			if(mouseJump.chargingJump)
			{
				DisplayJump();
			}

			if (!mouseJump.chargingJump)
			{
				ResetJump();
			}
		}

		void DisplayJump()
		{
			//display the jump slider while charging a jump
			sliderGraphic.SetActive(true);
			//set the value of the slider to match the current jump force
			jumpSlider.value = mouseJump.jumpForce;
			//getting directions
			Vector3 targetDirection = mouseJump.cam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
			targetDirection.Normalize();
			float rotationZ = Mathf.Atan2(targetDirection.y,targetDirection.x) * Mathf.Rad2Deg;
			//rotate the pivotPoint to face the mouse direction
			pivotPoint.transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);
		}

		void ResetJump()
		{
			//hide the nasty slider when not jumping
			sliderGraphic.SetActive(false);
			//reset the slider value
			jumpSlider.value = 0;	
		}

	}
}


