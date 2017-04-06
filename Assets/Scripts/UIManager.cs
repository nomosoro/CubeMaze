using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	// Use this for initialization
	public GameObject AimHintText;
	private string curAimHint="";
	private bool isAimHinting=false;
	void Awake(){
		
	}
	public void ShowAimHint(string hintString){
		if (curAimHint == hintString&&isAimHinting) {
			return;
		}
		isAimHinting = true;
		curAimHint = hintString;
		Text textref=AimHintText.GetComponent<Text>();
		textref.text = hintString;
		StartCoroutine (FadeTextToFullAlpha(1.0f,textref));
	}
	public void HideAimHint(){
		if (!isAimHinting) {
			return;
		}
		isAimHinting = false;
		Text textref=AimHintText.GetComponent<Text>();
		StartCoroutine (FadeTextToZeroAlpha(1.0f,textref));
	}

	public IEnumerator FadeTextToFullAlpha(float t, Text i)
	{
		i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
		while (i.color.a < 1.0f)
		{
			i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
			yield return null;
		}
	}

	public IEnumerator FadeTextToZeroAlpha(float t, Text i)
	{
		i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
		while (i.color.a > 0.0f)
		{
			i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
			yield return null;
		}
	}
}
