using UnityEngine;
using System.Collections;

public class volumeControl : EventManager {
	public void changeMusicVolume() {
		backgroundMusic.volume = musicSlider.value;
	}
	
	// Update is called once per frame
	public void changeSoundVolume() {
		audioSource.volume = soundSlider.value;
		keypadSource.volume = soundSlider.value;
		m2MoveSound.volume = soundSlider.value;
		objAudioSource.volume = soundSlider.value;
		monsterBackground.volume = soundSlider.value;
		womanVoice.volume = soundSlider.value;
	}
}
