using UnityEngine;
using System.Collections;

public class proxDoor : EventManager {
	// old method kept for reference
	//private object[] objects = GameObject.FindGameObjectsWithTag("obj");
	private float delta;
	private Vector3 thisPos;
	private bool locked = true;
	private string action = "";
	private float proximity = 10f;
	private bool animating = false;
	private float zRotate = 1f;
	//private bool animateToggle = false;
	private Vector3 direction = Vector3.up;
	private float doorSpeed = 8f;
	private Vector3 doorMin;
	private Vector3 doorMax;
	public string doorName;

	protected override void Start () {
		// protected or public to use override (child), call base.Start() to cascade them
		base.Start ();
		thisPos = this.transform.position;
		doorMin = new Vector3 (thisPos.x, 3, thisPos.z);
		doorMax = new Vector3 (thisPos.x, 15, thisPos.z);
	}

	// Update is called once per frame
	protected override void Update () {
		base.Update ();
		// if statement to do check only if object is visible in camera
		delta = Vector3.Distance (m2Pos, thisPos);
		if(delta < 7.5f) {
			// not stopping lerp
			//Debug.Log ("STOOOOOOP!!!");
			//base.stationary = true;
			//base.moveTo = base.m2.transform.position;
			//base.m2.transform.position = new Vector3(0, m2.transform.position.y, 0);
		}

		if (delta < proximity && locked == false && !animating) {
			//Debug.Log ("magnatude (delta) between " + this.name + " and " + m2.name + " = " + delta);
			animating = true;
			locked = true;
		}
		
		if(action == "open") {
			if(transform.position.y >= doorMax.y && direction == Vector3.up) {
				animating = false;
				this.transform.position = doorMax;
				direction = Vector3.down;
			}
			else if(transform.position.y <= doorMin.y && direction == Vector3.down) {
				animating = false;
				this.transform.position = doorMin;
				direction = Vector3.up;
			}
			if(animating) {
				transform.Translate(direction * doorSpeed * Time.deltaTime);
			}
		}
		else if(action == "rotate") {
			if(transform.rotation.z > 0.7f) {
				transform.Rotate (0, 0, 0.7f);
				animating = false;
			}
			else if (transform.rotation.z < 0.5f){
				transform.Rotate (0, 0, 0.5f);
				animating = false;
			}
			if(animating) {
				transform.Rotate (0, 0, transform.rotation.z + zRotate);
			}
		}
		else {
			//Debug.Log ("ERROR: no action listed");
		}

	} // END Update

	public void unlockDoor(string doorAction) {
		locked = false;
		action = doorAction;

		audioSource = (AudioSource)gameObject.AddComponent("AudioSource");
		if(doorAction == "open") audioSource.clip = doorOpenSound;
		else audioSource.clip = lockerOpen;
		audioSource.loop = false;
		audioSource.volume = 1.0f;
		if(!audioSource.isPlaying) audioSource.Stop();
		audioSource.Play();
	} // END unlockDoor

} // END class










