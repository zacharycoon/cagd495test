using UnityEngine;
using System.Collections;
using Assets.Scripts.Components;
using UnityEngine.UI;

public class AbilityUIManager : MonoBehaviour {

	RelicManager manager;
	Image Slash;
	Image Jump;
	Image WallJump;
	Image Dash;
	// Use this for initialization
	void Start () {
		manager = GameObject.FindGameObjectWithTag ("Player").GetComponent<RelicManager> ();

		Slash = transform.FindChild ("Canvas/Base/Slash/Image").gameObject.GetComponent<Image>();
		Jump = transform.FindChild ("Canvas/Base/Jump/Image").gameObject.GetComponent<Image>();
		WallJump = transform.FindChild ("Canvas/Base/WallJump/Image").gameObject.GetComponent<Image>();
		Dash = transform.FindChild ("Canvas/Base/Dash/Image").gameObject.GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
		if (manager.slashRelic) {
			Slash.enabled = true;
		} else {
			Slash.enabled = false;
		}
		if (manager.jumpRelic) {
			Jump.enabled = true;
		} else {
			Jump.enabled = false;
		}
		if (manager.wallJumpRelic) {
			WallJump.enabled = true;
		} else {
			WallJump.enabled = false;
		}
		if (manager.dashRelic) {
			Dash.enabled = true;
		} else {
			Dash.enabled = false;
		}

	}
}
