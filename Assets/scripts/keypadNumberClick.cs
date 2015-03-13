using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class keypadNumberClick : EventManager {
	public void clickThisNumber () {
		if (this.name != "x") {
			audioSource.clip = keypadSound;
			audioSource.Play();

			if(tappedCode != "-42") tappedCode += this.name;
			else tappedCode = this.name;
		}
		else {
			tappedCode = "-42";
		}
	}
}
