using UnityEngine;
using System.Collections;

public enum SectionType{
	freeSection,
	horzSection,
	vertSection,
	lockedSection
}

public class ChangeCamera : MonoBehaviour {
	//this script can be placed on triggers to set what type of camera should be used for this zone
//	public bool freeSection;
//	public bool horzSection;
//	public bool vertSection;
//	public bool lockedSection;

	public SectionType upComingSection;

	public bool setSpawnPoint;

	private GameObject spawner;
	private string ObjName;
	private float currentSection;

	// Use this for initialization
	void Start () {
		spawner = GameObject.FindGameObjectWithTag ("Spawn");

		//this whole section just makes sure that level design did not select two of the same sections
	

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col){
	

		if(col.gameObject.tag == ("Player")){ //when the player enters the trigger 
			CameraFollow.xPosLockedAt = transform.position.x; //send the x and y of the trigger to Camera Follow
			CameraFollow.yPosLockedAt = transform.position.y;

			if(setSpawnPoint){
				spawner.transform.position = this.transform.position; //if we also want this trigger to reset the spawn point
			}															//then bring the spawn point here

			//depending on the section type that is specified in the inspector, send the Camera Follow script



			switch (upComingSection) {
			case SectionType.freeSection:
				CameraFollow.camPhase = 1;
				break;
				
			case SectionType.horzSection:
				CameraFollow.camPhase = 2;
				break;
				
			case SectionType.vertSection:
				CameraFollow.camPhase = 3;
				break;
				
			case SectionType.lockedSection:
				CameraFollow.camPhase = 4;
				break;
				
			}




		}
	}


}
