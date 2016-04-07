using UnityEngine;
using System.Collections;


namespace Assets.Scripts.Components
{

	public class WallGrab: CustomComponentBase {
		float wallDist = 0.6f;
		float wallBuffer = 2f;
		LayerMask whatisWall = LayerMask.NameToLayer("Wall");

		bool jumpBuffer, walled, right, left, Jump;
		float jumpState, wallJumping, totalWallJumps, wallDrag, verticleSpeed;
		Vector2 moveVector;


		void WallSlide(bool grounded) //handles both wall sliding and wall jumping
		{
			//manages wallgrabbing using raycasts, incorperates variabels: walled, grounded, vert speed, gravity
			bool rightwalled = false; //we use seperate rays to check for walls to either the left or right 
			bool leftwalled = false;
			if (grounded || jumpBuffer) //if we are grounded, don't even check for walls
			{
				walled = false;
				return;
			}
			else //if we are not grounded, start checking for walls
			{


				Ray rightRay = new Ray (transform.position , Vector3.right); 
				Ray leftRay = new Ray (transform.position, Vector3.right * (-1));

				if(Physics.Raycast(rightRay, wallDist, whatisWall)) //check to see if there is a wall within wallDist to the right of us
				{
					rightwalled = true;

				}
				if(Physics.Raycast(leftRay, wallDist, whatisWall)) //check to see if there is a wall within wallDist to the left of us
				{
					leftwalled = true;
				}
				if(rightwalled || leftwalled){ //if we are on either wall make walled true to be used in other sections of controller
					walled = true;
				}
				else{ //if we are not on either wall then set walled to false to be used in other sections of controller
					walled = false;
				}
			}
			if (walled) {
				jumpState = 0;
				if (rightwalled) { //if we are on the right wall
					if(right &&(wallJumping == 0)){ //and holding the right key while against the right wall, reset wall jumping and slowly drag down the wall
						moveVector = new Vector2 (0, wallDrag);

						if(Jump && (totalWallJumps < 2)){ //if I jump while I am on the wall, set wall jumping to one, which is handled in update
							totalWallJumps++;
							wallJumping = 1f; //as a reminder, wall jumping is used like a 3 variable boolean, with -1 being left, 1 being right, and 0 being stand still
							StartCoroutine ("wallJumpCD"); //and start the walljumpCD coroutine
						}
					} 
					else {
						moveVector = new Vector2 (wallBuffer * (-1), verticleSpeed); //if we are not holding the key towards the wall down
						//then create a small buffer zone between the player and wall
					}
				}
				if (leftwalled) {//same as above but for left instead
					if(left &&(wallJumping == 0)){
						moveVector = new Vector2 (0, wallDrag);

						if(Jump && (totalWallJumps < 2)){
							totalWallJumps++;
							wallJumping = -1f; //as a reminder, wall jumping is used like a 3 variable boolean, with -1 being left, 1 being right, and 0 being stand still
							StartCoroutine ("wallJumpCD");
						}
					} else {
						moveVector = new Vector2 (wallBuffer, verticleSpeed); //same as for right


					}
				}
			}
		}


	}
}