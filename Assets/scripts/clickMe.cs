using UnityEngine;
using System.Collections;

public class clickMe : EventManager {
	void OnEnable() {
		//EventManager.OnClicked += displayMessage;
	}
	
	void OnDisable() {
		//EventManager.OnClicked -= displayMessage;
	}
	
	void displayMessage() {
		/*
		// Use this one to unlock all doors
		base.displayMessage (Random.Range(-10.0F, 10.0F).ToString() + "\nUnlocking all doors", 2);
		GameObject[] doors;
		doors = GameObject.FindGameObjectsWithTag("door");
		foreach (GameObject door in doors) {
			door.GetComponent<proxDoor>().unlockDoor();
		}
		*/
		
		// Use this one to unlock a single door
		base.displayMessage (Random.Range(-10.0F, 10.0F).ToString() + "\nUnlocking door 1");
		GameObject singleDoor;
		singleDoor = GameObject.Find("door_room1");
		singleDoor.GetComponent<proxDoor>().unlockDoor();
	}
}