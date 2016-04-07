using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	public float timeToDeath;
	// Use this for initialization
	void Awake() {
		StartCoroutine ("KillMe");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator KillMe(){
		yield return new WaitForSeconds (timeToDeath);
		Destroy (this.gameObject);
	}

	void OnTriggerEnter(Collider col){
		Destroy (this.gameObject);
	}


}
