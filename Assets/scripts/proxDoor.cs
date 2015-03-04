using UnityEngine;
using System.Collections;

public class proxDoor : EventManager {
	// old method kept for reference
	//private object[] objects = GameObject.FindGameObjectsWithTag("obj");
	private float delta;
	private Vector3 thisPos;
	private bool locked = true;
	private float proximity = 10f;
	private bool animating = false;
	//private bool animateToggle = false;
	private Vector3 direction = Vector3.up;
	private float doorSpeed = 12f;
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
			//base.m2.transform.position = Vector3.zero;
		}

		if (delta < proximity && locked == false && !animating) {
			//Debug.Log ("magnatude (delta) between " + this.name + " and " + m2.name + " = " + delta);
			animating = true;
			locked = true;
		}

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
		else if(animating) {
			transform.Translate(direction * doorSpeed * Time.deltaTime);
		}
		//Debug.Log(transform.position.y + " ~ " + doorMin + " ~ " + doorMax + " ~ " + animating + " ~ " + direction);

	} // end override

	public void unlockDoor() {
		locked = false;
	}


}










