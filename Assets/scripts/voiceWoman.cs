using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class voiceWoman : EventManager {
	List<string> womanText = new List<string>();

	protected override void Start() {
		// protected or public to use override (child), call base.Start() to cascade them
		base.Start();
		loadWomanData();
		StartCoroutine(womanRepeat());
	} // END Start
	
	IEnumerator womanRepeat() {
		yield return new WaitForSeconds(45);
		if(gameStarted) {
			int useThis = Random.Range(0, 2);
			base.displayMessage ("Woman Voice " + useThis);

			womanVoice.clip = womanSounds[useThis];
			womanVoice.Play();
		}
		StartCoroutine(womanRepeat());
	}

	void loadWomanData() {
		womanSounds.Add ((AudioClip)Resources.Load ("Sounds/WetPickup"));
		womanText.Add ("Woman message 1");
		womanSounds.Add ((AudioClip)Resources.Load ("Sounds/MetalPickup"));
		womanText.Add ("Woman message 2");
		womanSounds.Add ((AudioClip)Resources.Load ("Sounds/PlasticPickup"));
		womanText.Add ("Woman message 3");

	}

}
