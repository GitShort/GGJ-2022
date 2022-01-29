using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ReverseGamePlayerMovement : GamePlayerMovementBase
{
    
    private float horizontalValue;
    private bool isGrounded;
    private bool button = false;
    private bool higher = false;
    private bool buttonRealised = false;
    private float timer = 0f;

    protected override void Start(){
        base.Start();
    }

    void OnCollisionStay(){
        isGrounded = true;
    }


    private void OnCollisionExit(Collision other)
    {
        isGrounded = false;
    }


    // Update is called once per frame
    private void Update()
    {
        
    }
    
    public void OnMove(Vector2 value)
    {
        

        horizontalValue = value.x;

        // movementX = movementVector.x;
        // movementY = movementVector.y;
    }
    
    public void OnUp(float value)
    {

        if (value > 0.9f)
        {
            button = true;
            
        }
        else
        {
            button = false;
            buttonRealised = true;
        }


    }

    public void OnDown(float value)
    {

        Debug.Log(value);
        if (value > 0.9f)
        {
            
        }
        else
        {
            
        }
        // movementX = movementVector.x;
        // movementY = movementVector.y;
    }

    

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (isGrounded && button)
        {
            rb.AddForce(jump * jumpForce * Time.fixedDeltaTime, ForceMode.Impulse);
            higher = true;
            buttonRealised = false;
            timer = 0f;
        }

        if (button && !isGrounded && !buttonRealised)
        {
            timer += Time.fixedDeltaTime;
        }

        if (button && higher && timer > 0.1)
        {
            rb.AddForce(jump * jumpForce * 0.35f * Time.fixedDeltaTime, ForceMode.Impulse);
            higher = false;
        }

        rb.AddForce(Vector3.right * movementIntensity * horizontalValue);
        if (!isGrounded)
        {
            rb.AddForce(Vector3.left * movementIntensity * jumpingOppositeForce * horizontalValue);
        }

        if (isGrounded)
        {
            if (rb.velocity.magnitude > maximumSpeed)
            {
                rb.velocity = rb.velocity.normalized * maximumSpeed;
            }
        }
    }
}
