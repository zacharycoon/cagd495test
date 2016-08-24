using UnityEngine;
using System.Collections;

public class RollingPillar : MonoBehaviour {
	public Transform StartPoint, endpoint;
	public RollingPillarBehavior[] myChildren;
	public float speed;

	void Start(){
		myChildren = transform.gameObject.GetComponentsInChildren<RollingPillarBehavior> ();

	}

	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "Player") {
			for (int childCol = 0; childCol < myChildren.Length; childCol++) {
			//	Debug.Log ("fuck " + childCol);
			//	myChildren [childCol].isKinematic = false;
				myChildren[childCol].StartRolling = true;


			}

		}

	}

}
