using UnityEngine;
using System.Collections;

public class roomSwitcher : EventManager {
	protected override void Start () {
		base.Start ();
	}
	
	protected override void Update () {
		base.Update ();
	}

	void OnTriggerEnter(Collider thisObject) {
		//base.displayMessage ("TEXT");
		Debug.Log("Welcome");
		Debug.Log("Hit! Obj: " + thisObject.gameObject.name); 
		//Camera.main.transform.position = cameras[currentRoom].camPos;
		//Camera.main.transform.rotation = Quaternion.Euler(cameras[currentRoom].camRot);

	}

	void OnTriggerStay(Collider thisObject) {
		Debug.Log("STAY :)");
	}

	void OnTriggerExit(Collider thisObject) {
		Debug.Log("bye-bye birdie");
	}

}
