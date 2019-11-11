﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    public float walkSpeed = 0.5f;
        
    public float runSpeed = 1;
    public float gravity = -12;
    public float jumpHeight = 1;
    [Range(0, 1)]
    public float airControlPercent;

    public float turnSmoothTime = 0.2f;
    float turnSmoothVelocity;

    public float speedSmoothTime = 0.1f;
    float speedSmoothVelocity;
    float currentSpeed;
    float velocityY;
    public bool canMove;
    public Animator animator;
    Transform cameraT;
    public CharacterController controller;


    // Raycast Information
    [Header("RayCast info")]
    public float PushMinDistance = 1f;
    public float LedgeMinDistance = 1f;
    public GameObject RayForwardTarget;
    // AnimationController AnimController;

       
  
    // Knock back
    public float knockBackForce = 2f;
    public float knockBackTime = 0.5f;
    private float knockBackCounter;
    private Vector3 moveDirection;

    // audio 
    public AudioSource jumpSound;

  
    void Start()
    {
        canMove = true;
        //   AnimController.GetComponent<AnimationController>();
        //animator = GetComponentInChildren<Animator>();
     animator  = gameObject.GetComponentInChildren<Animator>();
        cameraT = Camera.main.transform;
        controller = GetComponent<CharacterController>();
        //controller = gameObj<CharacterController>();
        //  animator.SetBool("Aim", false);

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {

        if (canMove == false)
            return;

        


            // input
            Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            Vector2 inputDir = input.normalized;
            bool running = Input.GetKey(KeyCode.LeftShift);

            Move(inputDir, running);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
            // animator
            float animationSpeedPercent = ((running) ? currentSpeed / runSpeed : currentSpeed / walkSpeed * .5f);
            // set name of forward animation here

            animator.SetFloat("Forward", animationSpeedPercent, speedSmoothTime, Time.deltaTime);

        // ray jump and push
           // RayGroundCheck();
           // RayObsticalCheck();
     
       
    }

    void Move(Vector2 inputDir, bool running)
    {

        // knock back 
    if (knockBackCounter <= 0)
    {

        if (inputDir != Vector2.zero)
        {
            float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraT.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, GetModifiedSmoothTime(turnSmoothTime));
        }

        float targetSpeed = ((running) ? runSpeed : walkSpeed) * inputDir.magnitude;
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, GetModifiedSmoothTime(speedSmoothTime));

            velocityY += Time.deltaTime * gravity;
            moveDirection = transform.forward * currentSpeed + Vector3.up * velocityY;
        }
        else
        {
            knockBackCounter -= Time.deltaTime;
        }

        
       

        controller.Move(moveDirection * Time.deltaTime);

        if(knockBackCounter <= 0)
        currentSpeed = new Vector2(controller.velocity.x, controller.velocity.z).magnitude;

        if (controller.isGrounded)
        {
            velocityY = 0;
            // NOTE check if .. lag remove and set a wait in animation to time stop jumping
            // AnimController.StopJumpingAnimation();
            animator.SetBool("Jumping", false);
        }

    }

    void Jump()
    {
        if (controller.isGrounded)
        {
            jumpSound.Play();
            float jumpVelocity = Mathf.Sqrt(-2 * gravity * jumpHeight);
            velocityY = jumpVelocity;
            Debug.Log("HitJumpbutton");
            animator.SetBool("Jumping", true);
        }
    }

    float GetModifiedSmoothTime(float smoothTime)
    {
        if (controller.isGrounded)
        {
            return smoothTime;
        }

        if (airControlPercent == 0)
        {
            return float.MaxValue;
        }
        return smoothTime / airControlPercent;
    }

    public void RayGroundCheck()
    {
        
            Ray ray = new Ray(transform.position, Vector3.down);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 1f))
            {

                float Distance = Vector3.Distance(hit.point, transform.position);
            if (controller.isGrounded == false)
            {
                JumpingUp(Distance);
             //   Debug.Log(Distance);
            }
            }
        
    }


    public void JumpingUp(float distance)
    {
        animator.SetFloat("JumpingStateUp", distance);
    }


    public void RayObsticalCheck()
    {

        Ray ray2 = new Ray(transform.position, Vector3.forward);
        RaycastHit hit2;
        Debug.DrawLine(RayForwardTarget.transform.localPosition, RayForwardTarget.transform.forward , Color.red, 1f);
        if (Physics.Raycast(ray2, out hit2, 1f))
        {

            float Distance = Vector3.Distance(hit2.point, transform.position);
            Debug.Log(Distance);
            if (hit2.collider.gameObject.tag == "Pushable" && Distance <= PushMinDistance)
            {
                // Can Push
                Debug.Log("CanPush");
            }

            if (hit2.collider.gameObject.tag == "Ledge" && Distance <= LedgeMinDistance)
            {
                // is at the ledge
                Debug.Log("IsAtLedge");
            }
               
           
        }
    }


   public void HitLedge()
    {
        animator.SetBool("HitLedge", true);
        canMove = false;
        Invoke("DisableHitLedge", 2f);
    }
    public void DisableHitLedge()
    {
        animator.SetBool("HitLedge", false);
        canMove = true;
    }


    // knock back when hert
    public void KnockBack(Vector3 direction)
    {
        knockBackCounter = knockBackTime;

       // direction = new Vector3(1f, 1f, 1f);
        moveDirection = direction * knockBackForce;
        moveDirection.y = knockBackForce/2 * gravity;

    }
}
