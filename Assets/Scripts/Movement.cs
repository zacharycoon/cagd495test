	using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {
    //other shit
	private CharacterController charCont;


    //Input shit
    private bool right = false;
    private bool left = false;
	public bool Dash = false;
    private bool Jump = false;
   	private bool canDoubleJump = false;
	private bool moving = false;
    //movement shit
    public float gravRate;
	private float verticleSpeed = 0f;
	private Vector2 moveVector = Vector2.zero;
    public float speed;
	private float gravity;

    public float jumpHeight;
	public float dashX;
	public float dashY;
	public float dashGrav;
	private float realDashY;
	public bool canDash;
	private float realJumpHeight;
	public int jumpState =0;
	private bool Dashing = false;
	public float wallJumpGrav = 0;
	public float wallBuffer;
	public float wallDist;
	public float wallJumpx;
	public float wallJumpy;
	public float wallDrag;

	private float realWallJumpy;
	public float timeToApex;
	public float timeToMidApex;
	public float timeBackToWall;
	public float wallMidJumpx;
	public float wallMidJumpy;
	private float wallJumping = 0f;
	private bool grounded;
	private bool walled = false;
	private bool midWallJump;
    public LayerMask whatIsGround;
    public LayerMask whatisWall;
  
	private float playerDirection = 0f;
   
   
   
    
    //animation shit
    private Animator anim;

   

    // Use this for initialization
    void Start () {
		charCont = this.gameObject.GetComponent<CharacterController>();
        anim = this.gameObject.GetComponent<Animator>();
		gravity = gravRate; //gravRate will be changed, so store it
		realWallJumpy = wallJumpy; //wallJumpy will be altered, so we're storing it to rest later
		realDashY = dashY; //same as above

    }
	
	// Update is called once per frame
	void Update () {
		realJumpHeight = jumpHeight; // temp for math to try to convert jumpHeight as a public variable into units of distance jumped
        GetInput(); //Get's input, temp funtion outputs vairables: right, left, jump
		manageInput(); //currently handles input and runs related functions, incorperates variables: playerDirection ; manages functions: PlayerJump()
		WallSlide (); //manages wallgrabbing using raycasts, incorperates variabels: walled, grounded, vert speed, gravity
		MovePlayer (); //manages vertical and horizontal movement of player. incorperates variables: verticleSpeed, jumpstate, grounded, verticleSpeed, playerDireciton
		DashMove();
	
		grounded = charCont.isGrounded; //check grounded state using built in character controller component .isGrounded

		if (midWallJump) { //if im midwallJump then use those variables for movement
			moveVector = new Vector2 (wallMidJumpx * playerDirection, wallMidJumpy);
			}
		else if ((wallJumping != 0f)&&(!midWallJump)) { //if I AM walljumping but NOT midwalljump then apply gravity
			wallJumpy -= wallJumpGrav * Time.deltaTime;
			moveVector = new Vector2 (wallJumpx * wallJumping * (-1), wallJumpy);
		}


		if (grounded || walled || midWallJump) { //if I am grounded, reduce my verticle speed, reset my jump state to 0
			verticleSpeed = -0.1f;
			jumpState = 0; //reset double jump

		
		} else { //if I am not grounded, let gravity take place at its normal rate
			verticleSpeed -= gravity * Time.deltaTime; //apply gravity
		}	
	}


   	void MovePlayer() //manages vertical and horizontal movement of player. incorperates variables: verticleSpeed, jumpstate, grounded, verticleSpeed, playerDireciton

    {
		if ((wallJumping == 0)&&(!walled) && (!Dash) ) { //disable movement if player is walljumping, on the wall, or dashing
			
			if(!moving){
				playerDirection = 0f; //if there is no movement coming in, reset playerDirection and stop movement
			}
			moveVector = new Vector2 (playerDirection * speed, verticleSpeed); //calculate movement in the x and y 
		}
	
		charCont.Move (moveVector * Time.deltaTime); //apply movement in the x and y

    }



	void PlayerJump(float force, float direction) 
    {
		//causes the player to jump, takes the direction that the player is facing into concideration
		//also manages double jumping.


		if (walled) { //if I am walled, return, let the wall jumps be handled in the wallSlide function
			if(direction == 0){ //if I am walled and I am not pressing a directional key, return
				
				return;
			}
			else{ //if I am walled but then press a directional key, jump and iterate my jump state forward by one
				//verticleSpeed = realJumpHeight;
				jumpState++; //every time I jump, go forward one in jump state;
			}

		}
		if (jumpState < 2) { //jumpState is reset upon hitting the ground or a wall, this value can be changed to increase the amount of jumps made in the air
			verticleSpeed = realJumpHeight; //add realJumpHeight to verticle speed which is handled in the MovePlayer() function
			jumpState++; //incriment the jumpstate forward by 1 with each jump to limit the total jumps before hitting the gorund/wall
		}
          

            
   
       
   }

    void WallSlide() //handles both wall sliding and wall jumping
    {
		//manages wallgrabbing using raycasts, incorperates variabels: walled, grounded, vert speed, gravity
		bool rightwalled = false; //we use seperate rays to check for walls to either the left or right 
		bool leftwalled = false;
		if (grounded) //if we are grounded, don't even check for walls
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
				
					if(Jump){ //if I jump while I am on the wall, set wall jumping to one, which is handled in update
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

					if(Jump){
						wallJumping = -1f; //as a reminder, wall jumping is used like a 3 variable boolean, with -1 being left, 1 being right, and 0 being stand still
						StartCoroutine ("wallJumpCD");
					}
				} else {
					moveVector = new Vector2 (wallBuffer, verticleSpeed); //same as for right
			

				}
			}
		}
    }
		

	void DashMove(){
		if (!Dash) {
			dashY = realDashY;
			return;
		}
		if (walled) {
			Dash = false;
		}
			moveVector = new Vector2 (dashX * playerDirection, dashY);
			dashY -= dashGrav * Time.deltaTime;
			canDash = false;
		

	}



	void manageInput() //currently handles input and runs related functions, incorperates variables: playerDirection ; manages functions: PlayerJump()

    {
		if ((left || right)&&(wallJumping==0f)) { //checks to see what direction the player is inputting, playerDirection is basically a 3 value boolean
			if (right) {
				playerDirection = 1f; //if we are facing right
				moving = true;
			}
       
			if (left) {
				playerDirection = -1f; //if we are facing left
				moving = true;
			}
		} 
		else {
			moving = false;
			//playerDirection = 0f; //if we are not inputting to face in any direction, subject to be removed
		}
        if (Jump) //if the player uses jump input 
        {
			PlayerJump(realJumpHeight, playerDirection); //run jump function, feeding in the force of the jump and direction of the player
        }
       
		if (Dash) {
			Dash = true;
		}


    }

    void GetInput() //gets input to be used in the manageInput function, subject to be removed once a input manager is implemeted
    {
  

		if (Input.GetKey(KeyCode.D)){
            right = true;
		
        }
        else{
            right = false;


        }
		if (Input.GetKey(KeyCode.A))
		 {
            left = true;

        }
        else {
            left = false;
		
		
        }
		if ((Input.GetKeyDown(KeyCode.Space))&&(wallJumping ==0))
        {
            Jump = true;
        }
        else {
            Jump = false;
        }

		if (Input.GetKeyDown (KeyCode.LeftShift)) {
			Dash = true;
			print ("fuck");
		} //else {
			//Dash =false;
		//}


    }

	IEnumerator wallJumpCD(){ //this is invoked when the player wall jumps
							
		wallJumpy = realWallJumpy; //wallJumpy is modified when we jump, so we reset it at the start
		playerDirection = wallJumping; //we feed walljumping into player direction so we get the direction of the wall jump
		yield return new WaitForSeconds (timeToMidApex); //this timer takes us to the mid apex of the jump, then we check if we should continue jumping or jump back to the wall
		jumpState++; //this wall jump does count towards your jump total
		if (playerDirection == 1) { 
			if (right) { //if the player jumped on a right wall and is inputting right WHILE at the apex point
				wallJumpy = realWallJumpy; //reset wall jump gravity
				midWallJump = true; //let the rest of the code know we are in a mid wall jump
				yield return new WaitForSeconds (timeBackToWall); //this timer is how long it takes for us to get to the wall
				wallJumping = 0f; //reset wall jumping to zero, since we are back against the wall now we are no longer mid wall jump


				midWallJump = false; //reset mid wall jump
				yield break;//since we jumped back towards the wall instead of continuing in an arc, break out
			}
		}
		if(playerDirection == -1){ //mirrored of above
			if (left) {
				wallJumpy = realWallJumpy;
				midWallJump = true;
				yield return new WaitForSeconds (timeBackToWall);
				wallJumping = 0f;

				midWallJump = false;
				yield break;
			}
		}
				//if we were not holding the key in the direction of the wall we were just on: 
		yield return new WaitForSeconds(timeToApex); //wait for how long to reach the total apex
		midWallJump = false; //once we reach the total apex, we are no longer in the middle of wall jumping

		wallJumping = 0f; //once we reach the total apex, we are no longer in the middle of wall jumping
		wallJumpy = realWallJumpy; //since we modified walljumpY, reset it
		gravity = gravRate; //reset gravity, don't let it calculate while we are midair
	}


}
