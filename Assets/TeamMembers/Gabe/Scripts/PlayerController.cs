using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gabe;

namespace Gabe
{
	public class PlayerController : MonoBehaviour
	{
		public Camera cam;
		public Vector3 mousePos;
		private Rigidbody2D rb;

		public JumpSlider jumpSlider;
		public PlayerAudio playerAudio;
		
		public bool isOnGround;
		public float jumpCharge;
		public float jumpChargeTime;
		private float jumpForceMultiplier;
		public float jumpChargeLogBase = 10f;


		private void Awake(){
			rb = GetComponent<Rigidbody2D>();
			DontDestroyOnLoad(this.gameObject);
			jumpSlider = GetComponent<JumpSlider>();
			playerAudio = GetComponent<PlayerAudio>();
		}

		void Update(){
			mousePos = Input.mousePosition;
			
			if (Input.GetMouseButton(InputManager.jumpButton)){
				ChargeJump();
				jumpSlider.UpdateJumpSlider();
			}

			//start the jump once the mouse button is released
			if (Input.GetMouseButtonUp(InputManager.jumpButton)){
				ReleaseJump();
			}
		}

		void ChargeJump(){
			if (jumpCharge < 1){
				jumpCharge += (Time.deltaTime / jumpChargeTime); //Mathf.Log((Time.deltaTime / Mathf.Exp(jumpChargeTime)) + 1, jumpChargeLogBase);
				Debug.Log($"Jump {jumpCharge*100}% charged!");
				playerAudio.ChargeJump(jumpCharge);
			} else if (jumpCharge > 1) {
				jumpCharge = 1;
				Debug.Log($"Jump 100% charged");
			}
		}

		void ReleaseJump(){			
			playerAudio.ReleaseJump(jumpCharge); // Play jump release sound, at current intensity level
			Vector3 directionCalc = cam.ScreenToWorldPoint(mousePos) - transform.position; //check the direction of the jump
			directionCalc = directionCalc.normalized * (jumpCharge * jumpForceMultiplier); //add the jump force to the direction of the jump
			rb.AddForce(directionCalc, ForceMode2D.Impulse); //bingo boingo jumpo
			
			jumpCharge = 0; // Reset stuff
			jumpSlider.ResetJumpSlider();
		}

		private void OnDrawGizmos(){
			Debug.DrawLine(transform.position, cam.ScreenToViewportPoint(mousePos), Color.blue);
			Debug.DrawLine(transform.position, cam.ViewportToWorldPoint(new Vector3(mousePos.x,mousePos.y,0)), Color.red);
		}
	}
}