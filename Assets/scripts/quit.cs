using UnityEngine;
using System.Collections;

public class quit : EventManager {
	public void quitGame() {
		audioSource.clip = quitSound;
		audioSource.Play ();

		StartCoroutine(pauseMe());
	}

	IEnumerator pauseMe(){
		yield return new WaitForSeconds(quitSound.length); // Wait till sound is played
		//yield return new WaitForSeconds(3f);
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#else
		Application.Quit();
		#endif
	}
}
