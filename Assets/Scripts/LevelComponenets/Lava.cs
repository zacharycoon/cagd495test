using UnityEngine;
using System.Collections;
using Assets.Scripts.Components;

public class Lava : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "Player") {
			col.GetComponent<Health> ().Death ();
		}
	}

}
