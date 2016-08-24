using UnityEngine;
using System.Collections;

public enum platType{
	right,
	left, 
	none

}


public class WheelPlatform : MonoBehaviour {

	public WheelPlatforms myWheel;
	public platType myType;

	float breakPoint;
	float addspeed;
	// Use this for initialization
	void Start () {
		myWheel = this.gameObject.transform.parent.GetComponent<WheelPlatforms> ();
		addspeed = myWheel.JumpSpeedAdded;
		breakPoint = myWheel.WheelBreakpoint;
	}
	
	void FixedUpdate(){
		if (Mathf.Abs (this.gameObject.transform.localPosition.x) < breakPoint) {
			myType = platType.none;
		}

		if (this.gameObject.transform.localPosition.x > breakPoint) {
			myType = platType.right;
		}

		if (this.gameObject.transform.localPosition.x < (breakPoint * -1)) {
			myType = platType.left;
		}


	}

	void OnTriggerEnter(Collider col){

		if (col.gameObject.tag == "Player") {
			if (myType == platType.none) {
				return;
			}
			if (myType == platType.right) {
				print ("right");
				myWheel.rotationSpeed -= addspeed;
				return;
			
			}

			if (myType == platType.left) {
				print ("left");
				myWheel.rotationSpeed += addspeed;
				return;
			}





		}


	}


}
