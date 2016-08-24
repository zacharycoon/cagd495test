using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Scripts.Components
{


	public class Health: CustomComponentBase {
		public float CurStuff= 0f;
		float pointValue;
		List<GameObject> collectedLoot;

		public void TotalStuff(){
			GameObject[] tempList = GameObject.FindGameObjectsWithTag ("Loot");
			float maxStuff = tempList.Length;
			pointValue = 1f / maxStuff;
			collectedLoot = new List<GameObject> ();

		}

		void OnTriggerEnter(Collider col){
			if (col.gameObject.tag == "Loot") {
				CurStuff += pointValue;
				collectedLoot.Add (col.gameObject);
				col.gameObject.SetActive (false);
			}

		}

		public void TakeDamage(float damage){
			for (int relicsLost = 0; relicsLost < damage; relicsLost++) {
				GameObject turnOn = collectedLoot [Random.Range (0, collectedLoot.Count)];
				collectedLoot.Remove (turnOn);
				turnOn.SetActive (true);
				CurStuff -= pointValue;
			}

		}

		public void Death(){
			Debug.Log ("death things");
		}


	}
}