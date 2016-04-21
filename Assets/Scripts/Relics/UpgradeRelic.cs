using UnityEngine;
using System.Collections;
using Assets.Scripts.Components;

public enum RelicType{
	DoubleJump,
	WallJump,
	Dash,
	Slash
}

public class UpgradeRelic : MonoBehaviour {
	public RelicType UnlockWhichSkill;
	public float parTime;

	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "Player") {
			if (UnlockWhichSkill == RelicType.DoubleJump) {
				col.gameObject.GetComponent<RelicManager> ().jumpRelic = true;
			}

			if (UnlockWhichSkill == RelicType.WallJump) {
				col.gameObject.GetComponent<RelicManager> ().wallJumpRelic = true;
			}
			if (UnlockWhichSkill == RelicType.Dash) {
				col.gameObject.GetComponent<RelicManager> ().dashRelic = true;
			}
			if (UnlockWhichSkill == RelicType.Slash) {
				col.gameObject.GetComponent<RelicManager> ().slashRelic = true;
			}

			GameObject.FindGameObjectWithTag("StartPoint").GetComponent<Timer> ().StartTime (parTime);

		



		}

	}



}
