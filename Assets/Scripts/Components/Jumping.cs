using UnityEngine;
using System.Collections;


namespace Assets.Scripts.Components
{

	public class Jumping: CustomComponentBase {

		public bool JustwallJumped; // used for the destructable pillars
		public float wallJumpHeight = 20f;
		public float jumpStage = 0f;
		public static float maxJumps = 1;
		public static float maxWallJumps = 999f;

		float wallJumps = 0f;
		float wallJumpDir = 0f; 
		float wallJumpGrav = 0f;

		float wallJumpx = 12f;
		float wallJumpy = 20f;

		float realWallJumpy = 12f;
		float timeToApex  = 0.1f;
		float timeToMidApex = 0.13f;
		float timeBackToWall = 0.10f;
		float wallMidJumpx = 6f;
		float wallMidJumpy = 4f;

		float direction;
		private bool walled = false;
		private bool midWallJump;
		//private float totalWallJumps = 0f;





		public void BasicJump(float jumpHeight){
			
			if ((jumpStage < maxJumps)) {
				PlayerMovement.verticleSpeed = jumpHeight;
				jumpStage++;

			}
		

		}


		public void WallJump (float playerDir,float wallDir){

			if (wallDir == 1f) { //if we are on the right wall
				if ((playerDir > 0f) && (wallJumpDir == 0)) { //and holding the right key while against the right wall, reset wall jumping and slowly drag down the wall
				//	PlayerMovement.moveVector = new Vector2 (0, wallDrag);
					//Debug.Log ("More drag than west hollywood");
					}
					if((wallJumps < maxWallJumps)){ //if I jump while I am on the wall, set wall jumping to one, which is handled in update
						wallJumps++;
					PlayerMovement.overrideInput = true;	
	
						wallJumpDir = 1f; //as a reminder, wall jumping is used like a 3 variable boolean, with -1 being left, 1 being right, and 0 being stand still
						StartCoroutine ("wallJumpCD"); //and start the walljumpCD coroutine

				}
					 
					//else {
					//	PlayerMovement.moveVector = new Vector2 (wallBuffer * (-1), PlayerMovement.verticleSpeed); //if we are not holding the key towards the wall down
						//then create a small buffer zone between the player and wall
				//	}
				}
			if (wallDir == -1f) {//same as above but for left instead
				if ((playerDir < 0f) && (wallJumpDir == 0)) {
					//PlayerMovement.moveVector  = new Vector2 (0, wallDrag);

					if ((wallJumps < maxWallJumps)) {
						wallJumps++;
						PlayerMovement.overrideInput = true;
						wallJumpDir = -1f; //as a reminder, wall jumping is used like a 3 variable boolean, with -1 being left, 1 being right, and 0 being stand still
						StartCoroutine ("wallJumpCD");
					}
				}
				// else {
					//	PlayerMovement.moveVector  = new Vector2 (wallBuffer, PlayerMovement.verticleSpeed); //same as for right


					//}
				}
			

		}

		public void resetJumps(){
			wallJumps = 0;
			if (jumpStage == 0) {
				return;
			} else {
				jumpStage = 0;

			}
		}


		IEnumerator wallJumpCD(){ //this is invoked when the player wall jumps
			JustwallJumped = true;
			wallJumpy = realWallJumpy; //wallJumpy is modified when we jump, so we reset it at the start
	
			direction = wallJumpDir; //we feed walljumping into player direction so we get the direction of the wall jump
			PlayerMovement.moveVector = new Vector2 (wallJumpx * wallJumpDir * (-1), PlayerMovement.verticleSpeed + wallJumpy);
			yield return new WaitForSeconds (timeToMidApex); //this timer takes us to the mid apex of the jump, then we check if we should continue jumping or jump back to the wall
			JustwallJumped = false;
			/*
			if (direction == 1) { 
				if (PlayerMovement.playerDir > 0) { //if the player jumped on a right wall and is inputting right WHILE at the apex point

						
					wallJumpy = realWallJumpy; //reset wall jump gravity
					PlayerMovement.moveVector = new Vector2 (wallMidJumpx * direction, wallMidJumpy);

					yield return new WaitForSeconds (timeBackToWall); //this timer is how long it takes for us to get to the wall
					PlayerMovement.overrideInput = false;
					wallJumpDir = 0f; //reset wall jumping to zero, since we are back against the wall now we are no longer mid wall jump

					PlayerMovement.verticleSpeed = 0f;
			
					yield break;//since we jumped back towards the wall instead of continuing in an arc, break out
				}
			}
			if(direction== -1){ //mirrored of above
				if (PlayerMovement.playerDir < 0) {
					wallJumpy = realWallJumpy;
					PlayerMovement.moveVector = new Vector2 (wallMidJumpx * direction, wallMidJumpy);
					yield return new WaitForSeconds (timeBackToWall);
					PlayerMovement.overrideInput = false;
					wallJumpDir = 0f;
					PlayerMovement.verticleSpeed = 0f;

					yield break;
				}
			}
			//if we were not holding the key in the direction of the wall we were just on: 
			PlayerMovement.moveVector = new Vector2 (wallJumpx * wallJumpDir * (-1), PlayerMovement.verticleSpeed);
			yield return new WaitForSeconds(timeToApex); //wait for how long to reach the total apex
			*/
			PlayerMovement.overrideInput = false;
			PlayerMovement.verticleSpeed = wallJumpHeight;
			//PlayerMovement.verticleSpeed = 0f;
			wallJumpDir = 0f; //once we reach the total apex, we are no longer in the middle of wall jumping
			//wallJumpy = realWallJumpy; //since we modified walljumpY, reset it

		}

	}
}