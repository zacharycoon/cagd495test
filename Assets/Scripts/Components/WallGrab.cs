using UnityEngine;
using System.Collections;


namespace Assets.Scripts.Components
{

	public class WallGrab: CustomComponentBase {
		float wallDist = 0.6f;
		public LayerMask whatisWall = LayerMask.NameToLayer("Wall");

		bool jumpBuffer, walled;




		public float WallSlide(bool grounded) //handles both wall sliding and wall jumping
		{
			
			//manages wallgrabbing using raycasts, incorperates variabels: walled, grounded, vert speed, gravity
		
			//we use seperate rays to check for walls to either the left or right 

		
				

			return castRays ();


		
		}
		float castRays(){
			bool rightwalled = false;
			bool leftwalled = false;

			Ray rightRay = new Ray (transform.position , Vector3.right); 
			Ray leftRay = new Ray (transform.position, Vector3.right * (-1));

			if(Physics.Raycast(rightRay, wallDist, whatisWall)) //check to see if there is a wall within wallDist to the right of us
			{
				Debug.Log ("1f");
				return 1f;
				rightwalled = true;

			}
			else if(Physics.Raycast(leftRay, wallDist, whatisWall)) //check to see if there is a wall within wallDist to the left of us
			{
				Debug.Log ("-1f");	
				return -1f;
				leftwalled = true;
			}

			else{ //if we are not on either wall then set walled to false to be used in other sections of controller

				Debug.Log ("0f");
			return 0f;
			}
		}


	}
}