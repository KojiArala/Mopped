using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

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
				Debug.Log("Using " + itemName);
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
			// middle button clicked...not working for some reason
		}
	}

}
