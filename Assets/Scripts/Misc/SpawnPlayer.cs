using UnityEngine;
using System.Collections;

public class SpawnPlayer : MonoBehaviour {

	public GameObject player;
	// Use this for initialization
	void Start () {
		//Transform spawnPoint = GameObject.FindGameObjectWithTag ("StartPoint").transform;

		Instantiate (player, transform.position, Quaternion.identity);

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
