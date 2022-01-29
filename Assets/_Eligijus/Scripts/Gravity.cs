using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    [SerializeField] private Vector3 gravityForce;
    [SerializeField] private Vector3 inertiaTensor = Vector3.one;
    [SerializeField] private bool syncTwoRbs;
    [SerializeField] private Rigidbody rbToSync;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.inertiaTensor = inertiaTensor;
        rb.AddForce(gravityForce * 9.81f, ForceMode.Acceleration);
        
        if (syncTwoRbs)
        {
            rb.velocity = new Vector3(rbToSync.velocity.x, -rbToSync.velocity.y, rbToSync.velocity.z);
            rb.angularVelocity = -rbToSync.angularVelocity;
        }

    }
}
