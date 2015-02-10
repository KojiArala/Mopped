﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class proxObj : EventManager {
	// old method kept for reference
	//private object[] objects = GameObject.FindGameObjectsWithTag("obj");
	private float delta;
	private Vector3 thisPos;
	private Dictionary<string, string> objects = new Dictionary<string, string>();
	private string temp = null;
	private Text txt;

	protected override void Start () {
		// protected or public to use override (child), call base.Start() to cascade them
		base.Start ();
		thisPos = this.transform.position;
		addGameObjects();
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update ();
		// if statement to do check only if object is visible in camera
		delta = Vector3.Distance (m2Pos, thisPos);

		if (this.tag == "room_structure") {
			// delta for support beam is ok but not working correctly for walls...scale issue???
			//Debug.Log (this.tag + " ~~~ " + this.name);
		}

		if(delta < 3) {
			//Debug.Log ("M2 is within range of " + this.name);
		}

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		Physics.Raycast(ray, out hit);

		if (Input.GetMouseButton (0)) {
			// left button clicked;
			if (base.stationary && (hit.transform.gameObject.tag == "obj" || hit.transform.gameObject.tag == "obj_pickup" || hit.transform.gameObject.tag == "floor") && !EventSystem.current.IsPointerOverGameObject()) {
				// get vector3 but only use x - z position
				base.moveTo = new Vector3(hit.point.x, base.lastPosition.y, hit.point.z);
				base.stationary = false;
			}

			if(hit.transform.gameObject.tag == "obj_pickup"){
				base.inventory.Add(new inventory(hit.transform.gameObject.name));
				Destroy (hit.transform.gameObject);

				textMe.text = "";
				foreach(inventory thisOne in base.inventory) {
					if(base.inventoryOverlay.TryGetValue(thisOne.name, out temp)){
						// prints out all objects in inventoryOverlay that match items in inventory List
						//   in order you picked them up
						textMe.text += temp + "\n";
					}

				}
			}

			if(hit.transform.gameObject.name == "door_keypad_room1") {
				base.displayMessage ("Unlocking door 1", 1);
				GameObject singleDoor;
				singleDoor = GameObject.Find("door_room1");
				singleDoor.GetComponent<proxDoor>().unlockDoor();
			}
		}
		if (Input.GetMouseButton (1)) {
			// right button clicked;
			if(objects.TryGetValue(hit.transform.gameObject.name, out temp)){
				base.displayMessage (temp, 4);
			}
			else {
				base.displayMessage ("", 1);
				Debug.Log (hit.transform.gameObject.name + " not in Dictionary");
			}
		}
		if (Input.GetMouseButton (2)) {
			// middle button clicked;
		}

	}

	void OnMouseDown () {
		// only for left click
		//Debug.Log ("you clicked me " + this.name);
	}

	void addGameObjects (){
		//room 1 objects
		objects.Add ("bucket", "Bucket, contains Class 1 cleaning solution.\nAlly in war on floor based contaminants.\n\n");
		objects.Add ("pencil", "Pencil. Non-permanent writing implement.\nFrequent source of floor debris.\n\n");
		objects.Add ("notebook", "Paper notepad. Labeled as property\nof Maintenance Chief Harlow.\n\n");
		objects.Add ("bottle", "This is a bottle\n\n\n");
		objects.Add ("door_room1", "It is a door. It opens.\nSometimes it does not.\nThat is when I see it most.\n");
		objects.Add ("door_keypad_room1", "Hey look...a keypad\non the wall\n\n");
		//room 2 objects

	}
}