using UnityEngine;
using System.Collections;

public class RelicGUI : MonoBehaviour {

	static public int RelicsCollected;
	private GUIText thistext;


	// Use this for initialization
	void Start () {
		thistext = this.gameObject.GetComponent<GUIText> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		thistext.text = "Relics Collected: " + RelicsCollected; 


	}
}
