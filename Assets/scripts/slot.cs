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
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		Physics.Raycast(ray, out hit);
		if(hit.transform.gameObject != null) thisObject = hit.transform.gameObject;
		
		if (Input.GetMouseButton (0) && !EventSystem.current.IsPointerOverGameObject()) {
			// left button clicked;
			Debug.Log("left " + thisObject.name);
		}

		if (Input.GetMouseButton (1) && !EventSystem.current.IsPointerOverGameObject()) {
			// right button clicked;
			Debug.Log("right " + thisObject.name);
		}

		if (Input.GetMouseButton (2) && !EventSystem.current.IsPointerOverGameObject()) {
			// middle button clicked;
			Debug.Log("center " + thisObject.name);
		}
	}

	// Uses the item
	public void Use() {
		// stuff goes here
		Debug.Log ("WOOT");
	}

	public void displayInfo() {
		Debug.Log ("hiya");
	}
}
