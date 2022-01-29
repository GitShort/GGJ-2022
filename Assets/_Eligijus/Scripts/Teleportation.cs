using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportation : MonoBehaviour
{
    [SerializeField] private Vector3 forcePositionVector = new Vector3(0.01f, 0f, 0f);
    private void OnTriggerEnter(Collider other)
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out GamePlayerMovementBase baseScript) && other.gameObject.TryGetComponent(out Gravity baseScriptGravity))
        {
            baseScript.jump = baseScript.jump * -1;
            baseScriptGravity.gravityForce = baseScriptGravity.gravityForce * -1;
            baseScript.JumpOnImpulse(forcePositionVector);
            baseScript.ApplyRotation();
        }
    }
    
}
