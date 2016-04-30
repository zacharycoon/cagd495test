using UnityEngine;
using System.Collections;

public class Pillar : MonoBehaviour {
	public Transform pillarRoot;
	public Transform platform;
	public Transform platformPoint;
	public float fallspeed;
	public float fallAngle;
	// Use this for initialization
	void Start () {
		platform.position = platformPoint.position;

		if (fallAngle < 0) {
		//	fallAngle = 360 - fallAngle;
		}

	}
	
	void OnTriggerEnter(Collider col){
		if(col.gameObject.tag == ("Player")){
			InvokeRepeating("Fall", 0.01f,0.01f);
		}
	}

	void Fall () {
		//pillarRoot.localEulerAngles= new Vector3 (0f, 0f, fallAngle);
		if (Mathf.Abs(pillarRoot.localEulerAngles.z) <= Mathf.Abs(fallAngle)) {
			
			platform.position = platformPoint.position;
			Debug.Log (Mathf.Abs (fallAngle));
			Debug.Log (pillarRoot.localEulerAngles.z);
			pillarRoot.Rotate (Vector3.forward * fallspeed);
		}
	}
}
