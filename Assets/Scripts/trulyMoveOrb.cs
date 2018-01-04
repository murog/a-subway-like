﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trulyMoveOrb : MonoBehaviour {
	public KeyCode moveL;
	public KeyCode moveR;

	public float horizVel = 0;

	public int laneNum = 2;
	public string controlBlocked = "n";
	// Use this for initialization
	void Start () {
		print ("i'm starting haha");

	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<Rigidbody>().velocity = new Vector3 (horizVel, 0, 4);
		if ((Input.GetKeyDown(moveL)) && (laneNum > 1)) {
			horizVel = -2;
			StartCoroutine (stopSlide ());
			laneNum -= 1;
			controlBlocked = "y";
		}

		if ((Input.GetKeyDown (moveR)) && (laneNum < 4) && (controlBlocked == "n")) {
			horizVel = 2;
			StartCoroutine (stopSlide ());
			laneNum += 1;
			controlBlocked = "y";
		}
			
	}

	void OnCollisionEnter(Collision other)
	{	
		if (other.gameObject.tag == "lethal") {
			print("this is lethal");
			Destroy (gameObject);
		} else {
			print("colliding rn");
		}
	}

	IEnumerator stopSlide ()
	{
		yield return new WaitForSeconds(0.5f);
		horizVel = 0;
		controlBlocked = "n";
	}
}