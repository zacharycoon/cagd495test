using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	//the camera phase decides the behavior of the camera, 1 is free, 2 is horizontal, 3 is vertical, and 4 is locked to a position
	static public float camPhase = 1;
	static public bool getTrans = false;     
	public float smoothRate;

	 Transform player;

	private Transform thisTransform;
	private Vector2 velocity;
	public float camdistance;
	private Vector2 newPos2D;
	public float yshift;

	static public float xPosLockedAt;
	static public float yPosLockedAt;

	// Use this for initialization
	void Start () {
		thisTransform = transform; 
		velocity = new Vector2 (0.5f, 0.5f); 
		player = GameObject.FindGameObjectWithTag("Player").transform; 
	}
	
	//Note: the camPhase int is handled in the Change Camera script
	void Update () {
		if (getTrans) {
			yPosLockedAt = player.position.y;
			xPosLockedAt = player.position.x;
			getTrans = false;
		}


		 //all of this is just temporary, so we can use the keyboard to change the camera type if we do not
		//have the triggers set in game yet
		if (Input.GetKeyDown (KeyCode.Alpha1)) { // unlock both
			camPhase = 1;
		}
		if (Input.GetKeyDown (KeyCode.Alpha2)) { //lock y pos
			camPhase = 2;
			yPosLockedAt = player.position.y;
		}
		if (Input.GetKeyDown (KeyCode.Alpha3)) { // lock x pos
			camPhase = 3;
			xPosLockedAt = player.position.x;
		}	
		if (Input.GetKeyDown (KeyCode.Alpha4)) { // lock x pos and y pos
			camPhase = 4;
			yPosLockedAt = player.position.y;
			xPosLockedAt = player.position.x;
		}	




		
		if ((float.IsNaN (newPos2D.x)) || (float.IsNaN (newPos2D.y))) {
			transform.position = Vector3.Slerp (transform.position, player.position, 1);
			
		}
		newPos2D = Vector2.zero;

		if (camPhase == 1) {
			newPos2D.x = Mathf.SmoothDamp (thisTransform.position.x, player.position.x, ref velocity.x, smoothRate);
			newPos2D.y = Mathf.SmoothDamp (thisTransform.position.y, player.position.y + yshift, ref velocity.y, smoothRate);
			//newPos2D.y = Mathf.SmoothDamp(thisTransform.position.y, player.position.y, ref velocity.y, smoothRate);
		}

		if (camPhase == 2) {
			newPos2D.x = Mathf.SmoothDamp (thisTransform.position.x, player.position.x, ref velocity.x, smoothRate);
			newPos2D.y = Mathf.SmoothDamp (thisTransform.position.y, yPosLockedAt, ref velocity.y, smoothRate);
			//newPos2D.y = Mathf.SmoothDamp(thisTransform.position.y, player.position.y, ref velocity.y, smoothRate);
		}
		if (camPhase == 3) {
			newPos2D.x = Mathf.SmoothDamp (thisTransform.position.x, xPosLockedAt, ref velocity.x, smoothRate);
			newPos2D.y = Mathf.SmoothDamp (thisTransform.position.y, player.position.y + yshift, ref velocity.y, smoothRate);
			//newPos2D.y = Mathf.SmoothDamp(thisTransform.position.y, player.position.y, ref velocity.y, smoothRate);
		}
		if (camPhase == 4) {
			newPos2D.x = Mathf.SmoothDamp (thisTransform.position.x, xPosLockedAt, ref velocity.x, smoothRate);
			newPos2D.y = Mathf.SmoothDamp (thisTransform.position.y, yPosLockedAt, ref velocity.y, smoothRate);
			//newPos2D.y = Mathf.SmoothDamp(thisTransform.position.y, player.position.y, ref velocity.y, smoothRate);
		}




		Vector3 newPos = new Vector3 (newPos2D.x, newPos2D.y, camdistance);
		transform.position = Vector3.Slerp (transform.position, newPos, 1);
	




	}
}
