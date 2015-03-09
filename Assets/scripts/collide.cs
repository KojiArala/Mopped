using UnityEngine;
using System.Collections;

public class collide : EventManager {
	Vector3 lastPos;

	protected override void Start () {
		// protected or public to use override (child), call base.Start() to cascade them
		base.Start();
		lastPos = m2.transform.position;
	}

	//rigidbody.angularVelocity = Vector3.zero;
	protected override void Update () {
		base.Update();

	} // END Update
	
	void OnTriggerEnter(Collider thisObject) {
		stationary = true;
		lastPos = m2.transform.position;

		base.moveTo = m2.transform.position;
		base.lastPosition = m2.transform.position;
		m2.transform.Translate(new Vector3(0, 0, 0));
		m2.transform.position = base.moveTo;
		Debug.Log ("STOP " + stationary);
	} // END OnTriggerEnter

	void OnTriggerStay(Collider thisObject) {
		stationary = true;
		m2.transform.position = lastPos;
	} // END OnTriggerStay

	void OnTriggerExit(Collider thisObject) {
		// exit stuff goes here
	} // END OnTriggerExit

}
