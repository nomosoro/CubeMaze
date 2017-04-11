using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoBehaviour {
	public Vector3 defaultPos;
	public Quaternion defaultRot;
	private bool haveDefault = false;
	public void RecordDefaultTransform(){
		defaultPos = transform.position;
		defaultRot = transform.rotation;
		haveDefault = true;
	}
	public void ResetPlane(){
		if (!haveDefault) {
			Debug.LogWarning ("You are reseting a plane without a default transform");
			return;
		}
		transform.position = defaultPos;
		transform.rotation = defaultRot;
	}
}
