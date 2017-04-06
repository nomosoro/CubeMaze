using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	#region Field

	public LayerMask layerMask = -1;
	public Transform followCamera;
	public UIManager uiManager;
	public GameObject mazeManager;
	private Vector3 centerOffset = new Vector3(0,0.6f,0);
	private float speed=5;
	private Rigidbody rb;
	private Animator anim;
	private PlayerCameraController cameraScript;
	private PlaneManager planeManagerScript;
	private Transform lastAimedTransform = null;
	#endregion

	#region Initialization
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody> ();
		cameraScript = followCamera.GetComponent<PlayerCameraController>();
		cameraScript.lookAt = GetCenter();
		cameraScript.follow = GetCenter ();
		cameraScript.offsetFollow += centerOffset;
		cameraScript.offsetLookAt += centerOffset;
		planeManagerScript = mazeManager.GetComponent<PlaneManager> ();
		uiManager = GameObject.FindGameObjectWithTag ("UIManager").GetComponent<UIManager> ();
	}
	#endregion

	#region Update Methods
	// Update is called once per frame
	void Update () {
		HandleMove ();
		HandleCamera ();
		HandleRotation ();
		HandleEvents ();
		HandleAiming ();
		HandleInteracting ();
	}

	private void HandleMove(){
		float h_move=Input.GetAxisRaw("Horizontal");
		float v_move=Input.GetAxisRaw("Vertical");
		bool isRun = Input.GetKey (KeyCode.LeftShift);
		float curSpeed = isRun ? 1.5f * speed : speed;
		rb.AddRelativeForce (v_move*Vector3.forward*Time.deltaTime*100*curSpeed);
		rb.AddRelativeForce (h_move*Vector3.right*Time.deltaTime*100*curSpeed);
		anim.SetFloat ("Speed",v_move);
	}
	private void HandleCamera(){
		float mouse_scroll = Input.GetAxis ("Mouse ScrollWheel");
		cameraScript.lookAt = GetCenter ();
		cameraScript.follow = GetCenter ();
		cameraScript.Zoom (mouse_scroll);
		RaycastHit hit;
		if (Physics.Raycast (cameraScript.lookAt,-cameraScript.aimRay.direction, out hit,1000f)) {
			cameraScript.Distance = Mathf.Clamp (hit.distance-0.5f, cameraScript.MIN_DISTANCE, cameraScript.MAX_DISTANCE);
		} else {
			cameraScript.Distance = cameraScript.MAX_DISTANCE;
		}
	}
	private void HandleRotation(){
		Vector3 currentLookAt = cameraScript.aimRay.direction;
		currentLookAt.y = 0.0f;
		Quaternion newRotation = Quaternion.LookRotation (currentLookAt);
		transform.rotation = newRotation;
	} 
	private void HandleEvents(){
		planeManagerScript.UpdateCurrentVisitingPoint (transform.position);
	}
	private void HandleAiming(){
		RaycastHit hit;
		if (Physics.Raycast (cameraScript.aimRay, out hit, 1000f, layerMask.value)) {
			Debug.Log ("hitting!");
			lastAimedTransform = hit.transform;
			MonoBehaviour[] scriptList = lastAimedTransform.GetComponents<MonoBehaviour> ();
			foreach (MonoBehaviour script in scriptList) {
				if (script is IAimable) {
					(script as IAimable).OnAim ();
				}
			}
		} else {
			if (lastAimedTransform != null) {
				MonoBehaviour[] scriptList = lastAimedTransform.GetComponents<MonoBehaviour> ();
				foreach (MonoBehaviour script in scriptList) {
					if (script is IAimable) {
						(script as IAimable).OnAimOut ();
					}
				}
				lastAimedTransform = null;
			}
		}
	}
	private void HandleInteracting(){
		bool isInteract = Input.GetKeyDown (KeyCode.E);
		if (isInteract) {
			RaycastHit hit;
			if (Physics.Raycast (cameraScript.aimRay,out hit,1000f,layerMask.value)) {
				MonoBehaviour[] scriptList=hit.collider.transform.gameObject.GetComponents<MonoBehaviour>() ;
				foreach (MonoBehaviour script in scriptList) {
					if (script is IInteractable) {
						(script as IInteractable).OnInteract ();	
					}
				}
			}
		}
	}
	#endregion

	#region Utility Methods
	Vector3 GetCenter(){
		return transform.position + centerOffset;
	}

	#endregion

	#region Debug Methods

	#endregion
}
