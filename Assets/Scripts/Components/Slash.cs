using UnityEngine;
using System.Collections;


namespace Assets.Scripts.Components
{
	

	public class Slash: CustomComponentBase {
		public float slashDistance = 3f;
		public static bool canSlash = false;

		public void SlashAttack(float Dir){

			if (!canSlash) {
				return;
			}

			RaycastHit hit;
			Vector3 RayDir = new Vector3 (Dir, 0f, 0f);

			if (Physics.Raycast (transform.position, RayDir, out hit, slashDistance))
			{
				if (hit.transform.tag == "Destructable") {
					hit.transform.gameObject.GetComponent<CanDestroy> ().hp -= 1f;
				}
			}
		}

	}
}