﻿using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class slot : EventManager {
	public string itemName;
	public string DescriptionGame;
	public string DescriptionInventory;
	public string useWith;
	public string soundType;
	public bool slotEmpty;
	//public int maxSize; // The max amount of times the item can stack

	// sprites for the button, normal
	public Sprite spriteNorm;
	public Sprite spriteHigh;
	// sprites for the button, second state for "used" items
	// overlay ones take care of overlay graphics (EG. notebook image swap)
	public Sprite spriteNorm2;
	public Sprite spriteHigh2;
	public Sprite overlayNorm2;
	public Sprite overlayHigh2;

	private GameObject thisInnerObject;
	private int mouseOffset = 27;

	protected override void Start() {
		// protected or public to use override (child), call base.Start() to cascade them
		base.Start();
		slotEmpty = true;
		iconMoving = false;
	}

//	public void OnRightPointerClick(PointerEventData data) {
//		Debug.Log("Right-click "+gameObject.name);
//	}

	protected override void Update() {
		base.Update();

		if(slotPicked) {
			Vector3 mousePos = Input.mousePosition;
			mousePos = new Vector3(mousePos.x+mouseOffset, mousePos.y-mouseOffset, mousePos.z);
			pickedSlotIcon.transform.position = mousePos;
			//GetSiblingIndex(), SetSiblingIndex(), SetAsFirstSibling(), SetAsLastSibling()
			// display order is bottom up, items on the bottom will be displayed above everything
			// so using SetAsLastSibling() will display that piece on top of everything else in
			// the hierarchy of the current Canvas item
			pickedSlotIcon.transform.SetAsLastSibling();
		}
	} // END Update

	// Uses the item
	public void clicked() {
		if (Input.GetMouseButton (0)) {
			hoverTextBox.transform.position = new Vector3 (0, -50, 0);

			// left button clicked...
			if(itemName != "" && !slotPicked) {
				// move overlay into position
				slotX = this.transform.position.x;
				slotY = this.transform.position.y;
				slotZ = this.transform.position.z;
				slotPicked = true;
				useItemWith = useWith;
				iconMoving = true;
				// attach slot to mouse
				pickedSlotIcon = GameObject.Find (this.name);
			}

			else if(iconMoving) {
				// something picked up so check if usable with another inventory item
				if(this.name == useItemWith) {
					base.useItem(true);
				}
				else {
					base.swapSlot(this);
				}
//				if(this.GetComponent<slot>().slotEmpty) {
//					base.swapSlot(this);
//				}
//				else {
//					dontUseItem(this.itemName);
//				}
			}
		} // END left button click IF

		if (Input.GetMouseButton (1)) {
			// right button clicked...display info
			if(itemName[0] == '~') {
				GameObject overlay = GameObject.Find (itemName);
				if(overlay.transform.position.x < 0) overlay.transform.position = new Vector2 (overlay.transform.position.x + base.offset, base.guiBottom);
				//slotPicked = false;
			}
			else if(DescriptionGame != "") {
				base.displayMessage(DescriptionGame);
			}
		} // END right button click IF

		if (Input.GetMouseButton (2)) {
			// middle button clicked
		} // END middle button click IF
	} // END clicked

	public void OnMouseOver() {
		//Debug.Log (Input.mousePosition + " ~ " + ray);
		if(this.name != "slot") {
			string nameText = this.itemName;
			try {
				if(nameText != "" && nameText[0] == '~') nameText = nameText.Substring(1);
			}
			catch(System.Exception e) {
				string errorString = e.ToString();
			}
				
			Text hoverString = hoverTextBox.GetComponent<Text>();
			hoverString.text = nameText;

			Vector3 objPosition = new Vector3(this.transform.position.x + 30, this.transform.position.y + 40, this.transform.position.z);
			hoverTextBox.transform.position = objPosition;
		}
	}
	
	public void OnMouseExit() {
		Text hoverString = hoverTextBox.GetComponent<Text>();
		hoverString.text = "";
		hoverTextBox.transform.position = new Vector3 (0, -50, 0);
	}

} // END class
