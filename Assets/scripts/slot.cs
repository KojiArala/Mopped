using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class slot : EventManager {
	public Sprite spriteNorm;
	public Sprite spriteHigh;
	public string itemName;
	public string itemDescription;
	public string useWith;
	public bool slotEmpty;
	private GameObject thisObject;

	public int maxSize; // The max amount of times the item can stack
	
	protected override void Start () {
		// protected or public to use override (child), call base.Start() to cascade them
		base.Start ();
		slotEmpty = true;
	}

	public void OnRightPointerClick(PointerEventData data) {
		Debug.Log("Right-click "+gameObject.name);
	}

	protected override void Update () {
		base.Update ();
	}

	// Uses the item
	public void clicked() {
		// stuff goes here
		if (Input.GetMouseButton (0)) {
			// left button clicked...
			if(itemName != "") {
				if(useWith != "") {
					Debug.Log("Use me with " + useWith);
				}
				else {
					Debug.Log("Using " + itemName);
				}
			}
		}
		
		if (Input.GetMouseButton (1)) {
			// right button clicked...display info
			if(itemDescription != "") {
				if(itemName[0] == '~') {
					GameObject overlay = GameObject.Find (itemName);
					overlay.transform.position = new Vector2 (overlay.transform.position.x + base.offset, overlay.transform.position.y);
				}
				else {
					base.displayMessage(itemDescription);
				}
			}
		}
		
		if (Input.GetMouseButton (2)) {
			// middle button clicked
			Debug.Log("middle click");
		}
	}

	void checkInventory(){
		Button[] buttons;
		buttons = base.invBox.GetComponentsInChildren<Button>();
		foreach (Button thisOne in buttons) { // Loop through each button inside the inventory "box"
//			if(thisOne.GetComponent<slot>().slotEmpty && !slotFound) {
//				thisOne.GetComponent<slot>().itemName = thisObject.GetComponent<proxObj>().itemName;
//				thisOne.GetComponent<slot>().itemDescription = thisObject.GetComponent<proxObj>().itemDescription;
//				thisOne.GetComponent<slot>().useWith = thisObject.GetComponent<proxObj>().useWith;
//				thisOne.GetComponent<slot>().slotEmpty = false;
//				
//				thisOne.GetComponent<Image>().sprite = thisObject.GetComponent<proxObj>().spriteNorm;
//				SpriteState st = new SpriteState();
//				st.highlightedSprite = thisObject.GetComponent<proxObj>().spriteHigh;
//				st.pressedSprite = thisObject.GetComponent<proxObj>().spriteNorm;
//				thisOne.spriteState = st;
//
//				break;
//			}
		}
	}

}
