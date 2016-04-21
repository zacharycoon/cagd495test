using UnityEngine;
using System.Collections;



public class Timer : MonoBehaviour {



	public float TimeRemaining;
	bool countingDown = false;

	// Use this for initialization
	public void StartTime (float parTime) {
		countingDown = true;
		TimeRemaining = parTime;
		InvokeRepeating ("CountDown", 0.1f, 0.1f);
	}
	
	// Update is called once per frame
	public void CountDown () {
		TimeRemaining -= 0.1f;

	}

	void OnTriggerEnter(Collider col){
		if ((col.gameObject.tag == "Player")&&(countingDown)) {
			CancelInvoke ("CountDown");
		
		}
	

	}

}
