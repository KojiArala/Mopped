using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class roomSwitcher : EventManager {
//	enum rooms { room0, room1, room2, room3, room4 };
	List<cameras> cameras = new List<cameras>();
	int currentRoom = -1;
	bool switchPad = false;
	//rooms previousRoom;

	Dictionary<string, int> roomsData = new Dictionary<string, int>();

	protected override void Start() {
		// protected or public to use override (child), call base.Start() to cascade them
		base.Start();

		roomData ();
		cameraData ();
		switchCamera(1); // start in room 1 until the menu screen is created
	} // END Start

	void OnTriggerEnter(Collider thisObject) {
		if(thisObject.name == "m2") {
			int tempCode;
			string tempRoomName = this.name;
			if(tempRoomName[0] == '~') switchPad = true;
			if(roomsData.TryGetValue(tempRoomName, out tempCode)){
				if(tempCode == currentRoom && switchPad) tempCode--;
				switchCamera(tempCode);
			}
			else {
				Debug.Log("ERROR: room data for " + tempRoomName + " not in Dictionary");
			}
		}
	} // END OnTriggerEnter

	void switchCamera(int thisCam) {
		currentRoom = thisCam;
		Camera.main.transform.position = cameras[thisCam].position;
		Camera.main.transform.rotation = Quaternion.Euler(cameras[thisCam].rotation);
		//Camera.main.fieldOfView -= 1; //decrease field of view (zoom)
	}

	void OnTriggerStay(Collider thisObject) {
		//Debug.Log("STAY :)");
	} // END OnTriggerStay

	void OnTriggerExit(Collider thisObject) {
		//Debug.Log("bye-bye birdie");
	} // END OnTriggerExit

	void roomData() {
		// room data used to determine which camera data to pull
		// first argument is name of collider second is the int or index of the camaraData List
		roomsData.Add( "room0", 0);			// room 0 - menu screen?
		roomsData.Add( "room1", 1);			// room 1
		roomsData.Add( "room2", 2);			// room 2
		roomsData.Add( "~room2_part2", 3);	// room 2 second half
		roomsData.Add( "room3", 4);			// room 3

	} // END roomData

	void cameraData() {
		// room camera data first Vector3 is position, second Vector3 is rotation
		cameras.Add( new cameras(new Vector3(0, 0, 0), new Vector3 (0, 0, 0) ));				// room 0 - menu screen?
		cameras.Add( new cameras(new Vector3(-1.25f, 3.7f, -6.5f), new Vector3 (17, 35, 0) ));	// room 1
		cameras.Add( new cameras(new Vector3(50, 4, -1), new Vector3 (0, -40, 0) ));			// room 2
		cameras.Add( new cameras(new Vector3(43, 4, 62), new Vector3 (14.8f, 12, 0) ));			// room 2 second half
		cameras.Add( new cameras(new Vector3(41, 0.25f, 18), new Vector3 (5, 287, 0) ));		// room 3

	} // END cameraData


}
