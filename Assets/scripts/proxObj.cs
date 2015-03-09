using UnityEngine;
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
	private Dictionary<string, int> keypadCodes = new Dictionary<string, int>();
	private Dictionary<string, string> keypads = new Dictionary<string, string>();
	private Dictionary<string, string> doors = new Dictionary<string, string>();
	private Dictionary<string, string> actions = new Dictionary<string, string>();
	private string temp = null;
	private string nameTemp = null;
	private int tempCode;
	private string currentDoor;
	private string doorAction;
	private GameObject currentKeypad;
	private Text txt;
	private Image[] images;
	private Button[] buttons;
	private GameObject thisObject;
	private GameObject tempObject;
	
	public string itemName;
	public string itemDescription;
	public string useWith;

	// sprites for the button, normal
	public Sprite spriteNorm;
	public Sprite spriteHigh;
	// sprites for the button, second state for "used" items
	public Sprite spriteNorm2;
	public Sprite spriteHigh2;
	public Sprite overlayNorm2;
	public Sprite overlayHigh2;

	public int maxSize; // The max amount of times the item can stack

	protected override void Start () {
		// protected or public to use override (child), call base.Start() to cascade them
		base.Start ();
		thisPos = this.transform.position;
		addGameObjects();
		thisCode = -100;
		currentDoor = null;
		doorAction = null;
	} // END Start
	
	// Update is called once per frame
	protected override void Update () {
		base.Update ();
		// if statement to do check only if object is visible in camera
		delta = Vector3.Distance (m2Pos, thisPos);

		if (this.tag == "room_structure") {
			// delta for support beam is ok but not working correctly for walls...scale issue???
		}

		if(delta < 3) {
			// M2 is within range of "this.name"
		}

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
			// left button clicked;
			if(slotPicked && !EventSystem.current.IsPointerOverGameObject()) {
				// something picked up so check if usable in world space
				if(thisObject.name == useItemWith) {
					base.useItem(true);
				}
				else {
					dontUseItem(thisObject.name);
				}
			}

			else if (stationary && !iconMoving
			    				&& (thisObject.tag == "obj" ||
			                        thisObject.tag == "obj_pickup" ||
			                        thisObject.tag == "keypad" ||
			                        thisObject.tag == "floor")
			    				&& !EventSystem.current.IsPointerOverGameObject()) {
				if(thisObject.tag == "keypad" && keypadClosed){
					//Debug.Log (thisObject.name);
					if(keypads.TryGetValue(thisObject.name, out nameTemp)){
						tempObject = GameObject.Find (nameTemp);
						tempObject.transform.position = new Vector2 (tempObject.transform.position.x + base.offset, base.guiBottom);
						keypadClosed = false;
						if(keypadCodes.TryGetValue(thisObject.name, out tempCode)){
							thisCode = tempCode;
						}
						else {
							Debug.Log ("ERROR: proxObj keypad code does not exist in dictionary");
							thisCode = -100;
						}
						//currentDoor
						if(doors.TryGetValue(thisObject.name, out nameTemp)){
							currentDoor = nameTemp;
							currentKeypad = thisObject;
						}
						else {
							Debug.Log ("ERROR: proxObj keypad code does not exist in dictionary");
							thisCode = -100;
						}
						//action for door
						if(actions.TryGetValue(thisObject.name, out nameTemp)){
							doorAction = nameTemp;
						}
						else {
							Debug.Log ("ERROR: proxObj keypad code does not exist in dictionary");
							thisCode = -100;
						}

					}
					else {
						Debug.Log ("ERROR: proxObj keypad does not exist in dictionary");
						nameTemp = "ERROR (" + thisObject.name + ")";
					}
				} // END check if a keypad IF
			}

			if(!EventSystem.current.IsPointerOverGameObject() && thisObject.tag != "room_structure" && thisObject.name != "m2") {//move M2 to clicked position
				//	get vector3 but only use x - z position
				base.moveTo = new Vector3(hit.point.x, base.lastPosition.y, hit.point.z);
				stationary = false;
			}
		} // END left button click IF

		if (Input.GetMouseButton (1)) {
			// right button clicked;
			if(!EventSystem.current.IsPointerOverGameObject()) {
				if(objects.TryGetValue(thisObject.name, out temp)){
					base.displayMessage (temp);
				}
				else {
					base.displayMessage ("");
					Debug.Log (thisObject.name + " not in Dictionary");
				}
			}
		} // END right button click IF

		if (Input.GetMouseButton (2)) {
			// middle button clicked;
		} // END middle button click IF

		//check keypad code entry if keypad code is loaded
		if (thisCode > 0) {
			if(thisCode == int.Parse(tappedCode)) {
				GameObject singleDoor;
				singleDoor = GameObject.Find(currentDoor);
				singleDoor.GetComponent<proxDoor>().unlockDoor(doorAction);
				string thisDoorName = singleDoor.GetComponent<proxDoor>().doorName;
				base.displayMessage ("Opening " + thisDoorName);

				tempObject.transform.position = new Vector2 (-606, tempObject.transform.position.y);
				keypadClosed = true;
				thisCode = -100;
				tappedCode = "-42";
				currentDoor = null;
				currentKeypad.gameObject.tag = "Untagged";
				currentKeypad = null;
				//Debug.Log("OPENED " + singleDoor.GetComponent<proxDoor>().doorName);
			}
		} // END keypad code IF
	} // END Update

	void OnMouseDown () {
		// only for left click
		if(this.tag == "obj_pickup") {
			if(!slotPicked) addToInventory();
		}
	} // END OnMouseDown

	void addGameObjects (){
		//keypads
		//	name of keypad object in room
		//	name of keypad panel to move into place
		keypads.Add ("door_keypad_room1", "room1_keypad");
		keypads.Add ("cabinet_door", "room1_keypad");

		//keypad codes
		//	code used to "open" object
		keypadCodes.Add ("door_keypad_room1", 9432);
		keypadCodes.Add ("cabinet_door", 0042);

		//keypad objects
		//	name of object in room the keypad "opens"
		doors.Add ("door_keypad_room1", "door_room1");
		doors.Add ("cabinet_door", "cabinet_door");

		//keypad objects
		//	what to do with the object, move or rotate
		actions.Add ("door_keypad_room1", "open");
		actions.Add ("cabinet_door", "rotate");

		//room 1 objects
		//	name of room object
		//	descriptive text for that object
		objects.Add ("bucket", "Bucket, contains Class 1 cleaning solution.\nAlly in war on floor based contaminants.\n\n");
		objects.Add ("pencil", "Pencil. Non-permanent writing implement.\nFrequent source of floor debris.\n\n");
		objects.Add ("notebook", "Paper notepad. Labeled as property\nof Maintenance Chief Harlow.\n\n");
		objects.Add ("bottle", "This is a bottle\n\n\n");
		objects.Add ("door_room1", "It is a door. It opens.\nSometimes it does not.\nThat is when I see it most.\n");
		objects.Add ("door_keypad_room1", "Hey look...a keypad\non the wall\n\n");
		objects.Add ("mop", "My mop. My greatest weapon in the war on filth. It must be retrieved; a stain exists in the next room.");
		//room 2 objects

	} // END addGameObjects

	void addToInventory(){
		bool slotFound = false;
		buttons = base.invBox.GetComponentsInChildren<Button>();
		foreach (Button thisOne in buttons) { // Loop through each button inside the inventory "box"
			if(thisOne.GetComponent<slot>().slotEmpty && !slotFound) {
				string tempNameString = thisObject.GetComponent<proxObj>().itemName;
				if(tempNameString != "" && tempNameString[0] == '~') tempNameString = tempNameString.Substring(1);
				thisOne.name = tempNameString;
				thisOne.GetComponent<slot>().itemName = thisObject.GetComponent<proxObj>().itemName;
				thisOne.GetComponent<slot>().itemDescription = thisObject.GetComponent<proxObj>().itemDescription;
				thisOne.GetComponent<slot>().useWith = thisObject.GetComponent<proxObj>().useWith;
				thisOne.GetComponent<slot>().slotEmpty = false;

				thisOne.GetComponent<slot>().spriteNorm = thisObject.GetComponent<proxObj>().spriteNorm;
				thisOne.GetComponent<slot>().spriteHigh = thisObject.GetComponent<proxObj>().spriteHigh;
				thisOne.GetComponent<slot>().spriteNorm2 = thisObject.GetComponent<proxObj>().spriteNorm2;
				thisOne.GetComponent<slot>().spriteHigh2 = thisObject.GetComponent<proxObj>().spriteHigh2;
				thisOne.GetComponent<slot>().overlayNorm2 = thisObject.GetComponent<proxObj>().overlayNorm2;
				thisOne.GetComponent<slot>().overlayHigh2 = thisObject.GetComponent<proxObj>().overlayHigh2;

				thisOne.GetComponent<Image>().sprite = thisObject.GetComponent<proxObj>().spriteNorm;
				SpriteState st = new SpriteState();
				st.highlightedSprite = thisObject.GetComponent<proxObj>().spriteHigh;
				st.pressedSprite = thisObject.GetComponent<proxObj>().spriteNorm;
				thisOne.spriteState = st;
				slotFound = true;

				// once slot has been set remove the selected object from game
				Destroy (thisObject);
				break; // break statement appears to not work at all, it sets every button anyhoo
			}
		}
		if(!slotFound) {
			// all slots are full, no more room in inventory
			base.displayMessage("Inventory full, please clear something out before trying to add something new.");
		}
	} // END addToInventory

	void clearEmptySlots(){
		buttons = base.invBox.GetComponentsInChildren<Button>();
		foreach (Button thisOne in buttons) { // Loop through each button inside the inventory "box"
			if(thisOne.GetComponent<slot>().slotEmpty) {
				thisOne.name = "slot";
				// change slot variables to emptySlot
				thisOne.GetComponent<slot>().itemName = null;
				thisOne.GetComponent<slot>().itemDescription = null;
				thisOne.GetComponent<slot>().useWith = null;
				thisOne.GetComponent<slot>().slotEmpty = true;

				thisOne.GetComponent<slot>().spriteNorm = emptySlot.GetComponent<slot>().spriteNorm;
				thisOne.GetComponent<slot>().spriteHigh = emptySlot.GetComponent<slot>().spriteHigh;
				thisOne.GetComponent<slot>().spriteNorm2 = emptySlot.GetComponent<slot>().spriteNorm2;
				thisOne.GetComponent<slot>().spriteHigh2 = emptySlot.GetComponent<slot>().spriteHigh2;
				thisOne.GetComponent<slot>().overlayNorm2 = emptySlot.GetComponent<slot>().overlayNorm2;
				thisOne.GetComponent<slot>().overlayHigh2 = emptySlot.GetComponent<slot>().overlayHigh2;

				thisOne.GetComponent<Image>().sprite = emptySlot.GetComponent<slot>().spriteNorm;
				SpriteState st = new SpriteState();
				st.highlightedSprite = emptySlot.GetComponent<slot>().spriteNorm;
				st.pressedSprite = emptySlot.GetComponent<slot>().spriteNorm;
				thisOne.spriteState = st;
				
				break;
			}
		}
	} // END clearEmptySlots
	

}
