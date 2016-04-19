using UnityEngine;
using System.Collections;


namespace Assets.Scripts.Components
{

	public class WallGrab: CustomComponentBase {
		float wallDist = 0.6f;
	//	public LayerMask whatisWall = LayerMask.NameToLayer("Wall");
		int whatisWall = 1 << 8; //for some reason this assigns the raycast to look for the 8th layer mask, which should be "Wall"
								//if your "Wall" layer is not your 8th layer, you're in for some serious shit
		float wallBuffer = 0.05f;

		float wallDrag = -1.5f;
		bool jumpBuffer, walled;




		public float WallSlide(float playerDir) //handles both wall sliding and wall jumping
		{
			
			float wallDir = castRays ();

			if (wallDir != 0) {
				
				if (wallDir == 1) {
					if (playerDir == 1) {
	
						PlayerMovement.overrideInput = true;	
						PlayerMovement.moveVector = new Vector2 (0, wallDrag);
						PlayerMovement.verticleSpeed = 0f;
					} else {

							//	PlayerMovement.moveVector = new Vector2 ((wallBuffer * -1f), PlayerMovement.verticleSpeed);	
						transform.localPosition = new Vector3(transform.position.x + (wallBuffer *-1), transform.position.y, transform.position.z);
										PlayerMovement.overrideInput = false;	
					}
				}
				if (wallDir == -1) {
					if (playerDir == -1) {
	
						PlayerMovement.overrideInput = true;	
						PlayerMovement.moveVector = new Vector2 (0, wallDrag);
						PlayerMovement.verticleSpeed = 0f;
					} else {
	
						//	PlayerMovement.moveVector = new Vector2 ((wallBuffer * -1f), PlayerMovement.verticleSpeed);	
						transform.localPosition = new Vector3(transform.position.x + (wallBuffer), transform.position.y, transform.position.z);
						PlayerMovement.overrideInput = false;	
					}
				}
			}


		
				

			return wallDir;


		
		}
		float castRays(){
		//	if (whatisWall == null) {
			//	int whatisWall = 1 << 8;
		//	}
			//Debug.Log (whatisWall.value);
			bool rightwalled = false;
			bool leftwalled = false;

			Ray rightRay = new Ray (transform.position , Vector3.right); 
			Ray leftRay = new Ray (transform.position, Vector3.right * (-1));

			if(Physics.Raycast(rightRay, wallDist, whatisWall)) //check to see if there is a wall within wallDist to the right of us
			{
		
				return 1f;
				rightwalled = true;

			}
			else if(Physics.Raycast(leftRay, wallDist, whatisWall)) //check to see if there is a wall within wallDist to the left of us
			{
				
				return -1f;
				leftwalled = true;
			}

			else{ //if we are not on either wall then set walled to false to be used in other sections of controller

			
			return 0f;
			}
		


		}


	}
}