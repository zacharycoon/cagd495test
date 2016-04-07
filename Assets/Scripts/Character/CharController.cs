using System;
using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Character
{

	class CharController : Components.ControllerBase
	{
		private static CharController _instance;
		public static CharController Instance{
			get{
				_instance = _instance ?? GameObject.FindObjectOfType<CharController> ();
				if (_instance == null) {
					Debug.LogWarning ("No CharController in scene but an object is attempted to access it");
				}
				return _instance;
			}
		}



	float dashPhase;
	float wallJumping =0;
	float playerDirection =0;
	Vector2 moveVector = Vector2.zero;
	bool walled, moving;
	float speed = 18f;
	float verticleSpeed =0f;
	CharacterController charCont;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		MovePlayer ();


	}



	void MovePlayer() //manages vertical and horizontal movement of player. incorperates variables: verticleSpeed, jumpstate, grounded, verticleSpeed, playerDireciton

	{
		if ((wallJumping == 0)&&(!walled) ) { //disable movement if player is walljumping, on the wall, or dashing

			if(!moving &&(dashPhase != 2f)){
				playerDirection = 0f; //if there is no movement coming in, reset playerDirection and stop movement
			}
			moveVector = new Vector2 (playerDirection * speed, verticleSpeed); //calculate movement in the x and y 
		}

		charCont.Move (moveVector * Time.deltaTime); //apply movement in the x and y

	}

}
}