using UnityEngine;
using System.Collections;
using Assets.Scripts.Components;

public class FragilePillar : MonoBehaviour {
	Transform PillarBotHalf;
	BoxCollider CrackTrigger;
	public Jumping playerjumping;
	bool canDestroy;

	// Use this for initialization
	void Start () {
		PillarBotHalf = this.gameObject.transform.FindChild ("BotHalf");
		CrackTrigger = this.gameObject.GetComponent<BoxCollider> ();

	}
	

	void OnTriggerEnter(Collider col){

		if (col.gameObject.tag == "Player") {
			canDestroy = true;
		
			playerjumping = col.gameObject.GetComponent<Jumping> ();
			}
		}

	void OnTriggerExit(Collider col){
		canDestroy = false;
	}

	void Update(){
		if (canDestroy&& playerjumping.JustwallJumped) {
			PillarBotHalf.GetComponent<Rigidbody> ().isKinematic = false;
		}

	}

}
