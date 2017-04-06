using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownableWall : MonoBehaviour,IAimable,IInteractable {
	private Shifter shifter;
	public bool locked = false;
	private GameObject UIManager;
	private UIManager uiManager;
	private bool isDown=false;
	LayerMask mask=1;
	// Use this for initialization
	void Start(){
		UIManager = GameObject.FindWithTag ("UIManager");
		uiManager = UIManager.GetComponent<UIManager> ();
		shifter = GetComponent<Shifter>();
		shifter.direct = transform.up * -1;
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
	public void OnAim(){
		uiManager.ShowAimHint ("Door.");

	}

	public void OnInteract(){
		Debug.Log ("Interacting! ");
		shifter.Toggle ();
	}
}
