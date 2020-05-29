using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    
    
   
    public CharacterController2D controller;
    public Animator animator;
    float horizontalMove = 0f;
    public float runSpeed = 40f;
    private bool dashCD = false;
    private double timeStamp;

    

    // Update is called once per frame
    void Update()
    {

        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));                                      //Animator is set, so it can be evaluated for the animation, if the player is moving.

        if (Input.GetButtonDown("Jump"))
        {

            controller.Jump();
        }


        if ((Input.GetKeyDown(KeyCode.Q)  || Input.GetKeyDown(KeyCode.E)) && !dashCD) {             //Check for the dash buttons input

            dashCD = controller.Dash(dashCD);
            timeStamp = Time.time + 0.35;
            animator.SetBool("Dash", true);                                                            //Animator is set, so if dash is triggered, the dash animation is played.
            
        }
        else{
            animator.SetBool("Dash", false);
        }

        if (timeStamp <= Time.time)
        {
            dashCD = false;
        }

    }

    void FixedUpdate()
    {

        controller.Move(horizontalMove * Time.fixedDeltaTime);
        
    }
}
