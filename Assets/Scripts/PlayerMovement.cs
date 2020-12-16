using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

  public float speed;

    Rigidbody rb;
    Vector3 moveDirection;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
       rb = GetComponent<Rigidbody>(); 
       anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float mH = Input.GetAxis ("Horizontal");
        float mV = Input.GetAxis ("Vertical");
        rb.velocity = new Vector3 (mH * speed, rb.velocity.y, mV * speed) *-1;

        if(Input.GetAxis ("Horizontal") != 0 || Input.GetAxis ("Vertical") != 0  ) {

           anim.SetBool("isWalking", true);

        } else {
               anim.SetBool("isWalking", false);
        }


    }
}
