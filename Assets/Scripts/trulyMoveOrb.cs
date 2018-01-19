﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //do i need this if i have line 3

namespace NetworkingDog {
	
public class trulyMoveOrb : Singleton<trulyMoveOrb> {
	public KeyCode moveL;
	public KeyCode moveR;
	public float horizVel = 0;
	public float laneNum = 2.0f;
	public bool controlBlocked = false;
	private float waitToLoad;
	private string status;
	public float zScenePos;
	public Transform path;
	public static float playerPosition;
	private Rigidbody rb;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
			int num = Random.Range (1, 3);
			print("the number is " + num.ToString());
			if (num % 2 == 0) {
				horizVel = 1f;
			} else {
				horizVel = -1f;
			}

	}
	
	// Update is called once per frame
	void Update () {
		if ((transform.position.x > 5) || (transform.position.x < -5)) {
			print("yer outta bounds");
			StartCoroutine(outOfBounds());
		}
			
		if (Input.GetKeyDown (moveL)) {
			if (horizVel < 5.5) {
				horizVel++;
				if (horizVel == 0) {
				horizVel++;
				}
			}
			print ("left");
		} else if (Input.GetKeyDown (moveR)) {
			if (horizVel > -5.5) {
				horizVel--;
				if (horizVel == 0) {
					horizVel--;
				}
			}	
			print ("right");
		} else {
//			horizVel = 0;
			print ("else");
		}
		rb.velocity = new Vector3 (horizVel, 0, 0);


//		GetComponent<Rigidbody>().velocity = new Vector3 (horizVel, 0, 0);
//		if ((Input.GetKeyDown (moveL)) && (laneNum > 1) && (!controlBlocked)) {
//			horizVel = -1;
////			StartCoroutine (stopSlide ());
//			laneNum -= 0.5f;
//			controlBlocked = true;
//			print ("left!");
//		} else if ((Input.GetKeyDown (moveR)) && (laneNum < 4) && (!controlBlocked)) {
//			horizVel = 1;
////			StartCoroutine (stopSlide ());
//			laneNum += 0.5f;
//			controlBlocked = true;
//			print ("right!");
//		} else {
////			horizVel = 0;
//			print ("else!!");
//		}
//		if (status == "exit") {
//			waitToLoad += Time.deltaTime;
//		}
		if (status == "exit") {
			SceneManager.LoadScene ("LevelComplete");
		} 
//		print (controlBlocked);
//		playerPosition = transform.position.z;
			
	}

	void OnCollisionEnter(Collision other)
	{	
			if (other.gameObject.tag == "lethal") {
				print ("this is lethal");
				print (other.gameObject);
				Destroy (gameObject);
			} else if (other.gameObject.tag == "klout") {
				GM.kloutCount += 1;
			} else if (other.gameObject.tag == "exit") {
				print ("this is the exit huh");
				status = "exit";
			} else if (other.gameObject.tag == "sidewalk") {
				Physics.IgnoreCollision(other.collider, GetComponent<Collider>());
			}
	}

	void OnCollisionStay(Collision other) {
		if (other.gameObject.tag == "klout") {
			GM.kloutCount += 1;
//			print (GM.kloutCount);
		} 
	}
			

	IEnumerator stopSlide ()
	{
		yield return new WaitForSeconds(0.3f);
		horizVel = 0;
		controlBlocked = false;
//		return;
	}

	IEnumerator outOfBounds() 
	{
		yield return new WaitForSeconds (3.5f);
			if (transform.position.x > 5 || transform.position.x < -5) {
				print ("hey u lost");
				status = "exit";
			} else {
				print ("ok ur cool");
				status = "cool";
			}
	}
}
}
