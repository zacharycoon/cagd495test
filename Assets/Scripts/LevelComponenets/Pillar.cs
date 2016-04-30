using UnityEngine;
using System.Collections;

public class Pillar : MonoBehaviour {
	public Transform pillarRoot;
	public Transform platform;
	public Transform platformPoint;
	public float fallspeed;
	public float fallAngle;

	BoxCollider myTrigger;
	// Use this for initialization
	void Start () {
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

	void Fall () {
		//pillarRoot.localEulerAngles= new Vector3 (0f, 0f, fallAngle);

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
