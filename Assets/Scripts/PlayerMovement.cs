using System.Collections;


using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    Animator anim;
    protected CharacterController characterController;

    protected Vector3 movement;

    public float _rotationSpeed = 180;

    private Vector3 rotation;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (characterController.isGrounded == true)
        {

            movement = new Vector3(
               0, 0, Input.GetAxis("Vertical"));
            movement = transform.TransformDirection(movement);
            movement *= 5.0f;

            if (movement.x != 0 || movement.y != 0)
            {
                anim.SetBool("isWalking", true);
            }
            else
            {
                anim.SetBool("isWalking", false);
            }



            if (Input.GetKey(KeyCode.Space) == true)
            {
                movement.y = 10.0f;
                anim.SetBool("isWalking", false);
            }
        }

        movement.y -= 20.0f * Time.deltaTime;

        characterController.Move(movement * Time.deltaTime);



    }

    void LateUpdate()
    {
        this.rotation = new Vector3(0, Input.GetAxis("Horizontal") * _rotationSpeed * Time.fixedDeltaTime, 0);

        this.transform.Rotate(this.rotation);
    }
}