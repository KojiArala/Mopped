using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class toggleInventory : EventManager {
	protected override void Update () {
		if (Input.GetMouseButtonDown(0)) {
			if (EventSystem.current.IsPointerOverGameObject()) {
				//RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
				//Debug.Log(hit.transform.gameObject.name);

				//Debug.Log(EventSystem.current.currentSelectedGameObject.ToString());
				//Debug.Log(EventSystem.current.firstSelectedGameObject);
				Debug.Log(EventSystem.current.currentSelectedGameObject);

				if (textBoxMe.activeSelf) {
					textBoxMe.SetActive(false);
				}
				else {
					textBoxMe.SetActive(true);
				}
			}
		}
	}
}
