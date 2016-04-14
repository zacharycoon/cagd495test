using UnityEngine;
using System.Collections;
using Assets.Scripts.Components;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour {
	Health health;
	public Image HealthFill;

	// Use this for initialization
	void Start () {
		health = GameObject.FindGameObjectWithTag ("Player").GetComponent<Health> ();
	}
	
	// Update is called once per frame
	void Update () {
		float myhp = health.CurHealth;
		HealthFill.fillAmount = myhp* 0.01f;

	}
}
