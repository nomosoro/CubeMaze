using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoBehaviour {
	private Vector3 defaultPos;
	private Quaternion defaultRot;
	private bool haveDefault = false;
	public void RecordDefaultTransform(){
		defaultPos = transform.position;
		defaultRot = transform.rotation;
		haveDefault = true;
	}
	public void ResetTransform(){
		if (!haveDefault) {
			Debug.LogWarning ("You are reseting a plane without a default transform");
			return;
		}
		transform.position = defaultPos;
		transform.rotation = defaultRot;
	}
}
