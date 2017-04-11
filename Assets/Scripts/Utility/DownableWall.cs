using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownableWall : MonoBehaviour,IInteractable {
	private Shifter shifter;
	public bool locked = false;
	private bool isDown=false;
	// Use this for initialization
	void Start(){

		shifter = GetComponent<Shifter>();
		shifter.direct = transform.up * -1;
		shifter.shiftDist = 3f;
	}
	void Down(){
		if (locked) {
			return;
		}
		isDown=true;
		shifter.Shift ();
	}
	void Up(){
		if (locked) {
			return;
		}
		isDown = false;
		shifter.Unshift ();
	}
	public void OnInteract(){
		Debug.Log ("Interacting with downable wall.");
		shifter.ToggleWithTransition ();
	}
}
