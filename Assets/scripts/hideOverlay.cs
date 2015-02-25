using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class hideOverlay : EventManager {
	public void hideMe() {
		this.transform.position = new Vector2 (this.transform.position.x - base.offset, this.transform.position.y);
	}

	public void showMe() {
		this.transform.position = new Vector2 (this.transform.position.x + base.offset, this.transform.position.y);
	}

//	void OnMouseDown () {
//		// only for left click
//	}

}
