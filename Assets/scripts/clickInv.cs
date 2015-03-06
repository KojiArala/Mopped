using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class clickInv : EventManager {

	protected override void Start () {
		// protected or public to use override (child), call base.Start() to cascade them
		base.Start ();
	} // END Start

	protected override void Update () {
		base.Update ();
	} // END Update
	
	// Uses the item
	public void clicked() {
		if (Input.GetMouseButton (0)) {
			// left button clicked...
			if(iconMoving) {
				base.moveSlotBack();
			}
		} // END left button click IF
		
		if (Input.GetMouseButton (1)) {
			// right button clicked...display info
		} // END right button click IF
		
		if (Input.GetMouseButton (2)) {
			// middle button clicked
		} // END middle button click IF
	} // END clicked
	
} // END class
