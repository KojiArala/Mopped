﻿using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class toggleInventory : EventManager {
	public Sprite up;
	public Sprite down;

	public void toggleInv () {
		if (base.invTab.transform.position.y == 75) {
			base.invTab.GetComponent<Image>().sprite = base.invTab.GetComponent<toggleInventory>().up;
			base.invTab.transform.position = new Vector2 (base.invTab.transform.position.x, base.invTab.transform.position.y - 60);
		}
		else {
			base.invTab.GetComponent<Image>().sprite = base.invTab.GetComponent<toggleInventory>().down;
			base.invTab.transform.position = new Vector2 (base.invTab.transform.position.x, base.invTab.transform.position.y + 60);
		}

		// OLD hide toggle
//		if (base.invBox.activeSelf) {
//			base.invBox.SetActive(false);
//		}
//		else {
//			base.invBox.SetActive(true);
//		}
	}
}
