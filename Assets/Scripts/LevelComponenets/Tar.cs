using UnityEngine;
using System.Collections;
using Assets.Scripts.Components;

public class Tar : MonoBehaviour {
	public float tarSpeed;


	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "Player") {
			PlayerMovement.speedModifier = tarSpeed;
		
		}
	}

	void OnTriggerExit(Collider col){
		if (col.gameObject.tag == "Player") {
			PlayerMovement.speedModifier = 1f;
		}

	}
	



}
