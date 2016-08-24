using UnityEngine;
using System.Collections;

public class BatBehavior : MonoBehaviour {
	public Transform[] locations;
	Transform targetTrans;
	public float direction = 1f;
	public float speed;
	public int targetIndex = 1;
	public float yRange;
	public float speedShift;

	Vector3 YFactor;
	// Use this for initialization
	void Start () {
		targetTrans = locations [targetIndex];
		YFactor = new Vector3 ( Random.Range ((yRange * -1f), yRange), Random.Range ((yRange * -1f), yRange),  0);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Vector3.MoveTowards (transform.position, locations[targetIndex].position + YFactor, speed  * Time.deltaTime);
	


		if (transform.position == (locations[targetIndex].position + YFactor) && direction > 0) {
			if (targetIndex == locations.Length - 1) {
				direction = -1;
			} else {
				targetIndex++;
				YFactor = new Vector3 ( Random.Range ((yRange * -1f), yRange), Random.Range ((yRange * -1f), yRange), 0);
			}
		}

		if (transform.position == (locations[targetIndex].position + YFactor) && direction < 0) {
			if (targetIndex == 0) {
				direction = 1;
			} else {
				targetIndex--;
				YFactor = new Vector3 ( Random.Range ((yRange * -1f), yRange), Random.Range ((yRange * -1f), yRange), 0);
			}
		}


	}

	void OnTriggerStay(Collider col){
		if (col.gameObject.tag == "Bat") {
			Debug.Log("something");
			speed += Random.Range((speedShift *-1), speedShift);
			//YFactor += new Vector3 (0, Random.Range ((yRange * -1f), yRange), 0);

		}
	}

	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "Player") {
			Debug.Log("DEAL DAMAGE");
		}
	}


}
