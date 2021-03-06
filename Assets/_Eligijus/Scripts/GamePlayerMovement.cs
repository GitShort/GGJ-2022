using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class GamePlayerMovement : GamePlayerMovementBase
{
    
    private float horizontalValue;
    private bool isGrounded;
    private bool oneTimeGround = false;
    private bool button = false;
    private bool higher = false;
    private bool buttonRealised = false;
    private float timer = 0f;
    
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
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
            higher = false;
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
        if (movementEnabled)
        {
            Array.Clear(overlappingColliders, 0, overlappingColliders.Length);

            int numColliding = Physics.OverlapBoxNonAlloc(hoverPosition.transform.position, cubeSize,
                overlappingColliders,
                gameObject.transform.rotation, hoverLayerMask, QueryTriggerInteraction.Ignore);

            if (numColliding > 0)
            {
                isGrounded = true;
                if (oneTimeGround)
                {
                    CameraShake.Shake(0.05f, 0.06f);
                    oneTimeGround = false;
                    AudioManager.instance.PlaySound("Impact", this.gameObject);
                }
            }
            else
            {
                isGrounded = false;
                oneTimeGround = true;
            }

            base.FixedUpdate();
            if (isGrounded && button)
            {
                rb.AddForce(jump * jumpForce * Time.fixedDeltaTime, ForceMode.Impulse);
                higher = true;
                buttonRealised = false;
                timer = 0f;
                AudioManager.instance.PlaySound("Jump", this.gameObject);
            }

            if (button && !isGrounded && !buttonRealised)
            {
                timer += Time.fixedDeltaTime;
            }

            if (button && higher && timer > 0.15)
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
}
