using UnityEngine;
using System.Collections;

public class m2Collider : EventManager {

	//void OnCollisionEnter(Collision thisColide) {
	void OnTriggerEnter(Collider thisColide){
		Debug.Log("in here");
		if(thisColide.gameObject.tag == "room_structure") {
			Debug.Log("TEST");
			m2.transform.Translate(new Vector3(0,0,0));
		}
	}
}


