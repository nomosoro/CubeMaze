using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	// Use this for initialization
	public GameObject AimHintText;
	private string curAimHint="";
	private bool isAimHinting=false;
	private bool forceEndFullingAimHint=false;
	private bool forceEndZeroingAimHint=false;
	void Awake(){
		
	}
	public void ShowAimHint(string hintString){
		if (curAimHint == hintString && isAimHinting) {
			return;
		}
		Debug.Log("Showing");
		isAimHinting = true;
		curAimHint = hintString;
		Text textref=AimHintText.GetComponent<Text>();
		textref.text = hintString;
		forceEndZeroingAimHint = true;
		StartCoroutine (FadeTextToFullAlpha(textref));
	}
	public void HideAimHint(){
		Debug.Log("Hiding");
		if (!isAimHinting) {
			return;
		}
		isAimHinting = false;
		Text textref=AimHintText.GetComponent<Text>();
		forceEndFullingAimHint = true;
		StartCoroutine (FadeTextToZeroAlpha(textref));
	}

	public IEnumerator FadeTextToFullAlpha(Text i,float t=1.0f)
	{
		forceEndFullingAimHint = false;
		while (i.color.a < 1.0f & !forceEndFullingAimHint)
		{
			Debug.Log("Fading to FULL");
			i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
			yield return null;
		}
	}

	public IEnumerator FadeTextToZeroAlpha(Text i,float t=1.0f)
	{
		forceEndZeroingAimHint = false;
		while (i.color.a > 0.0f && !forceEndZeroingAimHint)
		{
			Debug.Log("Fading to ZERO");
			i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
			yield return null;
		}
	}
}
