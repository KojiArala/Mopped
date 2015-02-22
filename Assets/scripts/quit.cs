using UnityEngine;
using System.Collections;

public class quit : MonoBehaviour {
	public void quitGame() {
		#if UNITY_EDITOR
		// set the PlayMode to stop
		Debug.Log("quitting game");
		#else
		Application.Quit();
		#endif
	}
}
