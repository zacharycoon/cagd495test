using UnityEngine;
using System.Collections;

public class Death : MonoBehaviour {
	private GameObject spawner;
	public float deathTime;
	public float waitToInput;
	static public bool dying = false;
	private Vector3 currentLoc;
	// Use this for initialization
	void Start () {
		spawner = GameObject.FindGameObjectWithTag ("Spawn"); 
		transform.position = new Vector3 (spawner.transform.position.x, spawner.transform.position.y, this.transform.position.z);

		dying = false;

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.R)) { //This is just a placeholder because we don't have a death condition right now
			StartCoroutine("Respawn"); 

		}

		if (Input.GetKeyDown (KeyCode.T)){ //set the spawner transform
			spawner.transform.position = new Vector3 ( transform.position.x, transform.position.y, transform.position.z);

		}
	}
	IEnumerator Respawn(){ //manages respawning 
		currentLoc = new Vector3 (transform.position.x, transform.position.y, transform.position.z); //get the player position
		dying = true; //set death to true, this stops the update loop of the main movement controller
		yield return new WaitForSeconds (deathTime); //wait for however long
		dying = false; //set death back to false, letting the player control themselves again.
		transform.position = new Vector3 (spawner.transform.position.x, spawner.transform.position.y, this.transform.position.z); 
						//move the player to the spawner location
		yield return new WaitForSeconds (waitToInput); //how long after respawner should the player wait before they can input again
														//currently not implimented 

	}

}
