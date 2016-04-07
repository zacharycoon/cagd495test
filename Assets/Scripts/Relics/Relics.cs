using UnityEngine;
using System.Collections;

public class Relics : MonoBehaviour {
	static public int currentRelics;
	// Use this for initialization
	public GameObject myChild;
	private bool canCollect = true;
	void Awake(){
		//myChild = this.gameObject.transform.GetChild (0);
	}

	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "Player") {
			if (canCollect) {
				RelicGUI.RelicsCollected += 1;
			}
			canCollect = false;
			myChild.SetActive (false);
		}

	}


}
