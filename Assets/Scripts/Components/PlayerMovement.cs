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
		public static bool overrideInput;
		public float walled;
		public static float verticleSpeed;
		public static float speed = 15;
		public static Vector2 moveVector;
		public static float playerDir;
		float gravity = 80f;
		bool grounded;
		public bool checkforwalls;
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
			
			walled = _wallGrab.WallSlide (playerDir);
			
			
		}

		public void DashPlayer(){

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