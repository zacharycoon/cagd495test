using UnityEngine;
using System.Collections;




public class SetSection : MonoBehaviour {
	public SectionType RightSection;
	public SectionType LeftSection;


	// Use this for initialization
	void Start () {
		foreach (Transform child in transform) {
			if(child.name == "Right"){
				child.GetComponent<ChangeCamera>().upComingSection = RightSection;
			}
			if(child.name == "Left"){
				child.GetComponent<ChangeCamera>().upComingSection = LeftSection;
			}

		}


	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
