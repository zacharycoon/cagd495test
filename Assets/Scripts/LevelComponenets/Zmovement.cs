using UnityEngine;
using System.Collections;

public class Zmovement : MonoBehaviour {

	[Tooltip("delay before it starts moving")]
	public float StartDelay;

	[Tooltip("The amount of time that the platform moves")]
	public float movementTime;

	[Tooltip("The amount of time that the platform waits before it retracts, set to 0 for it to go in and out immediately")]
	public float waitTime;
	

	//[Tooltip("this speed value is being refrenced when the game runs, so editing the value during play won't work, if you want to fool around with values you need to do it outside of play mode")]
	public float speed;

	float currentspeed =0;

	Vector3 startpos;
	 bool move = true;
	bool changeDir;


	// Use this for initialization
	void Start () {
		if (speed < 0) {
		//	speed *= -1;
		}
		move = true;
		startpos = this.gameObject.transform.position;
		Invoke ("GetGoing", StartDelay);
	}
	
	// Update is called once per frame
	void Update () {
		if (move && transform.position.z <= startpos.z) {
			
			Vector3 moveVector = new Vector3 (0, 0, currentspeed);
			transform.Translate (moveVector * Time.deltaTime);
		}
	}
		void GetGoing(){
		//transform.position = startpos;
		StartCoroutine ("moveAround"); 
		currentspeed = speed;
		}

	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "Player") {
		//	currentspeed = 0;
		//	StopAllCoroutines ();
			move = false;
		//	Invoke ("GetGoing", 1f);
		}

	}

	void OnTriggerExit(Collider col){
		if (col.gameObject.tag == "Player") {
			move = true;
		}

	}


	 IEnumerator moveAround(){
		yield return new WaitForSeconds (movementTime);
		currentspeed = 0f;
		yield return new WaitForSeconds ( waitTime);
		speed *= -1f;
		currentspeed = speed;
		changeDir = !changeDir;
		if (!changeDir) {
			transform.position = startpos;
		}
		StartCoroutine ("moveAround"); 
		yield break;
	}
}
