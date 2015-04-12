using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class roomSwitcher : EventManager {
//	enum rooms { room0, room1, room2, room3, room4 };
	public class rotPosData {
		public Vector3 pos;
		public Vector3 rot;
		
		public rotPosData(Vector3 newPos, Vector3 newRot) {
			pos = newPos;
			rot = newRot;
		}
	}

	List<cameras> cameras = new List<cameras>();
	List<rotPosData> m2Data = new List<rotPosData>();
	int currentRoom = -1;
	bool switchPad = false;
	//rooms previousRoom;

	Dictionary<string, int> roomsData = new Dictionary<string, int>();

	protected override void Start() {
		// protected or public to use override (child), call base.Start() to cascade them
		base.Start();

		roomData();
		cameraData();
		m2PlaceData();
		switchCamera(1); // start in room 1 until the menu screen is created

	} // END Start

	void OnTriggerEnter(Collider thisObject) {
		if(thisObject.name == "m2") {
			int tempCode;
			string tempRoomName = this.name;
			if(tempRoomName[0] == '~') switchPad = true;
			if(roomsData.TryGetValue(tempRoomName, out tempCode)){
				if(tempCode == currentRoom && switchPad) tempCode--;
				if(tempCode != currentRoom) switchCamera(tempCode);
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
		// move M2 into new room start position
		//stationary = true;
		moveTo = m2Data [thisCam].pos;
		m2.transform.position = m2Data[thisCam].pos;
		m2.transform.rotation = Quaternion.Euler(m2Data[thisCam].rot);
		// add in 1 to 3 unit forward movement
		//m2.transform.localPosition += Vector3.forward;
		//moveTo = Vector3.forward;
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
		roomsData.Add( "room0", 0);			// room 0 - menu screen, placeholder only
		roomsData.Add( "room1", 1);			// room 1
		roomsData.Add( "room2", 2);			// room 2
		roomsData.Add( "~room2_part2", 3);	// room 2 second half
		roomsData.Add( "room3", 4);			// room 3

	} // END roomData

	void cameraData() {
		// room camera data first Vector3 is position, second Vector3 is rotation
		cameras.Add( new cameras(new Vector3(0, 0, 0), new Vector3 (0, 0, 0) ));							// room 0 - menu screen, placeholder only
		cameras.Add( new cameras(new Vector3(-1.25f, 3.7f, -6.5f), new Vector3 (17, 35, 0) ));				// room 1
		cameras.Add( new cameras(new Vector3(33.82f, 9.89f, 23.53f), new Vector3 (26.4f, 78.9f, 357.4f) ));	// room 2
		cameras.Add( new cameras(new Vector3(57.41f, 8.3f, 68.39f), new Vector3 (24, 328.64f, 358.25f) ));	// room 2 second half
		cameras.Add( new cameras(new Vector3(41, 0.25f, 18), new Vector3 (5, 287, 0) ));					// room 3

	} // END cameraData

	void m2PlaceData() {
		// m2 data for new room start first Vector3 is position, second Vector3 is rotation
		m2Data.Add (new rotPosData(new Vector3 (5.81f, -3.5f, 6.6f), new Vector3 (0, 0, 0)));		// room 0 - menu screen, placeholder only
		m2Data.Add (new rotPosData(new Vector3 (5.81f, -3.5f, 6.6f), new Vector3 (0, 0, 0)));		// room 1
		m2Data.Add (new rotPosData(new Vector3 (38.5f, -3.5f, 19), new Vector3 (0, 89, 0)));		// room 2
		m2Data.Add (new rotPosData(new Vector3 (47.8f, -3.5f, 70.6f), new Vector3 (0, 342, 0)));	// room 2 second half

	}

}
