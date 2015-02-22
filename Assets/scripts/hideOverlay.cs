using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class hideOverlay : MonoBehaviour {
	public void hideMe() {
		this.transform.position = new Vector2 (this.transform.position.x - 1000, this.transform.position.y);
	}

//	void OnMouseDown () {
//		// only for left click
//	}

}
