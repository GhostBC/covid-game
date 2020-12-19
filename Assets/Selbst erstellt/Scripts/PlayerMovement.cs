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
  private AudioSource audioSource;

  private float verticalRotation;
  private float horizontalRotation;

  void Start() {

    Cursor.lockState = CursorLockMode.Locked;
    Cursor.visible = false;
    verticalRotation = transform.localEulerAngles.x;
    horizontalRotation = transform.eulerAngles.y;

    characterController = GetComponent < CharacterController > ();
    anim = GetComponent < Animator > ();
    audioSource = GetComponent < AudioSource > ();

  }

  // Update is called once per frame
  void Update() {

    move();

  }

  private void move() {

    float _mouseVertical = -Input.GetAxis("Mouse Y");
    float _mouseHorizontal = Input.GetAxis("Mouse X");

    verticalRotation += _mouseVertical * (mouseSensitivity * 200) * Time.deltaTime;
    horizontalRotation += _mouseHorizontal * (mouseSensitivity * 200) * Time.deltaTime;

    verticalRotation = Mathf.Clamp(verticalRotation, upLimit, downLimit);

    cameraHolder.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
    transform.rotation = Quaternion.Euler(0f, horizontalRotation, 0f);

    float verticalMove = Input.GetAxis("Vertical");

    Vector3 gravityMove = new Vector3(0, verticalSpeed, 0);
    Vector3 move = transform.forward * verticalMove;
    if (verticalMove < 0) {
      this.speed = 4;
    } else {
      this.speed = 5;

    }
	anim.SetFloat("speed", this.speed);
    characterController.Move(speed * Time.deltaTime * move + gravityMove * Time.deltaTime);


    if (verticalMove > 0) {

      anim.SetBool("isWalking", true);
      if (!audioSource.isPlaying && characterController.isGrounded) {
        audioSource.PlayOneShot(walkSound, 0.3F);
      }
      anim.SetBool("isWalkingBackwards", false);
 
    } else {

      if (verticalMove < 0 && characterController.isGrounded) {
        anim.SetBool("isWalkingBackwards", true);
      } else {
        anim.SetBool("isWalkingBackwards", false);
      }
      anim.SetBool("isWalking", false);

    }

    if (Input.GetKey(KeyCode.Space) == true && anim.GetBool("isJumping") == false) {
      verticalSpeed = 11;
      anim.SetBool("isJumping", true);
      audioSource.Stop();
      audioSource.PlayOneShot(jumpSound, 0.1F);

    }



    if (characterController.isGrounded) {
      if (anim.GetBool("isJumping")) {
        anim.SetBool("isJumping", false);
        anim.SetTrigger("exitJump");

      }
     // verticalSpeed = gravity * Time.deltaTime;

    } else {
     
      verticalSpeed -= gravity * Time.deltaTime;
    }




  }

}