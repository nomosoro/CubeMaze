using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour {
	public Vector3 lookAt;
	public Vector3 follow;
	public int layerMask = -1;
	public RaycastHit screenCenterHit;
	public float aimingDistance = Mathf.Infinity;
	private Transform camTransform;

	public float Distance {get; set;}
	public float MIN_DISTANCE{ get; set;}
	public float MAX_DISTANCE{ get; set;}
	public Vector3 offsetFollow = Vector3.zero;
	public Vector3 offsetLookAt = Vector3.zero;
	public Ray aimRay;
	private float Y_MIN_ROTATION = -20.0f;
	private float X_MIN_ROTATION = -Mathf.Infinity;
	private float Y_MAX_ROTATION = 90.0f;
	private float X_MAX_ROTATION = Mathf.Infinity;
	private bool reverseX = false;
	private bool reverseY = false;
	private float currentX =0.0f ;
	private float currentY =0.0f;
	private float zoomSensivity = 3.0f;
	private float sensivityX = 3.0f;
	private float sensivityY = 1.0f;

	private Camera cam;
	// Use this for initialization
	void Awake(){
		Distance = 10.0f;
		MIN_DISTANCE = 1.0f;
		MAX_DISTANCE = 10.0f;
	}

	void Start () {
		cam = Camera.main;
		camTransform = cam.transform;
	}
	
	// Update is called once per frame
	void Update(){
		HandleMouseMovement ();
		HandleAiming ();
	}
	void HandleAiming(){
		aimRay= cam.ViewportPointToRay (new Vector3 (0.5f, 0.5f, 0));
	}
	void HandleMouseMovement(){
		if (reverseX) {
			currentX += Input.GetAxis ("Mouse X");
		} else {
			currentX -= Input.GetAxis ("Mouse X");
		}
		if (reverseY) {
			currentY += Input.GetAxis ("Mouse Y");
		} else {
			currentY -= Input.GetAxis ("Mouse Y");
		}


		currentX = Mathf.Clamp (currentX,X_MIN_ROTATION,X_MAX_ROTATION);
		currentY = Mathf.Clamp (currentY,Y_MIN_ROTATION,Y_MAX_ROTATION);
	}
	void LateUpdate () {
		Vector3 direction = new Vector3 (0,0,-Distance);
		Quaternion rotation = Quaternion.Euler(sensivityY*currentY,-sensivityX*currentX,0);
		Vector3 newCamPos = follow + rotation * direction;
		newCamPos+=offsetFollow;
		camTransform.position = newCamPos;
		Vector3 lookAtPos = lookAt;
		lookAtPos+=offsetLookAt;
		camTransform.LookAt (lookAtPos);
	}

	public void Zoom(float value){
		MAX_DISTANCE = Mathf.Clamp( MAX_DISTANCE - zoomSensivity*value,MIN_DISTANCE,10.0f);
	}

	#region Debug Methods
	void OnDrawGizmos(){
		Gizmos.color = Color.blue;
		Gizmos.DrawRay (aimRay.origin,aimRay.direction*10f);
	}
	#endregion
}
