using UnityEngine;
using System.Collections;

public class startGame : EventManager {
	private GameObject startOverlay;
	private Vector2 startOverlayPosition;


	protected override void Start() {
		// protected or public to use override (child), call base.Start() to cascade them
		base.Start();
		startOverlay = GameObject.Find ("startScreen");
		startOverlayPosition = startOverlay.transform.position;

	} // END Start

	public void startTheGame () {
		startOverlay.transform.position = new Vector2 (startOverlay.transform.position.x - 3000, startOverlay.transform.position.y);
	}
	

}
