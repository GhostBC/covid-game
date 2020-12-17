using System.Collections;

using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class PlayerMovement: MonoBehaviour {

	Animator anim;
	protected CharacterController characterController;
	public float speed = 4;
	public float gravity = 9.81f;
	private float verticalSpeed = 0;

	public Transform cameraHolder;
	public float mouseSensitivity = 2f;
	public float upLimit = -50;
	public float downLimit = 50;

	void Start() {
		characterController = GetComponent < CharacterController > ();
		anim = GetComponent < Animator > ();
		//  anim.SetBool("isWalking", false);
	}

	// Update is called once per frame
	void Update() {

		move();
		rotate();

	}

	private void move() {

		float horizontalMove = Input.GetAxis("Horizontal");
		float verticalMove = Input.GetAxis("Vertical");

		if (characterController.isGrounded) {
			if (anim.GetBool("isJumping")) {
				anim.SetBool("isJumping", false);
				anim.SetTrigger("exitJump");
			}
			verticalSpeed = 0;

		} else {
			verticalSpeed -= gravity * Time.deltaTime;
		}
		Vector3 gravityMove = new Vector3(0, verticalSpeed, 0);
		Vector3 move = transform.forward * verticalMove + transform.right * horizontalMove;
		if (verticalMove < 0) {
			this.speed = 1;

		} else {

			this.speed = 4;
		}
		anim.SetFloat("speed", this.speed);
		characterController.Move(speed * Time.deltaTime * move + gravityMove * Time.deltaTime);

		if (Input.GetKey(KeyCode.Space) == true && anim.GetBool("isJumping") == false) {
			verticalSpeed = 5;

			anim.SetBool("isJumping", true);

		}

		anim.SetBool("isWalking", verticalMove != 0 || horizontalMove != 0);

	}

	private void rotate() {

		float horizontalRotation = Input.GetAxis("Mouse X");
		float verticalRotation = Input.GetAxis("Mouse Y");

		transform.Rotate(0, horizontalRotation * mouseSensitivity, 0);
		cameraHolder.Rotate( - verticalRotation * mouseSensitivity, 0, 0);

		Vector3 currentRotation = cameraHolder.localEulerAngles;
		if (currentRotation.x > 180) currentRotation.x -= 360;
		currentRotation.x = Mathf.Clamp(currentRotation.x, upLimit, downLimit);
		cameraHolder.localRotation = Quaternion.Euler(currentRotation);

	}

}