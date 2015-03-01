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
	private string temp = null;
	private string nameTemp = null;
	private int tempCode;
	private string currentDoor;
	private Text txt;
	private Image[] images;
	private Button[] buttons;
	private GameObject thisObject;
	private GameObject tempObject;
	
	public Sprite spriteNorm;
	public Sprite spriteHigh;
	public string itemName;
	public string itemDescription;
	public string useWith;
	
	public int maxSize; // The max amount of times the item can stack

	protected override void Start () {
		// protected or public to use override (child), call base.Start() to cascade them
		base.Start ();
		thisPos = this.transform.position;
		addGameObjects();
		thisCode = -100;
		currentDoor = null;
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
		if(hit.transform.gameObject != null) thisObject = hit.transform.gameObject;

		if (Input.GetMouseButton (0)) {
			//Debug.Log("Clicked on a GameObject (on GUI) " + EventSystem.current.IsPointerOverGameObject());
			// left button clicked;
			if (base.stationary
			    				&& (thisObject.tag == "obj" ||
			                        thisObject.tag == "obj_pickup" ||
			                        thisObject.tag == "keypad" ||
			                        thisObject.tag == "floor")
			    				&& !EventSystem.current.IsPointerOverGameObject()) {
				if(thisObject.tag == "keypad" && keypadClosed){
					//Debug.Log (thisObject.name);
					if(keypads.TryGetValue(thisObject.name, out nameTemp)){
						tempObject = GameObject.Find (nameTemp);
						tempObject.transform.position = new Vector2 (500, tempObject.transform.position.y);
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
				}
				//Debug.Log("Using this code " + thisCode + " for keypad " + nameTemp);
			}

			if(!EventSystem.current.IsPointerOverGameObject()) {//move M2 to clicked position
				//	get vector3 but only use x - z position
				base.moveTo = new Vector3(hit.point.x, base.lastPosition.y, hit.point.z);
				base.stationary = false;
			}

		}

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
		}

		if (Input.GetMouseButton (2)) {
			// middle button clicked;
		}

		//check keypad code entry if keypad code is loaded
		if (thisCode > 0) {
			Debug.Log ("Checking code " + thisCode);
			
			if(thisCode == int.Parse(tappedCode)) {
				base.displayMessage ("Unlocking door 1");
				GameObject singleDoor;
				singleDoor = GameObject.Find(currentDoor);
				singleDoor.GetComponent<proxDoor>().unlockDoor();
				
				tempObject.transform.position = new Vector2 (-300, tempObject.transform.position.y);
				keypadClosed = true;
				thisCode = -100;
				tappedCode = "-42";
				currentDoor = null;
				Debug.Log("OPENED");
			}
		}
		
	}

	void OnMouseDown () {
		// only for left click
		if(this.tag == "obj_pickup") {
			addToInventory();
		}
	}
	
	void addGameObjects (){
		//keypads
		//	name of keypad object in room
		//	name of keypad panel to move into place
		keypads.Add ("door_keypad_room1", "room1_keypad");

		//keypad codes
		//	name of keypad object in room
		//	code to use for the selected keypad
		keypadCodes.Add ("door_keypad_room1", 9432);


		//keypad codes
		//	name of keypad object in room
		//	code to use for the selected keypad
		doors.Add ("door_keypad_room1", "door_room1");
		
		
		//room 1 objects
		objects.Add ("bucket", "Bucket, contains Class 1 cleaning solution.\nAlly in war on floor based contaminants.\n\n");
		objects.Add ("pencil", "Pencil. Non-permanent writing implement.\nFrequent source of floor debris.\n\n");
		objects.Add ("notebook", "Paper notepad. Labeled as property\nof Maintenance Chief Harlow.\n\n");
		objects.Add ("bottle", "This is a bottle\n\n\n");
		objects.Add ("door_room1", "It is a door. It opens.\nSometimes it does not.\nThat is when I see it most.\n");
		objects.Add ("door_keypad_room1", "Hey look...a keypad\non the wall\n\n");
		//room 2 objects

	}

	void addToInventory(){
		bool slotFound = false;
		buttons = base.invBox.GetComponentsInChildren<Button>();
		foreach (Button thisOne in buttons) { // Loop through each button inside the inventory "box"
			if(thisOne.GetComponent<slot>().slotEmpty && !slotFound) {
				thisOne.GetComponent<slot>().itemName = thisObject.GetComponent<proxObj>().itemName;
				thisOne.GetComponent<slot>().itemDescription = thisObject.GetComponent<proxObj>().itemDescription;
				thisOne.GetComponent<slot>().useWith = thisObject.GetComponent<proxObj>().useWith;
				thisOne.GetComponent<slot>().slotEmpty = false;

				thisOne.GetComponent<Image>().sprite = thisObject.GetComponent<proxObj>().spriteNorm;
				SpriteState st = new SpriteState();
				st.highlightedSprite = thisObject.GetComponent<proxObj>().spriteHigh;
				st.pressedSprite = thisObject.GetComponent<proxObj>().spriteNorm;
				thisOne.spriteState = st;
				slotFound = true;

				// once slot has been set remove the selected object from game
				Destroy (thisObject);
//				string tempString = thisOne.GetComponent<slot>().itemName + " added to inventory";
//				if(tempString[0] == '~') tempString = tempString.Substring(1);
//				//if(tempString[0] == '~') tempString = tempString.Remove(0,1);
//				base.displayMessage(tempString);
				break; // break statement appears to not work at all, it sets every button anyhoo
			}
		}
		if(!slotFound) {
			// all slots are full, no more room in inventory
			Debug.Log("Inventory full, please clear something out before trying to add something new.");
		}
		
	}
}
