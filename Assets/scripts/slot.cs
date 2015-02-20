using UnityEngine;
using System.Collections;

public class slot : EventManager {
	public Sprite spriteNorm;
	public Sprite spriteHigh;
	public string itemName;
	public string itemDescription;
	public string useWith;
	public bool slotEmpty;

	public int maxSize; // The max amount of times the item can stack
	
	protected override void Start () {
		// protected or public to use override (child), call base.Start() to cascade them
		base.Start ();
		slotEmpty = true;
	}

	// Uses the item
	public void Use() {
		// stuff goes here
	}

	public void displayInfo() {

	}
}
