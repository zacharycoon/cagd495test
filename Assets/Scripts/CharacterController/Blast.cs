using UnityEngine;
using System.Collections;

public class Blast : MonoBehaviour {

	public bool charging;
	public float threshold;
	public float currentCharge;
	private float blastDirection;
	public Rigidbody projectile;
	public float speed;
	// Use this for initialization
	void Start () {
		currentCharge = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		GetInput ();
	}

	void FixedUpdate (){
	/*	if (Input.GetMouseButton (0)) {
			currentCharge++;
		}
		if (!(Input.GetMouseButton (0))) {

			if(currentCharge < threshold){
				currentCharge = 0f;

			}
			if(currentCharge > threshold){
				currentCharge = 0f;
				Debug.Log ("blast");
			}
		
			
		}
	*/

	}

	void GetInput(){
		//if (Input.GetMouseButton (0)) {
		if(Input.GetButton("Fire3")){
			currentCharge = (currentCharge + (1* Time.deltaTime));
		}
		if(Input.GetButtonUp("Fire3")){
		//if (Input.GetMouseButtonUp (0)) {
			if(currentCharge < threshold){
				currentCharge = 0f;
			}
			if(currentCharge > threshold){
				currentCharge =0f;
				if(Movement.facingright){
					blastDirection = 1f;
				}
				else{
					blastDirection = -1f;
				}
				BlastAttack(blastDirection);
			}

		}

	}

	void BlastAttack(float direction){
		Rigidbody BlastProj;
		BlastProj = Instantiate(projectile, transform.position, Quaternion.identity) as Rigidbody;
		BlastProj.AddForce(transform.right * speed * blastDirection);

	}


}
