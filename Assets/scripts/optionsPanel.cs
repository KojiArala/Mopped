using UnityEngine;
using System.Collections;

public class optionsPanel : EventManager {
	static GameObject options;

	protected override void Start() {
		// protected or public to use override (child), call base.Start() to cascade them
		base.Start();

		options = GameObject.Find("options");
	} // END Start

	public void hideOptions() {
		options.transform.position = new Vector2 (this.transform.position.x - base.offset, (Screen.height/2));
	}
	
	public void showOptions() {
		options.transform.position = new Vector2 ((Screen.width/2), (Screen.height/2));
	}

}
