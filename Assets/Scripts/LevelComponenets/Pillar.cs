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

	}
	
	void OnTriggerEnter(Collider col){
		if(col.gameObject.tag == ("Player")){
			InvokeRepeating("Fall", 0.1f,0.1f);
		}
	}

	void Fall () {
		//pillarRoot.localEulerAngles= new Vector3 (0f, 0f, fallAngle);
		if (transform.rotation.z < Mathf.Abs (fallAngle)) {
			pillarRoot.Rotate (Vector3.forward * fallspeed);
		}
	}
}
