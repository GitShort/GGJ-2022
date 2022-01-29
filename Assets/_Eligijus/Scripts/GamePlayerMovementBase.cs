using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayerMovementBase : MonoBehaviour
{
    
    public float movementIntensity;
    public float maximumSpeed = 10f;
    public float jumpingOppositeForce = 0.6f;
    public Vector3 jump;
    public float jumpForce = 2.0f;
    protected Rigidbody rb;
    
    private Quaternion angleToRotate;
    private bool startRotate = false;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        angleToRotate = transform.rotation;
    }
    
    
    public void JumpOnImpulse(Vector3 jumpVector)
    {
        rb.AddForce(jumpVector * jumpForce, ForceMode.Impulse);
    }

    public void ApplyRotation()
    {
        startRotate = true;
        if (angleToRotate.z == 0)
        {
            angleToRotate.z = 1;
        }
        else
        {
            angleToRotate.z = 0;
        }
    }

    protected virtual void FixedUpdate()
    {
        if (startRotate)
        {

            if (Vector3.Distance(transform.eulerAngles, angleToRotate.eulerAngles) > 0.01f)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, angleToRotate, 3.0f * Time.deltaTime);

            }
            else
            {
                transform.rotation = angleToRotate;
                startRotate = false;
            }
        }
    }
}
