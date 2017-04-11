using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof (PlaneManager))]
public class PlaneManagerEditor : Editor {
	PlaneManager m_Target;
	// Use this for initialization
	public override void OnInspectorGUI ()
	{
		m_Target = (PlaneManager)target;
		DrawDefaultInspector ();
		DrawCustomInspector ();
	}

	private void DrawCustomInspector(){
		GUILayout.Space (10);
		GUILayout.Label ("Operations",EditorStyles.boldLabel);
		if (GUILayout.Button ("Fix Planes")) {
			m_Target.FixPlanes ();
		}
		GUILayout.Space (10);
		if (GUILayout.Button ("Init Maze")) {
			m_Target.Init ();
		}
		GUILayout.Space (10);
		GUILayout.BeginHorizontal ();
		{	
			GUILayout.Space (80);
			if (GUILayout.Button ("Rotate Forward",GUILayout.Width(100),GUILayout.Height(40))) {
				m_Target.RotateMazeForward ();
				Undo.RecordObject (m_Target,"Rotate Forward");
			}
		}
		GUILayout.EndHorizontal ();
		GUILayout.Space (10);
		GUILayout.BeginHorizontal ();
		{	
			if (GUILayout.Button ("Rotate Left",GUILayout.Width(100),GUILayout.Height(40))) {
				m_Target.RotateMazeLeft ();
			}
			GUILayout.Space (60);
			if (GUILayout.Button ("Rotate Right",GUILayout.Width(100),GUILayout.Height(40))) {
				m_Target.RotateMazeRight ();
			}

		}
		GUILayout.EndHorizontal ();
		GUILayout.Space (10);
		GUILayout.BeginHorizontal ();
		{	
			GUILayout.Space (80);
			if (GUILayout.Button ("Rotate Back",GUILayout.Width(100),GUILayout.Height(40))) {
				m_Target.RotateMazeBack ();
			}
		}
		GUILayout.EndHorizontal ();
		GUILayout.Space (10);

		if (GUILayout.Button ("Reset Planes")) {
			m_Target.ResetPlanes ();
		}
		if (GUILayout.Button ("Unfold")) {
			m_Target.UnfoldSurroundingPlanes ();
		}
		if (GUILayout.Button ("Fold")) {
			m_Target.FoldSurroundingPlanes ();
		}
		if (GUILayout.Button ("Toggle Folding")) {
			m_Target.ToggleFold ();
		}

	}
}
