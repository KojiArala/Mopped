using UnityEngine;
using System.Collections;

public class m2Move : MonoBehaviour {
	private float moveSpeed = 10f;
	private float turnSpeed = 100f;

	void Start () {
	
	}
	
	void Update () {
		// GetKey, GetKeyDown, GetKeyUp
		if(Input.GetKey(KeyCode.UpArrow) || Input.GetKey("w"))
			transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
		
		if(Input.GetKey(KeyCode.DownArrow) || Input.GetKey("s"))
			transform.Translate(-Vector3.forward * moveSpeed * Time.deltaTime);
		
		if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey("a"))
			transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);
		
		if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey("d"))
			transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);
	}
}
