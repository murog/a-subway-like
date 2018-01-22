﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class startScreen : MonoBehaviour {
	public float horizontalSpeed = 2.0F;
	public float verticalSpeed = 2.0F;
	public float resetSpeed = 0.5F;
	public float cameraSpeed;
	private bool gameStart;
	private Rigidbody rb;
	private float h;
	private float v;
	Quaternion originalRotation;
	private bool restoreRotation = false;
	private float waitToLoad;
	public GUIText countDown;
	private bool doPanning = false;
	private Quaternion startRotation;
	private Quaternion endRotation;
	private Quaternion downRotation;
	private bool doZoom = false;
	[SerializeField]
	private Transform pug;
	public float startFOV;
	public float zoomInFOV;
	public float smooth;
	private bool freezeMovement = false;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		originalRotation = transform.rotation;
		countDown = GetComponent<GUIText> ();
		pug.gameObject.SetActive (false);
//		endRotation = new Quaternion (0, 0, 1, 0);
	}
	
	// Update is called once per frame
	void Update() {
		// allow for horizontal mouse movement
		if (!restoreRotation && !freezeMovement) {
			h = horizontalSpeed * Input.GetAxis ("Mouse X");
			v = verticalSpeed * Input.GetAxis ("Mouse Y");
			transform.Rotate (v, h, 0);
		}
		if (Input.GetMouseButtonDown (0)) {
			gameStart = true;
			restoreRotation = true;
//			transform.Rotate (0, -h, 0);
		}
		if (restoreRotation && !freezeMovement) {
			transform.rotation = Quaternion.Lerp(transform.rotation,originalRotation,Time.time * resetSpeed);
		}
		if (transform.rotation == originalRotation) {
			restoreRotation = false;
		}
		if ((gameStart) && (transform.position.z < 25)) {
			rb.velocity = (new Vector3 (0, 0, 1)) * cameraSpeed;
		}
		if (transform.position.z >= 25) {
			freezeMovement = true;
			rb.velocity = new Vector3 (0, 0, 0);
			startRotation = transform.rotation;
			endRotation =	Quaternion.Euler (3, 180, 0);
//			pug.transform.position = new Vector3(0, 0, 20);
//			downRotation = Quaternion.Euler (40, 0, 0);

			doPanning = true;
			pug.gameObject.SetActive (true);
			pug.gameObject.transform.position = new Vector3 (0, 0, 20);
			waitToLoad += Time.deltaTime;
		}
//		if (waitToLoad > 0) {
//			countDown.text = "get ready";
//		}

//		if (waitToLoad > 5) {
//			SceneManager.LoadScene ("Sidewalk");
//		}

		if (doPanning) {
			transform.rotation = Quaternion.Slerp (startRotation, endRotation, Time.deltaTime * 2);
			print (transform.rotation);
			if (transform.rotation == new Quaternion(0, -1.0f, 0.3f, 0)) {
				doPanning = false;
				print ("do panning is" + doPanning.ToString ());
//				startRotation = transform.rotation;
//				endRotation = 
					doZoom = true;
					startFOV = Camera.main.fieldOfView;
					zoomInFOV = 20;
				}
		}
		if (doZoom) {
//			transform.rotation = Quaternion.Lerp (startRotation, endRotation, Time.deltaTime);
			Camera.main.fieldOfView = Mathf.Lerp(startFOV, zoomInFOV, Time.deltaTime);
			if (Camera.main.fieldOfView == zoomInFOV) {
				doZoom = false;
				SceneManager.LoadScene ("Sidewalk");

			}
		}
	}
		
}
