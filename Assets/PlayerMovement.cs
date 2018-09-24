using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public float speed = 6f; //the speed of movement for the player

	Vector3 movement; //store the direction of the player's movement
	Animator anim;  //reference to the animator component
	Rigidbody playerRigidBody; //reference to the player rigidbody
	int floorMask;  //a layer so that a ray cam be casted at gameobjects sitting on this floor layer
	float camRayLength = 100f; //the length of the ray from the camera casted into our scene

	// Use this for initialization
	void Awake () {
		//get the layer mask
		floorMask = LayerMask.GetMask("Floor");

		//set up references
		anim = GetComponent<Animator>();
		playerRigidBody = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//store the input axes
		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");

		//Move the player around by applying force
		Move(h, v);

		//Turn the player to face our cursor;
//		Turning();

		//Animate the player.
		Animating(h, v);
	}

	void Move(float h, float v){
		//set the movement vector based on the axis input
		movement.Set (h, 0f, v);

		//normalise the movement vector and make it proportional to the speedper second.
		movement = movement.normalized*speed*Time.deltaTime;

		//update the player to it's current position plus the movement
		playerRigidBody.MovePosition(transform.position + movement);
	}

	void Animating(float h, float v){
		//create a boolean variable that can only be true or false.
		bool walking = h != 0f || v != 0f;

		//tell the animator whether or not our player is walking
		anim.SetBool("IsWalking", walking);
	}

}
