using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ReverseGamePlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementIntensity;
    private Rigidbody rb;
    private float horizontalValue;
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
            
        }
        else
        {
            
        }

        Debug.Log("Je");
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
        rb.AddForce(Vector3.right * movementIntensity * horizontalValue);
    }
}
