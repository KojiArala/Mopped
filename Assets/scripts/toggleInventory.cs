using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class toggleInventory : EventManager {

	protected override void Start () {
		// protected or public to use override (child), call base.Start() to cascade them
		base.Start ();
	}
	
	protected override void Update () {

		if (Input.GetMouseButtonDown(0)) {
			// only toggle if bag GUI sprite was clicked ... "inventory_image"
			//can't get name of the clicked object yet
			
			//RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
			//Debug.Log(hit.transform.gameObject.name);
			
			//Debug.Log(EventSystem.current.currentSelectedGameObject.GetComponent<Image>().name);
			
			//Debug.Log(EventSystem.current.currentSelectedGameObject.ToString());
			//Debug.Log(EventSystem.current.firstSelectedGameObject);
			Debug.Log(EventSystem.current.currentSelectedGameObject);

			///GetComponent<Image>();

			if (EventSystem.current.IsPointerOverGameObject()) {
				if (base.invBox.activeSelf) {
					base.invBox.SetActive(false);
				}
				else {
					base.invBox.SetActive(true);
				}
			}
		}
	}
}
