using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class clickedObject : EventManager {
	void OnMouseDown () {
		if(!EventSystem.current.IsPointerOverGameObject()){
			Debug.Log ("not over game object");
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			Physics.Raycast(ray, out hit);
			if (base.stationary) {
				base.moveTo = hit.point;
				base.stationary = false;
			}
		}
		else {
			Debug.Log ("over game object");
		}
	}
}
