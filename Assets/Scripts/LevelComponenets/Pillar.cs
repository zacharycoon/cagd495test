using UnityEngine;
using System.Collections;

public class Pillar : MonoBehaviour {
	public Transform pillarRoot;
	public Transform platform;
	public Transform platformPoint;
	float fallspeed;
	public float fallAngle;


	[Tooltip("be careful while adjusting these values, the speed increase is multaplicative with starting speed, so as you increase starting speed, make sure to dramatically decrease speedIncrease, down to 1.01 - 1.015ish")]
	public bool ___Hover_Here_For_Tool_Tip________;
	public float startingSpeed = 0.3f;

	public float speedIncrease = 1.02f;

	BoxCollider myTrigger;
	// Use this for initialization
	void Start () {
		fallspeed = startingSpeed;
		platform.position = platformPoint.position;
		myTrigger = this.gameObject.GetComponent<BoxCollider> ();
		if (fallAngle < 0) {
			fallAngle = 360 + fallAngle;


		}

	}
	
	void OnTriggerEnter(Collider col){


		if(col.gameObject.tag == ("Player")){
			InvokeRepeating("Fall", 0.01f,0.01f);
			Destroy (myTrigger);	
		
		}
	}


	void FixedUpdate(){
		
	}

	void Fall () {
		//pillarRoot.localEulerAngles= new Vector3 (0f, 0f, fallAngle);
		fallspeed *= speedIncrease;

		if (fallAngle < 180) {
			if (pillarRoot.localEulerAngles.z <= fallAngle) {
			
				platform.position = platformPoint.position;
				pillarRoot.Rotate (Vector3.forward * fallspeed);
			}
		}

		if (fallAngle > 180) {
			
			if (pillarRoot.localEulerAngles.z >= fallAngle || pillarRoot.localEulerAngles.z == 0) {

				platform.position = platformPoint.position;
				pillarRoot.Rotate (Vector3.forward * fallspeed);

			} 
		}



	}
}
