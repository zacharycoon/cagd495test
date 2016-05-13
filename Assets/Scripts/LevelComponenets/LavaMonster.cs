using UnityEngine;
using System.Collections;

public class LavaMonster : MonoBehaviour {


	public float lavaDuration;
	public float waitDuration;

	//public float moveHeight;

	Vector3 startPos;
	Vector3 targetPos;
	Vector3 endPos;
	public float speed;
	public float moveUpHeight = 3f;
	//public Transform endPosTrans;
	bool canStartLava = false;
	public bool spew;
	ParticleSystem  mySystem;
	public BoxCollider playerBlocker;

	// Use this for initialization
	void Start () {
		playerBlocker.enabled = false;
		mySystem = gameObject.transform.GetComponentInChildren<ParticleSystem> ();
		mySystem.Stop ();
		startPos = transform.position;
		endPos = startPos + new Vector3 (0, moveUpHeight, 0);
		targetPos = startPos;
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position != targetPos) {
			transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
		}

		if (targetPos == endPos && transform.position == endPos && canStartLava) {
			StartCoroutine("SpewLava");
			canStartLava = false;
		}

	}
	IEnumerator SpewLava(){
		spew = true;
		playerBlocker.enabled = true;
		mySystem.Play();
		yield return new WaitForSeconds (lavaDuration);
		spew = false;
		playerBlocker.enabled = false;
		mySystem.Stop ();
		yield return new WaitForSeconds (waitDuration);
		if (transform.position == endPos) {
			StartCoroutine("SpewLava");
		}
	}
	

	void Awaken(){
		targetPos = endPos;
			canStartLava = true;

	}

	void Sleep(){
		targetPos = startPos;
		StopAllCoroutines ();
		playerBlocker.enabled = false;
		mySystem.Stop ();
	}

	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "Player") {

			Awaken();
		}

	}

	void OnTriggerExit(Collider col){
		if (col.gameObject.tag == "Player") {
			Sleep();
		}

	}


}
