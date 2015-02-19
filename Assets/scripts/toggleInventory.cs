using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class toggleInventory : EventManager {
	public void toggleInv () {
		if (base.invBox.activeSelf) {
			base.invBox.SetActive(false);
		}
		else {
			base.invBox.SetActive(true);
		}
	}
}
