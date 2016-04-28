using UnityEngine;
using System.Collections;

public class WheelPlatforms : MonoBehaviour {

	public Transform wheel;
	public Transform[] platforms;
	public Transform[] platformPoints;
	public float rotationSpeed;


	// Use this for initialization
	void Start () {
		
		InvokeRepeating ("RotateWheel", 0.01f, 0.01f);
	}
	
	// Update is called once per frame
	void RotateWheel () {
		wheel.Rotate (Vector3.up * rotationSpeed);

		for (int plat = 0; plat < platforms.Length; plat++) {
			platforms [plat].position = platformPoints [plat].position;
		}


	}
}
