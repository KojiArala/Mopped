using UnityEngine;
using System.Collections;

public class item : EventManager {
	public Sprite spriteNorm;
	public Sprite spriteHigh;
	public string itemName;
	public string itemDescription;
	public string useWith;

	public int maxSize; // The max amount of times the item can stack

	// Uses the item
	public void Use() {
		// stuff goes here
	}
}
