using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class keypadNumberClick : EventManager {
	public void clickThisNumber () {
		if (this.name != "clear" && this.name != "close") {
			audioSource.clip = keypadSound;
			audioSource.Play();

			if(tappedCode != "-42") tappedCode += this.name;
			else tappedCode = this.name;
		}
		else if (this.name == "close") {
			tappedCode = "close";
		}
		else {
			tappedCode = "-42";
		}
	}
}
