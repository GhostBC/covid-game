using System.Collections;

using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

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

  private float oldYPosition;
  private float newYPosition;

  void Start() {

    Cursor.lockState = CursorLockMode.Locked;
    Cursor.visible = false;
    verticalRotation = transform.localEulerAngles.x;
    horizontalRotation = transform.eulerAngles.y;
    oldYPosition = transform.position.y;
    newYPosition = 0;

    characterController = GetComponent < CharacterController > ();
    anim = GetComponent < Animator > ();
    audioSource = GetComponent < AudioSource > ();


  }

  // Update is called once per frame
  void Update() {

    move();
    falldamage();

  }


  private void falldamage() {


    if(characterController.isGrounded) {
      
       
       if(newYPosition - oldYPosition > 5) {
         //Falldamage
         Debug.Log("FallDamage!! Difference: " + (newYPosition - oldYPosition) );

        StartCoroutine("CameraShake");
        GameObject.Find("Lebensanzeige").GetComponent<Image>().fillAmount -= 0.25f ;


         newYPosition = 0;
       } else {
        newYPosition = 0;
       }
    } else {
       oldYPosition = transform.position.y;
      if(newYPosition != oldYPosition && oldYPosition > newYPosition) {
        //get highest value
       newYPosition = transform.position.y;    
      }
    }

   // Debug.Log("old: " + oldYPosition + " new: " + newYPosition);

  }


  private IEnumerator CameraShake()
        {
          GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>()
         .GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>()
         .m_AmplitudeGain = 3;
               
                while(GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>()
         .GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>()
         .m_AmplitudeGain > 0) {
                yield return new WaitForSeconds(0.5f);
                if(GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>()
         .GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>()
         .m_AmplitudeGain - 1 > 0) {
                  GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>()
         .GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>()
         .m_AmplitudeGain -= 1;
                } else {
                  GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>()
         .GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>()
         .m_AmplitudeGain = 0;
                }
                }
                  
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
      return;
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