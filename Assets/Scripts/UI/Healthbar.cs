using UnityEngine;
using System.Collections;
using Assets.Scripts.Components;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour {
	Health health;
	public Image HealthFillBG;
	public Image HealthFill;

	// Use this for initialization
	void Start () {
		health = GameObject.FindGameObjectWithTag ("Player").GetComponent<Health> ();
		HealthFill.fillAmount = 0f;
		HealthFillBG.fillAmount = 0f;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float myhp = health.CurStuff;

		HealthFill.fillAmount = myhp;
		if (HealthFill.fillAmount > HealthFillBG.fillAmount) {
			HealthFillBG.fillAmount = HealthFill.fillAmount;
		}

	}
}
