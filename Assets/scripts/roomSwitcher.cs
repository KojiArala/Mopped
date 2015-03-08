using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class roomSwitcher : MonoBehaviour {
	//enum rooms : int { room0=0, room1=1, room2=2, room3=3, room4=4 };
	enum rooms { room0, room1, room2, room3, room4 };
	List<cameras> cameras = new List<cameras>();
	rooms previousRoom;

	Dictionary<string, int> roomsData = new Dictionary<string, int>();

	void Start () {
		roomData ();
		cameraData ();
		previousRoom = rooms.room0;
	}

	public void startMenu(){
		Camera.main.transform.position = cameras[0].position;
		Camera.main.transform.rotation = Quaternion.Euler(cameras[0].rotation);
		Debug.Log ("here");
	}


	void OnTriggerEnter(Collider thisObject) {
		// get string of enum value
		//rooms parsed_enum = (rooms)System.Enum.Parse(typeof(rooms), this.name, true);

//		//Debug.Log("Welcome");
//		Debug.Log("Hit " + this.name + " with object " + thisObject.gameObject.name);
//		Debug.Log(cameras[1].position);
//		Debug.Log(rooms.room1);
//		Debug.Log("There are " + cameras.Count + " cameras");
//		//Debug.Log (System.Enum.TryParse (this.name, out int test));

//		foreach(KeyValuePair<string, int> pair in roomsData) {
//			string thisOne = pair.Key + " = " + pair.Value;
//			Debug.Log(thisOne);
//		}

		int tempCode;
		//if(roomsData.ContainsKey(this.name)) {
		//if(roomsData.ContainsValue(300)) {
		if(roomsData.TryGetValue(this.name, out tempCode)){
			Camera.main.transform.position = cameras[tempCode].position;
			Camera.main.transform.rotation = Quaternion.Euler(cameras[tempCode].rotation);
		}
		else {
			Debug.Log("ERROR: room data for " + this.name + " not in Dictionary");
		}

	}

	void OnTriggerStay(Collider thisObject) {
		//Debug.Log("STAY :)");
	}

	void OnTriggerExit(Collider thisObject) {
		//Debug.Log("bye-bye birdie");
	}

	void roomData() {
		roomsData.Add( "room0", 0);
		roomsData.Add( "room1", 1);
		roomsData.Add( "room2", 2);
		roomsData.Add( "room3", 3);

	}

	void cameraData() {
		cameras.Add( new cameras(new Vector3(0, 0, 0), new Vector3 (0, 0, 0) ));
		cameras.Add( new cameras(new Vector3(-1.25f, 3.7f, -6.5f), new Vector3 (17, 35, 0) ));
		cameras.Add( new cameras(new Vector3(50, 4, -1), new Vector3 (0, -40, 0) ));

	}


}
