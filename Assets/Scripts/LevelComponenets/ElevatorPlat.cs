using UnityEngine;
using System.Collections;

public class ElevatorPlat : MonoBehaviour {
	Vector3 startPos;
	public float grav;
	public float JumpForce;
	float vertSpeed;
	public float HowOften;
	bool Go = false;
	bool addForce;
	Vector3 moveVec;
	// Use this for initialization
	void Start () {
		startPos = transform.position;
		InvokeRepeating ("goUp", HowOften, HowOften);
	}
	
	// Update is called once per frame
	void Update () {

		if (addForce) {
			vertSpeed = JumpForce;

			addForce = false;
		}
		if (transform.position.y >= startPos.y) {
			transform.Translate(moveVec * Time.deltaTime);
			vertSpeed -= grav*Time.deltaTime;
			moveVec.y = vertSpeed;
		}
		if (transform.position.y < startPos.y) {
			transform.position = startPos;
		}




	}
	void goUp(){
		addForce = true;
	}

}
