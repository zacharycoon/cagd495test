using UnityEngine;
using System.Collections;

public class BatNest : MonoBehaviour {

	public GameObject bats;
	[Tooltip("set startposition as the first target and end position as last target")]
	public Transform[] Targets;
	 public float speed;
	 public float numOfBats;
	public float numOfFlocks;
	public float flockDelay;
	 public float spawnDelay;
	 //Transform[] BatTrans;
	// Use this for initialization
	void Start () {
		StartCoroutine ("spawnBats");

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator spawnBats(){
		for (int flock = 0; flock < numOfFlocks; flock++) {
			for (int bat = 0; bat < numOfBats; bat++) {
				GameObject newBat = Instantiate (bats, Targets [0].position, Quaternion.identity) as GameObject;
				//BatTrans[bat] = newBat.transform;
				newBat.GetComponent<BatBehavior> ().locations = Targets;
				yield return new WaitForSeconds (spawnDelay);
			}
			yield return new WaitForSeconds (flockDelay);
		}

	}


}
