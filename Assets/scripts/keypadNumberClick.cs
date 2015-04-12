using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class keypadNumberClick : EventManager {
	public void clickThisNumber () {
		keypadSource.clip = keypadSound;
		keypadSource.Play();
		if (this.name != "clear" && this.name != "close") {
			if(tappedCode != "-42") tappedCode += this.name;
			else tappedCode = this.name;
		}
		else if (this.name == "close") {
			tappedCode = "close";
		}
		else {
			// clear
			tappedCode = "-42";
		}
	}
}
