using UnityEngine;
using System.Collections;
using Assets.Scripts.Components;

public class FragilePillar : MonoBehaviour {
	Transform PillarBotHalf;
	BoxCollider CrackTrigger;
	public bool destroy;

	// Use this for initialization
	void Start () {
		PillarBotHalf = this.gameObject.transform.FindChild ("BotHalf");
		CrackTrigger = this.gameObject.GetComponent<BoxCollider> ();

	}
	

	void OnTriggerEnter(Collider col){

		if (col.gameObject.tag == "Player") {
			Debug.Log ("something");
			destroy = col.gameObject.GetComponent<Jumping> ().JustwallJumped;
			}
		}


	void Update(){
		if (destroy) {
			Debug.Log ("fuck yea");
		}

	}

}
