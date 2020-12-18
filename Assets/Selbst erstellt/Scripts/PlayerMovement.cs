using System.Collections;

using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class PlayerMovement: MonoBehaviour {

	Animator anim;
	protected CharacterController characterController;
	private float speed = 5;
	public float gravity = 9.81f;
	private float verticalSpeed = 0;

	public Transform cameraHolder;
	private float mouseSensitivity = 2f;
	private float upLimit = -50;
	private float downLimit = 50;
	public AudioClip jumpSound;
	public AudioClip walkSound;
	private AudioSource  audioSource;


	void Start() {
		characterController = GetComponent < CharacterController > ();
		anim = GetComponent < Animator > ();
		 audioSource = GetComponent<AudioSource>();
	
	}

	// Update is called once per frame
	void Update() {

		move();
		rotate();

	}

	private void move() {

	
		float verticalMove = Input.GetAxis("Vertical");

	
		Vector3 gravityMove = new Vector3(0, verticalSpeed, 0);
		Vector3 move = transform.forward * verticalMove ;
		if (verticalMove < 0) {
			this.speed = 3;

		} else {
		
				this.speed = 5;
		
			
		}
		anim.SetFloat("speed", this.speed);
		characterController.Move(speed * Time.deltaTime * move + gravityMove * Time.deltaTime);

			if (characterController.isGrounded) {
			if (anim.GetBool("isJumping")) {
				anim.SetBool("isJumping", false);
				anim.SetTrigger("exitJump");
			}
			 verticalSpeed =  -characterController.stepOffset / Time.deltaTime;

		} else {
			verticalSpeed -= gravity  *  Time.deltaTime;
		}


		if (Input.GetKey(KeyCode.Space) == true && anim.GetBool("isJumping") == false) {
			verticalSpeed = 10;

			anim.SetBool("isJumping", true);
			audioSource.Stop();
			 audioSource.PlayOneShot(jumpSound, 0.1F);

		}


		if(verticalMove > 0 ) {
			  
	 	 anim.SetBool("isWalking",true);
		   if (!audioSource.isPlaying && characterController.isGrounded)
        {
		   audioSource.PlayOneShot(walkSound, 0.3F);
		}
		anim.SetBool("isWalkingBackwards",false);
		  if(!Input.GetMouseButton(0)) {
			  //Reset Camera behind the Player
			  	Quaternion resetRotation = Quaternion.Euler(transform.rotation.eulerAngles);
	 			 cameraHolder.rotation = Quaternion.Lerp(cameraHolder.rotation, resetRotation, Time.deltaTime * 3);
		  }
		} else {

			if(verticalMove < 0 && characterController.isGrounded) {
				anim.SetBool("isWalkingBackwards",true);
			} else {
				anim.SetBool("isWalkingBackwards",false);
			}
			anim.SetBool("isWalking",false);
			
		}

		
		

	}

	private void rotate() {

  if (Input.GetMouseButton(0)) {
		float horizontalRotation = Input.GetAxis("Mouse X");
		float verticalRotation = Input.GetAxis("Mouse Y");

		Vector3 currentRotation = cameraHolder.localEulerAngles;
		if (currentRotation.x > 180) currentRotation.x -= 360;
		currentRotation.x = Mathf.Clamp(currentRotation.x, upLimit, downLimit);
		currentRotation.z = 0;
		cameraHolder.localRotation = Quaternion.Euler(currentRotation);
			cameraHolder.Rotate( - verticalRotation * mouseSensitivity, horizontalRotation * mouseSensitivity, 0);
  } else {

   transform.Rotate(0.0f, Input.GetAxis ("Horizontal") * 0.65f, 0.0f);


  }

	}

}