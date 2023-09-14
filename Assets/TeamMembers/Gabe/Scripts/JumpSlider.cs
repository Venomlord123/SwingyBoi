using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gabe;
using UnityEngine.UI;

namespace Gabe
{
	[RequireComponent(typeof(PlayerController))]
	public class JumpSlider : MonoBehaviour
	{
		public PlayerController playerController;
		public GameObject sliderGraphic;
		public Transform pivotPoint;
		public Slider jumpSlider;

		void Awake(){
			playerController = GetComponent<PlayerController>();
			jumpSlider.maxValue = 1;

		}

		// public void StartJumpSlider(){
		// 	//display the jump slider while charging a jump
		// 	sliderGraphic.SetActive(true);
		// }

		public void UpdateJumpSlider(){
			sliderGraphic.SetActive(true);
			//set the value of the slider to match the current jump force
			jumpSlider.value = playerController.jumpCharge;
			//getting directions
			Vector3 targetDirection = playerController.cam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
			targetDirection.Normalize();
			float rotationZ = Mathf.Atan2(targetDirection.y,targetDirection.x) * Mathf.Rad2Deg;
			//rotate the pivotPoint to face the mouse direction
			pivotPoint.transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);
		}

		public void ResetJumpSlider(){
			//hide the nasty slider when not jumping
			sliderGraphic.SetActive(false);
			//reset the slider value
			jumpSlider.value = 0;
		}
	}
}


