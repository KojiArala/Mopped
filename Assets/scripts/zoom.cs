using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class zoom : EventManager {
	List<cameras> zooms = new List<cameras>();
	Dictionary<string, int> zoomData = new Dictionary<string, int>();
	private GameObject thisObject;
	private Vector3 previousCamPos;
	private Quaternion previousCamRot;
	private bool zoomedIn = false;

	protected override void Start() {
		// protected or public to use override (child), call base.Start() to cascade them
		base.Start();
		zoomsData();
		zoomCameras();
	} // END Start
	
	protected override void Update() {
		base.Update();

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		Physics.Raycast(ray, out hit);
		try {
			thisObject = hit.transform.gameObject;
		}
		catch (System.Exception e) {
			// Error catching stuff to do here is needed
			string errorString = e.ToString();
		}
		
		if (Input.GetMouseButton (0)) {
			if(thisObject.name[0] == '+') {
				Debug.Log ("CLICKED: " + thisObject.name);
				
				int tempCode;
				if(zoomData.TryGetValue(thisObject.name, out tempCode)){
					if(!zoomedIn) {
						previousCamPos = Camera.main.transform.position;
						previousCamRot = Camera.main.transform.rotation;
						zoomedIn = true;
					}
					switchCamera(tempCode);
				}
				else {
					Debug.Log("ERROR: FOO data for " + thisObject.name + " not in Dictionary");
				}
			} // END +

			if(thisObject.name[0] == '-') {
				Camera.main.transform.position = previousCamPos;
				Camera.main.transform.rotation = previousCamRot;
				zoomedIn = false;
			} // END +
			

		} // END left button click IF
		
		if (Input.GetMouseButton (1)) {
			// right button clicked
		} // END right button click IF
		
		if (Input.GetMouseButton (2)) {
			// middle button clicked;
		} // END middle button click IF
		
	} // END Update

	void switchCamera(int thisCam) {
		stationary = true;
		moveTo = m2.transform.position;
		Camera.main.transform.position = zooms[thisCam].position;
		Camera.main.transform.rotation = Quaternion.Euler(zooms[thisCam].rotation);
		//Camera.main.fieldOfView -= 1; //decrease field of view (zoom)
	}
	
	void zoomsData() {
		// room data used to determine which camera data to pull
		// first argument is name of collider second is the int or index of the camaraData List
		zoomData.Add( "+Panel1", 0);	// closeup 1
		zoomData.Add( "+Panel2", 1);	// closeup 2
		zoomData.Add( "test3", 2);	// closeup 3

	} // END roomData
	
	void zoomCameras() {
		// room camera data first Vector3 is position, second Vector3 is rotation
		zooms.Add( new cameras(new Vector3(-63.6f, 0, 40), new Vector3 (0, 0, 0) ));		// closeup 1
		zooms.Add( new cameras(new Vector3(-63.6f, 0, 40), new Vector3 (0, 0, 0) ));		// closeup 2
		zooms.Add( new cameras(new Vector3(0, 0, 0), new Vector3 (0, 0, 0) ));				// closeup 3

	} // END cameraData
	
	
}
