using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shifter : MonoBehaviour {
	public float shiftDist;
	// Use this for initialization
	public Vector3 direct;
	private bool isShifted=false;
	private bool transitionLock=false;
	void Awake(){
		shiftDist = 5;
		direct = transform.up;
	}
	void Start () {
		
	}
	public void ToggleWithTransition(){
		if (isShifted) {
			StartCoroutine(UnshiftWithTransition ());
		} else {
			StartCoroutine(ShiftWithTransition ());
		}
	}
	public void Toggle(){
		if (isShifted) {
			Unshift ();
		} else {
			Shift ();
		}
	}
	public void Shift(){
		Debug.Log ("Shifting!");
		if (isShifted) {
			return;
		} 
		isShifted = true;
		transform.Translate (direct*shiftDist*1,Space.World);
	}
	public void Unshift(){
		Debug.Log ("Unshifting!");
		if (!isShifted) {
			return;
		} 
		isShifted = false;
		transform.Translate (direct*shiftDist*-1,Space.World);
	}
	public IEnumerator ShiftWithTransition(float t = 1.0f){
		Debug.Log ("Shifting with transition!");
		if (isShifted || transitionLock) {
			
		} else {
			transitionLock = true;
			float timespan = 0f;
			while (timespan < t) {
				timespan += Time.deltaTime;
				transform.Translate (direct*shiftDist*1*Time.deltaTime/t,Space.World);
				yield return null;
			}
			isShifted = true;
			transitionLock = false;
		}
	}
	public IEnumerator UnshiftWithTransition(float t = 1.0f){
		Debug.Log ("Shifting with transition!");
		if (!isShifted || transitionLock) {

		} else {
			transitionLock = true;
			float timespan = 0f;
			while (timespan < t) {
				timespan += Time.deltaTime;
				transform.Translate (direct*shiftDist*-1*Time.deltaTime/t,Space.World);
				yield return null;
			}
			isShifted = false;
			transitionLock = false;
		}
	}


	// Update is called once per frame
	void Update () {
		
	}
}
