using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class hideOverlay : EventManager {
	public void hideMe() {
		string tempObjName = this.name;
		if(tempObjName != "" && tempObjName[0] == '~') tempObjName = tempObjName.Substring(1);

		if(!slotPicked) this.transform.position = new Vector2 (this.transform.position.x - base.offset, this.transform.position.y);
		if(tempObjName == useItemWith) {
			base.useItem();
		}
	}

	public void showMe() {
		this.transform.position = new Vector2 (this.transform.position.x + base.offset, this.transform.position.y);
	}


//	void OnMouseDown () {
//		// only for left click
//	}

}
