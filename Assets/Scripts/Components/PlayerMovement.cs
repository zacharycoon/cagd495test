using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Components
{

	public class PlayerMovement : CustomComponentBase {

		Components.Jumping _jump;
		Components.Dash _dash;
		Components.WallGrab _wallGrab;

	
		CharacterController charCont;

		public static bool applyGravity = true;
		public static bool overrideInput = false;
		float walled;
		public static float verticleSpeed;
		public static float normalSpeed = 15f;
		public static float boostedSpeed = 25f;
		public static float speed = 15f;
		public static Vector2 moveVector;
		public static float playerDir;
		public float gravity = 80f;
		bool grounded;
		bool checkforwalls;
		// Use this for initialization
		void Awake () {
			_jump = gameObject.AddComponent<Jumping> ();
			_dash = gameObject.AddComponent<Dash> ();
			_wallGrab = gameObject.AddComponent<WallGrab> ();

			charCont = this.gameObject.GetComponent<CharacterController> ();
		}

		// Update is called once per frame
	



		public void MovePlayer(float playerDirection){
			grounded = charCont.isGrounded;
			playerDir = playerDirection;

			_dash.ManageDashing (grounded, playerDir);

			if ((grounded || (walled != 0f))&& applyGravity) {
				_dash.ResetDashing ();
			}


			if (applyGravity) {
				if (grounded) {
					
					_jump.resetJumps ();
					verticleSpeed = -0.1f;
					checkforwalls = false;
				}
				if (!grounded) {
					verticleSpeed -= gravity * Time.deltaTime;
				}
			}

		
			if(!overrideInput){
		
				moveVector = new Vector2 (playerDirection * speed, verticleSpeed); //calculate movement in the x and y 
			}
		
			charCont.Move (moveVector * Time.deltaTime); //apply movement in the x and y
		
		}

	
		public void JumpPlayer(float Direction){
			if (walled != 0) {
				_jump.WallJump (Direction, walled);

				return;
			} else {
				if (grounded) {
					StartCoroutine ("groundJump");
					return;
				}
				_jump.BasicJump ();
			
				return;
			}
		
		}

		public void WallGrab(){
			if (checkforwalls&& !grounded) {
				walled = _wallGrab.WallSlide (playerDir);
			}
			
		}

		public void DashPlayer(){
			if (grounded) {
				StartCoroutine ("GroundDash");
			}
			_dash.StartDashing (playerDir);
		}
	
		IEnumerator GroundDash(){
			applyGravity = false;
			_dash.StartDashing (playerDir);
			yield return new WaitForSeconds (0.1f);
			applyGravity = true;
		}

		IEnumerator groundJump(){
			applyGravity = false;
			_jump.BasicJump ();
			yield return new WaitForSeconds (0.1f);
			applyGravity = true;
			yield return new WaitForSeconds (0.05f);
			checkforwalls = true;


		}
}
}