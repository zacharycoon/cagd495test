using UnityEngine;
using System.Collections;

public enum WheelType{
	Still,
	FreeSpinning,
	momentum
}

public class WheelPlatforms : MonoBehaviour {
	public WheelType WheelBehavior;

	public Transform wheel;
	public Transform[] platforms;
	public Transform[] platformPoints;
	public float rotationSpeed;
	public float JumpSpeedAdded;
	public float WheelBreakpoint;
	public float deceleration;
	public WheelPlatform[] platformComp;
	public float maxSpeed = 0.8f;

	// Use this for initialization
	void Start () {
		if (WheelBehavior == WheelType.Still) {
			for (int plat = 0; plat < platforms.Length; plat++) {
				platforms [plat].position = platformPoints [plat].position;
			}
			return;
		} else if (WheelBehavior == WheelType.FreeSpinning) {
			InvokeRepeating ("RotateWheel", 0.01f, 0.01f);
			return;
		} else if (WheelBehavior == WheelType.momentum) {
			InvokeRepeating ("RotateWheel", 0.01f, 0.01f);
			rotationSpeed = 0;
		}

		for (int plat = 0; plat < platforms.Length; plat++) {
			Debug.Log (plat);
			platformComp [plat] = platforms [plat].GetComponent<WheelPlatform> ();

		}

		//
	}
	
	void Update(){
		//wheel.Rotate (Vector3.up * rotationSpeed);

	}


	void RotateWheel () {
		//platforms [plat].position = platformPoints [plat].position;
		wheel.Rotate (Vector3.up * rotationSpeed);
		ChangePlatType ();
	
		if (rotationSpeed > 0) {
			rotationSpeed *= deceleration;
		}
		if (rotationSpeed < 0) {
			rotationSpeed *= deceleration;
		}

		if (Mathf.Abs (rotationSpeed) < 0.011) {
			rotationSpeed = 0;
		}

		if (Mathf.Abs (rotationSpeed) > maxSpeed) {
			if (rotationSpeed < 0) {
				rotationSpeed =  (maxSpeed * -1);
			} else {
				rotationSpeed = maxSpeed;
			}
		}

	}

	void ChangePlatType(){
		for (int plat = 0; plat < platforms.Length; plat++) {
			platforms [plat].position = platformPoints [plat].position;
		}

	}


}
