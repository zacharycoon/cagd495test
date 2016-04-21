using UnityEngine;
using System.Collections;


namespace Assets.Scripts.Components
{
	

	public class Dash: CustomComponentBase {
		public enum DashPhase{
			resting,
			startingLock,
			lockedDash,
			unlockedDash
		}
		public static bool DashUnlocked = false;
		public bool canDash;
		float dashGrav;
		float dashPhase = 0f;
		float dashSpeedIncrease = 30f;
		float dashJumpHeight = 15f;
		bool Dashing;
		bool facingRight = true;
		float dashDir;
		float lockedDashDur = 0.5f;
		public DashPhase currentPhase;

		public void ManageDashing(bool grounded, float playerDir){
			if (!DashUnlocked) {
				return;
			}


			if (playerDir == 1) {
				facingRight = true;
			} 
			if (playerDir == -1) {
				facingRight = false;
			}

			if (grounded) {
				
				ResetDashing ();
			}

			if (currentPhase == DashPhase.resting) { //dashPhase handles which stage of dashing I am in, with 0 being not dashing at all

				//PlayerMovement.speed += realspeed; //if I am not dashing, make sure that speed is set to my regular speed

				return;
			}

			if (currentPhase == DashPhase.startingLock) { //inputted the dash button, now start timers for locked dash and checking to reset dash
				
				if (facingRight) { //we use playerDirection to decide which direction we dash										//
					dashDir = 1f;
				} else {
					dashDir = -1f;
				}
				//grounded = false; 
				PlayerMovement.overrideInput = true;


				//StartCoroutine ("SetBoost"); //dashing speed is reset upon touching the ground, set boost prevents 
				PlayerMovement.verticleSpeed = dashJumpHeight; //the ground check from checking if we are grounded until a small amount of time has passed
			
				PlayerMovement.speed = dashSpeedIncrease; //speed up the character to boosted speed 
				PlayerMovement.moveVector = new Vector2(dashDir *dashSpeedIncrease, PlayerMovement.verticleSpeed);

				StartCoroutine ("SetDashing"); //The player is going to be put into dash phase 2, and while in dash phase
				currentPhase = DashPhase.lockedDash;

			}
			if (currentPhase == DashPhase.lockedDash) { //we just hang out here with our speed boosted until the player hits something or changes directions
				PlayerMovement.moveVector = new Vector2(dashDir * dashSpeedIncrease, PlayerMovement.verticleSpeed);

				//keep at it yo
			}
			if (currentPhase == DashPhase.unlockedDash) {
				if (playerDir != dashDir) {
					PlayerMovement.speed = PlayerMovement.normalSpeed;
				}
				PlayerMovement.overrideInput = false;
			}


		}
		public void ResetDashing(){
			//dashPhase = 0f;
			canDash = true;
			if (currentPhase == DashPhase.resting) {
			//	return;
			}

			StopCoroutine ("SetDashing");
			currentPhase = DashPhase.resting;
			PlayerMovement.overrideInput = false;
			PlayerMovement.speed = PlayerMovement.normalSpeed;
		}

		public void StartDashing(float playerDir){
			if (!canDash || (currentPhase != DashPhase.resting)) {
				return;
			}
	
			if (currentPhase == DashPhase.resting) {
				canDash = false;
				currentPhase = DashPhase.startingLock;

			}
		}

		public void OverrideDash(){
			if (currentPhase == DashPhase.resting) {
				return;
			}

			PlayerMovement.overrideInput = false;

			PlayerMovement.speed = PlayerMovement.normalSpeed;
			currentPhase = DashPhase.resting;

		}

		IEnumerator SetDashing(){


			yield return new WaitForSeconds (lockedDashDur); //this controls how long the player has their input locked  while dashing
			if (currentPhase != DashPhase.resting) {
				currentPhase = DashPhase.unlockedDash; 
			}
		}


	}
}