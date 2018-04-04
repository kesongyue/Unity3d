using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mercury : MonoBehaviour {

	Vector3 Sun = new Vector3(0,0,0);
	// Update is called once per frame
	void Update () {
		Vector3 axis = new Vector3(0, 1, 1);
		this.transform.RotateAround(Sun, axis, 20 * Time.deltaTime);
		this.transform.RotateAround(this.transform.position, Vector3.up, 1);

	}
}
