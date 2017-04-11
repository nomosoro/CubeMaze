using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof (PlaneController))]
public class PlaneControllerEditor : Editor {
	PlaneController m_Target;
	// Use this for initialization
	public override void OnInspectorGUI ()
	{
		m_Target = (PlaneController)target;
		DrawDefaultInspector ();
		DrawCustomInspector ();
	}

	private void DrawCustomInspector(){
		GUILayout.Space (10);
		GUILayout.Label ("Operations",EditorStyles.boldLabel);
		if (GUILayout.Button ("Reset Plane")) {
			m_Target.ResetPlane ();
		}
	}
}
