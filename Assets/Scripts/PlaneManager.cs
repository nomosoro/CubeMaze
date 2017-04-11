using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneManager : MonoBehaviour {

	public float planeLength;
	public float planeThickness;
	public Transform maze;
	public Transform planeA,planeA_,planeB,planeB_,planeC,planeC_;
	private Transform[] planes;
	public bool folded = true;
	private Transform curPlane;
	private Dictionary<Transform,Transform[]> surroundingDict;
	// Use this for initialization
	public void Init(){
		//It must be executed in folded mode;
		planes = new Transform[]{planeA,planeA_,planeB,planeB_,planeC,planeC_};

		curPlane = planeA;
		if (planeB.position.y * planeB.position.x != 0) {
			Debug.LogWarning ("Cant init maze when planes are not in init position");
			return;
		}
		RelatePlanes ();
		RecordPlanes ();
		folded = true;
	}
	public void FixPlanes(){
		planeA.position = new Vector3 (0f,26f,0f);
		planeA.rotation = Quaternion.Euler (0f,0f,0f);
		planeB.position = new Vector3 (0f,0f,-26f);
		planeB.rotation = Quaternion.Euler (-90f,0f,0f);
		planeC.position = new Vector3 (-26f,0f,0f);
		planeC.rotation = Quaternion.Euler (0f,0f,90f);
		planeA_.position = new Vector3 (0f,-26f,0f);
		planeA_.rotation = Quaternion.Euler (0f,90f,180f);
		planeB_.position = new Vector3 (0f,0f,26f);
		planeB_.rotation = Quaternion.Euler (90f,0f,0f);
		planeC_.position = new Vector3 (26f,0f,0f);
		planeC_.rotation = Quaternion.Euler (0f,0f,-90f);
	}
	void Awake(){
		planes = new Transform[]{planeA,planeA_,planeB,planeB_,planeC,planeC_};
		curPlane = planeA;
		RelatePlanes ();
		RecordPlanes ();
	}
	void Start () {
		ResetPlanes ();
		UnfoldSurroundingPlanes ();
	}

	public void UpdateCurrentVisitingPoint(Vector3 vp){
		if (Mathf.Abs (vp.x - maze.position.x) <= planeLength / 2 && Mathf.Abs (vp.z - maze.position.z) <= planeLength / 2) {
			return;
		}
		if(vp.x - maze.position.x > planeLength / 2){
			RotateMazeRight ();
			return;
		}
		if(maze.position.x - vp.x > planeLength / 2){
			RotateMazeLeft ();
			return;
		}
		if(vp.z - maze.position.z > planeLength / 2){
			RotateMazeForward ();
			return;
		}
		if( maze.position.z - vp.z > planeLength / 2){
			RotateMazeBack ();
			return;
		}
	}

	public void ToggleFold(){
		if (folded) {
			UnfoldSurroundingPlanes ();
		} else {
			FoldSurroundingPlanes ();
		}
	}
	public void UnfoldSurroundingPlanes(){
		if (folded == false) {
			return;
		}
		Transform[] surroundingPlanes;
		if (surroundingDict.TryGetValue (curPlane, out surroundingPlanes)) {
			foreach (Transform plane in surroundingPlanes) {
				UnfoldPlane (plane);
			}
		}
		folded = false;
	}
	public void FoldSurroundingPlanes(){
		if (folded == true) {
			return;
		}
		Transform[] surroundingPlanes;

		if (surroundingDict.TryGetValue (curPlane, out surroundingPlanes)) {
			foreach (Transform plane in surroundingPlanes) {
				FoldPlane (plane);
			}
		}
		folded = true;
	}
	public void RecordPlanes(){
		foreach (Transform plane in planes) {
			plane.GetComponent<PlaneController> ().RecordDefaultTransform ();
		}
	}
	public void ResetPlanes(){
		maze.position=Vector3.zero;
		maze.rotation = Quaternion.identity;
		foreach (Transform plane in planes) {
			plane.GetComponent<PlaneController> ().ResetPlane ();
		}
		folded = true;
	}
	void UnfoldPlane(Transform plane){
		Vector3 pointingDir = plane.position-maze.position;

		pointingDir.y = 0;
		pointingDir = pointingDir.normalized * planeLength / 2;
		Vector3 rotateAxis = Quaternion.AngleAxis (-90, Vector3.up) * (pointingDir);
		Vector3 rotatePoint = maze.position + pointingDir + Vector3.up * (planeLength)/2;
		plane.RotateAround (rotatePoint,rotateAxis,90);
	}

	void FoldPlane(Transform plane){
		Vector3 pointingDir = plane.position - maze.position;

		pointingDir.y = 0;
		pointingDir = pointingDir.normalized * planeLength / 2;
		Vector3 rotateAxis = Quaternion.AngleAxis (-90, Vector3.up) * (pointingDir);
		Vector3 rotatePoint = maze.position + pointingDir + Vector3.up * (planeLength)/2;
		plane.RotateAround (rotatePoint,rotateAxis,-90);
	}
	void UpdateCurPlane(){
		foreach(Transform plane in planes){
			if ((plane.position - maze.position).normalized == Vector3.up) {
				curPlane = plane;
			}
		}
	}
	public void RotateMazeForward(){
		FoldSurroundingPlanes ();
		maze.RotateAround(maze.position,Vector3.right,-90);
		maze.Translate (0, 0, planeLength,Space.World);
		UpdateCurPlane ();
		UnfoldSurroundingPlanes ();
	}
	public void RotateMazeBack(){
		FoldSurroundingPlanes ();
		maze.RotateAround(maze.position,Vector3.right,90);
		maze.Translate (0, 0, -planeLength,Space.World);
		UpdateCurPlane ();
		UnfoldSurroundingPlanes ();
	}
	public void RotateMazeRight(){
		FoldSurroundingPlanes ();
		maze.RotateAround(maze.position,Vector3.forward,90);
		maze.Translate (planeLength, 0, 0,Space.World);
		UpdateCurPlane ();
		UnfoldSurroundingPlanes ();
	}
	public void RotateMazeLeft(){
		FoldSurroundingPlanes ();
		maze.RotateAround(maze.position,Vector3.forward,-90);
		maze.Translate (-planeLength, 0, 0,Space.World);
		UpdateCurPlane ();
		UnfoldSurroundingPlanes ();
	}	
	void RelatePlanes(){
		surroundingDict=new Dictionary<Transform,Transform[]>();
		RelatePlane (planeA,planeC_,planeB,planeC,planeB_,planeA_);
		RelatePlane (planeC_,planeA_,planeB,planeA,planeB_,planeC);
		RelatePlane (planeA_,planeC_,planeB_,planeC,planeB,planeA);
		RelatePlane (planeC,planeA,planeB,planeA_,planeB_,planeC_);
		RelatePlane (planeB,planeC_,planeA_,planeC,planeA,planeB_);
		RelatePlane (planeB_,planeC_,planeA,planeC,planeA_,planeB);
	}
	void RelatePlane(Transform p,Transform t,Transform r,Transform b,Transform l,Transform o){
		surroundingDict.Add(p,new Transform[]{t,r,b,l});
	}

	void OnEnterPlane(Transform nextPlane){
		
	}
	// Update is called once per frame
	void Update () {
		
	}
}
