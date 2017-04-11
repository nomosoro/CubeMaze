using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	// Use this for initialization
	public GameObject AimHintGameObject;
	private const float HintTransition = 1.0f;
	private const float HintStay = 5.0f;
	private Text hintText;
	private string curAimHint="";
	private bool isAimHinting=false;
	private bool aimHintDisabled=false;
	private bool forceEndFullingAimHint=false;
	private bool forceEndZeroingAimHint=false;
	public struct Dialogue{
		public string text;
		public float playTime;
	}
	private List<Dialogue> dialogueList; 
	void Awake(){
		dialogueList = new List<Dialogue> ();
	}
	void Start(){
		hintText=AimHintGameObject.GetComponent<Text>();
		hintText.color=ColorAlpha( hintText.color, 0);
	}
	public void ShowAimHint(string text){
		if (curAimHint == text && isAimHinting) {
			return;
		}
		if (aimHintDisabled) {
			return;
		}
		Debug.Log("Showing");
		isAimHinting = true;
		curAimHint = text;
		hintText.text = text;
		forceEndZeroingAimHint = true;
		forceEndFullingAimHint = false;
		StartCoroutine (FadeTextToFullAlpha(hintText));
	}
	public void HideAimHint(){
		if (!isAimHinting) {
			return;
		}
		if (aimHintDisabled) {
			return;
		}
		Debug.Log("Hiding");
		isAimHinting = false;
		forceEndFullingAimHint = true;
		forceEndZeroingAimHint = false;
		StartCoroutine (FadeTextToZeroAlpha(hintText));
	}
	public IEnumerator FadeTextToFullAlpha(Text i,float t = HintTransition)
	{
		while (i.color.a < 1.0f & !forceEndFullingAimHint)
		{
			Debug.Log("Fading to Full");
			i.color=ColorAlpha( i.color, i.color.a + (Time.deltaTime / t));
			//i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
			yield return null;
		}
	}
	public IEnumerator FadeTextToZeroAlpha(Text i,float t = HintTransition)
	{
		while (i.color.a > 0.0f && !forceEndZeroingAimHint)
		{
			Debug.Log("Fading to Zero");
			//i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
			i.color=ColorAlpha( i.color, i.color.a-(Time.deltaTime / t));
			yield return null;
		}
	}

	public void Hint(string text){
		aimHintDisabled = true;
		forceEndZeroingAimHint = true;
		forceEndFullingAimHint = true;
		hintText.text = text;
		StartCoroutine (DisplayDialogue(hintText,5.0f));
	}
	public IEnumerator DisplayDialogue(Text i,float transitionTime = HintTransition, float stayTime = HintStay)
	{
		
		while (i.color.a < 1.0f)
		{
			Debug.Log("Fading to Full in DisplayText");
			//i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
			i.color=ColorAlpha( i.color, i.color.a + (Time.deltaTime / transitionTime));
			yield return null;
		}
		yield return new WaitForSeconds (stayTime);
		while (i.color.a > 0.0f)
		{
			Debug.Log("Fading to Zero in DisplayText");
			//i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
			i.color=ColorAlpha( i.color, i.color.a - (Time.deltaTime / transitionTime));
			yield return null;
		}
		aimHintDisabled = false;
	}




	private Color ColorAlpha(Color color, float alpha){
		color = new Color (color.r,color.g,color.b,alpha);
		return color;
	}
}
