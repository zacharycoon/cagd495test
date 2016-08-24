using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Components
{

	public class PlayerMovement : CustomComponentBase {

		Components.Jumping _jump;
		Components.Dash _dash;
		Components.WallGrab _wallGrab;


	
		CharacterController charCont;

		bool waitForJump;
		public static bool applyGravity = true;
		public static bool overrideInput = false;
		float walled;
		public static float verticleSpeed;
		public static float normalSpeed = 15f;
		public static float boostedSpeed = 30f;
		public static float speed = 15f;
		public static float speedModifier = 1f;
		public float jumpHeight = 28.5f;
		public float dashJump;
		public static Vector2 moveVector;
		public static float playerDir;
		public float gravity = 80f;
		public bool grounded;
		public bool checkforwalls;
		public static bool checkforground = true;
		public float upRayLength = 0.9f;
		public float ceilingHitSpeed = -1f;
		int celingmask = 1 << 8;
		int movingGround = 1 << 9;
		public float maxVertSpeed = -40;
		// Use this for initialization
		void Awake () {
			_jump = gameObject.AddComponent<Jumping> ();
			_dash = gameObject.AddComponent<Dash> ();
			_wallGrab = gameObject.AddComponent<WallGrab> ();

			charCont = this.gameObject.GetComponent<CharacterController> ();
		}

		// Update is called once per frame
	



		public void MovePlayer(float playerDirection){

			RaycastHit hit;
			Ray downRay = new Ray (transform.position, -Vector3.up); 
			if (Physics.Raycast (downRay, out hit, 1f)) {
				if (hit.transform.gameObject.layer == 9) {
					this.gameObject.transform.SetParent (hit.transform);
				} else {
					this.gameObject.transform.SetParent (null);
				}
			} else {
				this.gameObject.transform.SetParent (null);
			}

			if (checkforground) {
				grounded = charCont.isGrounded;
			} else {
				grounded = false;
			}
			playerDir = playerDirection;
			_dash.ManageDashing (grounded, playerDir);

			if (grounded && !waitForJump) {
				_jump.resetJumps ();
			
			}

			if (walled !=0) {

				_dash.OverrideDash ();
			}

			if (!grounded && !checkforwalls) {
				//checkforwalls = true;
			}
			if (grounded) {
				_wallGrab.notWall = 0f;
				_dash.OverrideDash ();
				_dash.ResetDashing ();
			}


			if (applyGravity) {
				if (grounded) {


					verticleSpeed = -0.1f;
					checkforwalls = false;
				}
				if (!grounded) {
					if (verticleSpeed > maxVertSpeed) {
						verticleSpeed -= gravity * Time.deltaTime;
					}
				}
				if (!waitForJump) {
					checkforwalls = true;

				}
			}

			Upray ();
			if(!overrideInput){
		
				moveVector = new Vector2 (playerDirection * speed* speedModifier, verticleSpeed); //calculate movement in the x and y 
			
			}
		
			charCont.Move (moveVector * Time.deltaTime); //apply movement in the x and y
		
			if (transform.position.z != 0) {
				transform.position = new Vector3 (transform.position.x, transform.position.y, 0f);
			}

		}

	
		public void JumpPlayer(float Direction){
			if (_wallGrab.notWall != 0) {
				return;
			}

			if ((walled != 0)&&!grounded) {
				_jump.WallJump (Direction, walled);

				return;
			} else {
			//	if (grounded) {
					StartCoroutine ("groundJump");
					
					return;
			//	}
			//	_jump.BasicJump (jumpHeight);
			//	print ("goddammit");
			//	return;
			}
		
		}

		public void WallGrab(){
			if (checkforwalls && !grounded) {

				walled = _wallGrab.WallSlide (playerDir);
				//Debug.Log (walled);
			}
			
		}

		public void DashPlayer(){
			if (walled != 0) {
				return;
			}
		//	if (grounded) {
				StartCoroutine ("GroundDash");
				_dash.StartDashing (playerDir);
				return;
		//	}
			//_jump.BasicJump (dashJump);
		//	_dash.StartDashing (playerDir);
		}
	

		public void Upray(){

			//Ray UpRay = new Ray (transform.position , transform.up); 
			if(Physics.Raycast(transform.position, transform.up, upRayLength, celingmask)){
				verticleSpeed = ceilingHitSpeed;
			}
		}


		IEnumerator GroundDash(){
			applyGravity = false;
			waitForJump = true;
			checkforground = false;
			//_jump.BasicJump (dashJump);

			yield return new WaitForSeconds (0.1f);
			checkforwalls = true;
			checkforground = true;
			waitForJump = false;
			applyGravity = true;
		}

		IEnumerator groundJump(){
			checkforwalls = false;
			waitForJump = true;
			checkforground = false;
			//applyGravity = false;
		
			_jump.BasicJump (jumpHeight);
			yield return new WaitForSeconds (0.05f);

			checkforground = true;
			//applyGravity = true;
			waitForJump = false;
		//	yield return new WaitForSeconds (0.1f);
			checkforwalls = true;
		



		}
}
}