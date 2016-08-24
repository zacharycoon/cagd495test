using UnityEngine;
using System.Collections;

public class _Camera : MonoBehaviour {

	Transform player;
	public float yDist;
	public float zDist;
	public float smoothing;

	public Vector3 playerScreenPos;

	Camera thisCam;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		thisCam = GetComponent<Camera> ();
	}
	
	// Update is called once per frame
	void Update () {
		playerScreenPos = thisCam.WorldToScreenPoint(player.position);
		playerScreenPos.x = (playerScreenPos.x / Screen.width) - 0.5f;
		playerScreenPos.y = playerScreenPos.y / Screen.height;
	
		if (playerScreenPos.x < 0.1f) {
			if (playerScreenPos.x < 0.2f) {
				transform.Translate (Vector3.right * Time.deltaTime * smoothing * -5f);
			}
			transform.Translate (Vector3.right * Time.deltaTime * smoothing * -1f);
		}
		if (playerScreenPos.x > -0.1f) {
			if (playerScreenPos.x > -0.2f) {
				transform.Translate (Vector3.right * Time.deltaTime * smoothing * 5);
			}
			transform.Translate (Vector3.right * Time.deltaTime * smoothing);
		}
	
	
	
	}
}
