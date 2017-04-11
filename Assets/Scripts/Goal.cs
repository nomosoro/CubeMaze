using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour, IInteractable {

	private UIManager uiManager;
	// Use this for initialization
	void Start ()
	{
		uiManager = GameObject.FindWithTag ("UIManager").GetComponent<UIManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void OnInteract(){
		
	}
}
