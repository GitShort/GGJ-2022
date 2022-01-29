using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportation : MonoBehaviour
{
    [SerializeField] private Vector3 forcePositionVector = new Vector3(0.01f, 0f, 0f);
    [SerializeField] private Collider _collider;
    [SerializeField] private bool oneWayteleport = false;
    private int previosLayer;



    private void OnTriggerEnter(Collider other)
    {
        if (_collider != null && oneWayteleport)
        {
            previosLayer = other.gameObject.layer;
            other.gameObject.layer = 8;
        }
        else
        {
            previosLayer = other.gameObject.layer;
            other.gameObject.layer = 16;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out GamePlayerMovementBase baseScript) && other.gameObject.TryGetComponent(out Gravity baseScriptGravity))
        {
            baseScript.jump = baseScript.jump * -1;
            baseScriptGravity.gravityForce = baseScriptGravity.gravityForce * -1;
            baseScript.JumpOnImpulse(forcePositionVector);
            baseScript.ApplyRotation();
            if (_collider != null && oneWayteleport)
            {
                other.gameObject.layer = previosLayer;
            }
            else
            {
                other.gameObject.layer = previosLayer;
            }
        }
    }
    
}
