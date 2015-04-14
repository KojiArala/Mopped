using UnityEngine;
using System.Collections;

public class voiceMonsterBackground : EventManager {
	protected override void Start() {
		// protected or public to use override (child), call base.Start() to cascade them
		base.Start();
		StartCoroutine(randomRepeat());
	} // END Start

	IEnumerator randomRepeat() {
		yield return new WaitForSeconds(Random.Range(5, 25));
		if(gameStarted) {
			monsterBackground.clip = monsterSound;
			monsterBackground.Play();
		}
		StartCoroutine(randomRepeat());
	}
	

}
