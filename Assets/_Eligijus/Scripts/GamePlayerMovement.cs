using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class GamePlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementIntensity;
    [SerializeField] float maximumSpeed = 10f;
    [SerializeField] private float jumpingOppositeForce = 0.6f;
    private Rigidbody rb;
    private float horizontalValue;
    
    
    public Vector3 jump;
    public float jumpForce = 2.0f;
    private bool isGrounded;
    private bool button = false;
    private bool higher = false;
    private bool buttonRealised = false;
    private float timer = 0f;
    
    // Start is called before the first frame update
    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
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

    private void FixedUpdate()
    {
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

        if (button && higher && timer > 1)
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
