using UnityEngine;
using System.Collections;

public class MoltenCrab : MonoBehaviour {

	bool grounded;
	public float speed = 155;
	public float randomTimeCheck;
	public float changeDirChance;
	public float damage;
	float forward =1;
	public float wallDist =0.6f;
	float verticleSpeed;
	CharacterController mycont;
	float grav = 10;

	// Use this for initialization
	void Start () {
		mycont = transform.gameObject.AddComponent<CharacterController> ();
		mycont.height = 1f;

		if (randomTimeCheck > 0) {
			InvokeRepeating("ChangeDir", randomTimeCheck, randomTimeCheck);
		}

	}
	
	// Update is called once per frame
	void Update () {
		grounded = mycont.isGrounded;

		if (grounded) {
			verticleSpeed = 0;
		} else {
			verticleSpeed -= grav * Time.deltaTime;
		}

		Vector3 moveVector = new Vector3 (speed * forward, verticleSpeed, 0f);
		mycont.SimpleMove (moveVector * Time.deltaTime);

		if (Physics.Raycast (transform.position, Vector3.right * forward, wallDist)) {
			forward *= -1f;
		}

	}
	void ChangeDir(){
		float chance = Random.Range (0, 100);
		if (chance < changeDirChance) {
			forward *= -1f;
		}

	}

}
