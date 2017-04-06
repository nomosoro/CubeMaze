using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shifter : MonoBehaviour {
	public float shiftDist;
	// Use this for initialization
	public Vector3 direct;
	private bool isShifted=false;
	void Awake(){
		shiftDist = 5;
		direct = transform.up;
	}
	void Start () {
		
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
	// Update is called once per frame
	void Update () {
		
	}
}
