using UnityEngine;
using System.Collections;

public class AimHint : MonoBehaviour,IAimable
{
	public string hintText;
	private UIManager uiManager;
	// Use this for initialization
	void Start ()
	{
		uiManager = GameObject.FindWithTag ("UIManager").GetComponent<UIManager> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
	public void OnAim(){
		uiManager.ShowAimHint (hintText);
	}
	public void OnAimOut(){
		uiManager.HideAimHint ();
	}
}

