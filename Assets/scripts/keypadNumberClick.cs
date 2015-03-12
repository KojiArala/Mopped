using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class keypadNumberClick : EventManager {
	public void clickThisNumber () {
		if (this.name != "x") {
			// method ends too quick so sound isn't played
//			audioSource = (AudioSource)gameObject.AddComponent("AudioSource");
//			audioSource.clip = keypadSound;
//			audioSource.loop = false;
//			audioSource.volume = 1.0f;
//			if(!audioSource.isPlaying) audioSource.Stop();

			// trying to play from "global" keyAudioSource in EventManager but not working
			keyAudioSource.Play();

			if(tappedCode != "-42") tappedCode += this.name;
			else tappedCode = this.name;
		}
		else {
			tappedCode = "-42";
		}
	}
}
