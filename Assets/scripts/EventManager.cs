using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EventManager : MonoBehaviour {
	public delegate void ClickAction();
	//public static event ClickAction OnClicked;
	protected GameObject m2;
	protected Vector3 m2Pos;
	protected Vector3 m2Rot;
	protected bool stationary = false;
	protected Vector3 moveTo;
	protected Vector3 lastPosition;
	private float moveSpeed = 1f;
	private float turnSpeed = 1f;
	protected List<inventory> inventory = new List<inventory>();
	protected Dictionary<string, string> inventoryOverlay = new Dictionary<string, string>();
	//protected Dictionary<string, GameObject> inventoryOverlay = new Dictionary<string, GameObject>();
	protected int offset = 1000;
	protected int guiBottom = 230;
	public GameObject messageBox;
	protected static string pickedObject;

	// camera positions and rotations
	public static int currentRoom = 1;
	public struct cameraPosRot {
		public Vector3 camPos;
		public Vector3 camRot;
		
		public cameraPosRot(Vector3 posTemp, Vector3 rotTemp) {
			camPos = posTemp;
			camRot = rotTemp;
		}
	}
	public List<cameraPosRot> cameras = new List<cameraPosRot>();
	
	// UI elements
	protected GameObject invBox;
	protected GameObject invTab;
	protected static GameObject emptySlot;
	protected static GameObject tempSwapSlot;
	protected static GameObject pickedSlotIcon;
	protected static float slotX;
	protected static float slotY;
	protected static float slotZ;
	protected static bool slotPicked;
	protected static string useItemWith;
	protected static bool iconMoving;

	// keypad code setup
	public static int thisCode;
	public static string tappedCode;
	public static string numberTemp;
	public static bool keypadClosed = true;

	void Awake () {
		m2 = GameObject.Find("m2");
		messageBox = GameObject.Find("message");
		lastPosition = moveTo = m2.transform.position;
		stationary = true;
		addRoomCameras();
		addInventoryItems();
		getUIElements();

		// to get all text children of a canvas
		//GameObject canvas = GameObject.Find("Canvas");
		//Text[] textValue = canvas.GetComponentsInChildren<Text>();
		//textValue[0].text = "hey";

		pickedObject = "";

		thisCode = -10;
		tappedCode = "-42";
		numberTemp = "";

		slotX = -100;
		slotY = -100;
		slotZ = -100;
		slotPicked = false;
		useItemWith = "";
		iconMoving = false;

		Camera.main.transform.position = cameras[currentRoom].camPos;
		Camera.main.transform.rotation = Quaternion.Euler(cameras[currentRoom].camRot);
		//Camera.main.fieldOfView -= 1; //decrease field of view (zoom)
	} // END Awake
	
	protected virtual void Start() {
		// protected or public to use virtual (parent)
		//textBoxMe.SetActive(false);	// uncomment to hide by default
	} // END Start

	protected virtual void Update () {
		m2Pos = m2.transform.position;
		if (lastPosition != moveTo) {
			m2.transform.Translate(new Vector3(0,0,0));
			lastPosition = moveTo;
			// remove following line once smooth rotation is working
			m2.transform.LookAt(moveTo);
			// setting Rotation point isn't working
			//m2Rot = new Vector3(moveTo.x, 0, moveTo.z);
		}

		if (!stationary) {
			//smooth rotation not working yet
			//m2.transform.rotation = Quaternion.Slerp(m2.transform.rotation,Quaternion.LookRotation(m2Rot),Time.deltaTime * turnSpeed);

			//m2.transform.position = Vector3.Lerp(m2Pos, moveTo, Time.deltaTime * moveSpeed);
			m2.transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);

			if(Vector3.Distance (m2.transform.position, moveTo) < .2f) {
				stationary = true;
				//Debug.Log ("FINAL: " + m2.transform.position + " ~~~ " + moveTo);
			}
		}
	} // END Update

	protected Vector3 getM2Pos(){
		return m2Pos;
	} // END getM2Pos
	
	protected void displayMessage(string displayString) {
		// convert escaped line breaks to real ones
		displayString = displayString.Replace("\\n", "\n");
		Text thisString = messageBox.GetComponent<Text>();
		thisString.text = displayString;

		//CancelInvoke();
		Invoke ("removeText", 5);
	} // END displayMessage
	
	void removeText() {
		Text thisString = messageBox.GetComponent<Text>();
		// try fade out before nulling the value
		thisString.text = "";
	} // END removeText

	protected void useItem(bool useItem) {
		// can use item with clicked world object so do it here
		if(useItem) useInventoryItem();
		
//		if(useItemWith != "" && useItemWith[0] == '~') useItemWith = useItemWith.Substring(1);
		pickedSlotIcon.name = "slot";
		pickedSlotIcon.GetComponent<slot>().itemName = null;
		pickedSlotIcon.GetComponent<slot>().itemDescription = null;
		pickedSlotIcon.GetComponent<slot>().useWith = null;
		pickedSlotIcon.GetComponent<slot>().slotEmpty = true;

		pickedSlotIcon.GetComponent<slot>().spriteNorm = emptySlot.GetComponent<slot>().spriteNorm;
		pickedSlotIcon.GetComponent<slot>().spriteHigh = emptySlot.GetComponent<slot>().spriteHigh;
		pickedSlotIcon.GetComponent<slot>().spriteNorm2 = emptySlot.GetComponent<slot>().spriteNorm2;
		pickedSlotIcon.GetComponent<slot>().spriteHigh2 = emptySlot.GetComponent<slot>().spriteHigh2;
		pickedSlotIcon.GetComponent<slot>().overlayNorm2 = emptySlot.GetComponent<slot>().overlayNorm2;
		pickedSlotIcon.GetComponent<slot>().overlayHigh2 = emptySlot.GetComponent<slot>().overlayHigh2;

		pickedSlotIcon.GetComponent<Image>().sprite = emptySlot.GetComponent<slot>().spriteNorm;
		SpriteState st = new SpriteState();
		st.highlightedSprite = emptySlot.GetComponent<slot>().spriteHigh;
		st.pressedSprite = emptySlot.GetComponent<slot>().spriteNorm;
		pickedSlotIcon.GetComponent<Button>().spriteState  = st;
		moveSlotBack();
	}// END useItem

	protected void dontUseItem(string thisString) {
		// can't use item with clicked world object, keep slot variables the same
		bool noErr = true;
		string firstObj = pickedSlotIcon.GetComponent<slot>().itemName;
		try {
			if(firstObj != "" && firstObj[0] == '~') firstObj = firstObj.Substring(1);
		}
		catch(System.Exception e) {
			string errorString = e.ToString();
			noErr = false;
		}

		string secondObj = thisString;
		if(secondObj != "" && secondObj[0] == '~') secondObj = secondObj.Substring(1);
		if(noErr) displayMessage("Sorry, you can't use " + firstObj + " with " + secondObj);
		moveSlotBack();
	} // END dontUseItem

	void moveSlotBack() {
		// move "slot back to (slotX, slotY), Cancel mouse follow
		pickedSlotIcon.transform.position = new Vector3(slotX, slotY, slotZ);
		slotPicked = false;
		iconMoving = false;
	}
	
	protected void swapSlot(slot thisSlot) {
		SpriteState st;
		string tempNameString;

		/******************************************************
		 *****************************************************/
		//tempNameString = thisSlot.itemName;
		//if(tempNameString != "" && tempNameString[0] == '~') tempNameString = tempNameString.Substring(1);
		tempSwapSlot.GetComponent<slot>().itemName = pickedSlotIcon.GetComponent<slot>().itemName;
		tempSwapSlot.GetComponent<slot>().itemDescription = pickedSlotIcon.GetComponent<slot>().itemDescription;
		tempSwapSlot.GetComponent<slot>().useWith = pickedSlotIcon.GetComponent<slot>().useWith;
		tempSwapSlot.GetComponent<slot>().slotEmpty = pickedSlotIcon.GetComponent<slot>().slotEmpty;
		
		tempSwapSlot.GetComponent<slot>().spriteNorm = pickedSlotIcon.GetComponent<slot>().spriteNorm;
		tempSwapSlot.GetComponent<slot>().spriteHigh = pickedSlotIcon.GetComponent<slot>().spriteHigh;
		tempSwapSlot.GetComponent<slot>().spriteNorm2 = pickedSlotIcon.GetComponent<slot>().spriteNorm2;
		tempSwapSlot.GetComponent<slot>().spriteHigh2 = pickedSlotIcon.GetComponent<slot>().spriteHigh2;
		tempSwapSlot.GetComponent<slot>().overlayNorm2 = pickedSlotIcon.GetComponent<slot>().overlayNorm2;
		tempSwapSlot.GetComponent<slot>().overlayHigh2 = pickedSlotIcon.GetComponent<slot>().overlayHigh2;
		
		tempSwapSlot.GetComponent<Image>().sprite = pickedSlotIcon.GetComponent<slot>().spriteNorm;
		st = new SpriteState();
		st.highlightedSprite = pickedSlotIcon.GetComponent<slot>().GetComponent<slot>().spriteHigh;
		st.pressedSprite = pickedSlotIcon.GetComponent<slot>().GetComponent<slot>().spriteNorm;
		tempSwapSlot.GetComponent<Button>().spriteState = st;

		/******************************************************
		 *****************************************************/
		tempNameString = thisSlot.itemName;
		if(tempNameString == "") tempNameString = "slot"; 
		else if(tempNameString != "" && tempNameString[0] == '~') tempNameString = tempNameString.Substring(1);
		pickedSlotIcon.name = tempNameString;
		pickedSlotIcon.GetComponent<slot>().itemName = thisSlot.itemName;
		pickedSlotIcon.GetComponent<slot>().itemDescription = thisSlot.itemDescription;
		pickedSlotIcon.GetComponent<slot>().useWith = thisSlot.useWith;
		pickedSlotIcon.GetComponent<slot>().slotEmpty = thisSlot.slotEmpty;
		
		pickedSlotIcon.GetComponent<slot>().spriteNorm = thisSlot.spriteNorm;
		pickedSlotIcon.GetComponent<slot>().spriteHigh = thisSlot.spriteHigh;
		pickedSlotIcon.GetComponent<slot>().spriteNorm2 = thisSlot.spriteNorm2;
		pickedSlotIcon.GetComponent<slot>().spriteHigh2 = thisSlot.spriteHigh2;
		pickedSlotIcon.GetComponent<slot>().overlayNorm2 = thisSlot.overlayNorm2;
		pickedSlotIcon.GetComponent<slot>().overlayHigh2 = thisSlot.overlayHigh2;
		
		pickedSlotIcon.GetComponent<Image>().sprite = thisSlot.spriteNorm;
		st = new SpriteState();
		st.highlightedSprite = thisSlot.spriteHigh;
		st.pressedSprite = thisSlot.spriteNorm;
		pickedSlotIcon.GetComponent<Button>().spriteState = st;
		
		/******************************************************
		 *****************************************************/
		tempNameString = tempSwapSlot.GetComponent<slot>().itemName;
		if(tempNameString != "" && tempNameString[0] == '~') tempNameString = tempNameString.Substring(1);
		thisSlot.name = tempNameString;
		thisSlot.itemName = tempSwapSlot.GetComponent<slot>().itemName;
		thisSlot.itemDescription = tempSwapSlot.GetComponent<slot>().itemDescription;
		thisSlot.useWith = tempSwapSlot.GetComponent<slot>().useWith;
		thisSlot.slotEmpty = tempSwapSlot.GetComponent<slot>().slotEmpty;
		
		thisSlot.spriteNorm = tempSwapSlot.GetComponent<slot>().spriteNorm;
		thisSlot.spriteHigh = tempSwapSlot.GetComponent<slot>().spriteHigh;
		thisSlot.spriteNorm2 = tempSwapSlot.GetComponent<slot>().spriteNorm2;
		thisSlot.spriteHigh2 = tempSwapSlot.GetComponent<slot>().spriteHigh2;
		thisSlot.overlayNorm2 = tempSwapSlot.GetComponent<slot>().overlayNorm2;
		thisSlot.overlayHigh2 = tempSwapSlot.GetComponent<slot>().overlayHigh2;

		thisSlot.GetComponent<Image>().sprite = tempSwapSlot.GetComponent<slot>().spriteNorm;
		st = new SpriteState();
		st.highlightedSprite = tempSwapSlot.GetComponent<slot>().spriteHigh;
		st.pressedSprite = tempSwapSlot.GetComponent<slot>().spriteNorm;
		thisSlot.GetComponent<Button>().spriteState  = st;

		/******************************************************
		 *****************************************************/
		tempSwapSlot.GetComponent<slot>().itemName = null;
		tempSwapSlot.GetComponent<slot>().itemDescription = null;
		tempSwapSlot.GetComponent<slot>().useWith = null;
		tempSwapSlot.GetComponent<slot>().slotEmpty = true;
		
		tempSwapSlot.GetComponent<slot>().spriteNorm = emptySlot.GetComponent<slot>().spriteNorm;
		tempSwapSlot.GetComponent<slot>().spriteHigh = emptySlot.GetComponent<slot>().spriteHigh;
		tempSwapSlot.GetComponent<slot>().spriteNorm2 = emptySlot.GetComponent<slot>().spriteNorm2;
		tempSwapSlot.GetComponent<slot>().spriteHigh2 = emptySlot.GetComponent<slot>().spriteHigh2;
		tempSwapSlot.GetComponent<slot>().overlayNorm2 = emptySlot.GetComponent<slot>().overlayNorm2;
		tempSwapSlot.GetComponent<slot>().overlayHigh2 = emptySlot.GetComponent<slot>().overlayHigh2;
		
		tempSwapSlot.GetComponent<Image>().sprite = emptySlot.GetComponent<slot>().spriteNorm;
		st = new SpriteState();
		st.highlightedSprite = emptySlot.GetComponent<slot>().spriteHigh;
		st.pressedSprite = emptySlot.GetComponent<slot>().spriteNorm;
		tempSwapSlot.GetComponent<Button>().spriteState  = st;

		//useItem(false);
		moveSlotBack();
	} // END swapSlot

//	void OnGUI() {
//		//left in for reference only
//		Font thisFont;
//		//random fonts for testing
//		//thisFont = Resources.Load<Font> ("LcdStd");
//		//thisFont = Resources.Load<Font> ("PhoenixScript");
//		//thisFont = Resources.Load<Font> ("MAGNETOB");
//		thisFont = Resources.Load<Font> ("Japonesa_0");
//		
//		myStyle.font = thisFont;
//		myStyle.fontSize = 20;
//		myStyle.normal.textColor = Color.black;
//		myStyle.alignment = TextAnchor.MiddleCenter;
//		myStyle.wordWrap = true;
//		myStyle.fixedWidth = textWidth;
//		myStyle.fixedHeight = textHeight;
//		myStyle.name = "textBox";
//		
//		rctOff = GUI.skin.customStyles[0].overflow;
//		myStyle.overflow = rctOff;
//
//		GUI.Label (new Rect (Screen.width / 2 - textWidth / 2, textBoxTop, 100, 30), thisString, myStyle);
//
//		/*
//		if(GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height - 50, 200, 30), "Unlock Door")) {
//			if(OnClicked != null)
//				OnClicked();
//		}
//		*/
//	} // END OnGUI

	void addRoomCameras() {
		// room 0, start menu screen?
		cameras.Add (new cameraPosRot (new Vector3 (0, 0, 0), new Vector3 (0, 0, 0)));
		// room 1+
		cameras.Add (new cameraPosRot (new Vector3 (-1.25f, 3.7f, -6.5f), new Vector3 (17, 35, 0)));
	} // END addRoomCameras
	
	void addInventoryItems() {
		//room 1 objects
		inventoryOverlay.Add ("bucket", "Bucket object in your inventory now");
		inventoryOverlay.Add ("pencil", "Pencil...mmmmmm...graphite");
		inventoryOverlay.Add ("notebook", "Notebook, ode to pulp");
		inventoryOverlay.Add ("bottle", "Bottle, not just for babies anymore");
		//room 2 objects
	} // END addInventoryItems

	void getUIElements(){
		invTab = GameObject.Find ("inventory_image");
		invBox = GameObject.Find ("inventory");
		emptySlot = GameObject.Find ("emptySlot");
		tempSwapSlot = GameObject.Find ("tempSwapSlot");

	} // END getUIElements

	void useInventoryItem() {
		// useItemWith is name of picked object so switch/case statement to handle what to do
		if(useItemWith != "" && useItemWith[0] == '~') useItemWith = useItemWith.Substring(1);
//		if(useItemWith != "" && useItemWith[0] == '~') useItemWith = useItemWith.Remove(0,1);
		switch (useItemWith) {
		case "notebook":
			// clicked pencil on notebook
			doNotebook();
			break;
		default:
			Debug.Log("No option available in useInventoryItem for item " + useItemWith);
			//print("Incorrect intelligence level.");
			break;
		}

		
	} // END useInventoryItem

	void doNotebook() {
		GameObject iconObj = GameObject.Find ("notebook");
		GameObject overlayObj = GameObject.Find ("~notebook");

		iconObj.GetComponent<Image>().sprite = iconObj.GetComponent<slot>().spriteNorm2;
		SpriteState st = new SpriteState();
		st.highlightedSprite = iconObj.GetComponent<slot>().spriteNorm2;
		st.pressedSprite = iconObj.GetComponent<slot>().spriteNorm2;
		iconObj.GetComponent<Button>().spriteState = st;

		overlayObj.GetComponent<Image>().sprite = iconObj.GetComponent<slot>().overlayNorm2;

	}

} // END class









