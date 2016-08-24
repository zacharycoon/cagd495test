using UnityEngine;
using System.Collections;

public class CanDestroy : MonoBehaviour {
	public float hp;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (hp < 1) {
			Destroy (this.gameObject);
		}
	}
}
