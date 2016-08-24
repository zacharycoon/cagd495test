using UnityEngine;
using System.Collections;

public class BasicAnim : MonoBehaviour {

	private Animator thisAnim;
	// Use this for initialization
	void Start () {
		thisAnim = this.gameObject.GetComponent<Animator> ();
		thisAnim.SetBool ("FacingRight", Movement.facingright);

	}
	
	// Update is called once per frame
	void Update () {
		thisAnim.SetBool ("FacingRight", Movement.facingright);

		thisAnim.SetFloat ("Moving", Movement.playerDirection);

	}
}
