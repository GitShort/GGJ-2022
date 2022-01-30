using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportation : MonoBehaviour
{
    [SerializeField] private Vector3 forcePositionVector = new Vector3(0.01f, 0f, 0f);
    [SerializeField] private Collider _collider;
    [SerializeField] private bool oneWayTeleport = false;
    [SerializeField] private bool OneColorTeleport = false;
    [SerializeField] private float offset = 0.1f;
    private int previosLayer;
    private void Start()
    {
  
    }

    private void OnTriggerEnter(Collider other)
    {

        if (_collider != null && oneWayTeleport)
        {
            previosLayer = other.gameObject.layer;
            other.gameObject.layer = 8;
        }
        else if(!OneColorTeleport)
        {
            previosLayer = other.gameObject.layer;
            other.gameObject.layer = 16;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out GamePlayerMovementBase baseScript) && other.gameObject.TryGetComponent(out Gravity baseScriptGravity))
        {
            // baseScript.jump = baseScript.jump * -1;
            // baseScriptGravity.gravityForce = baseScriptGravity.gravityForce * -1;
            // baseScript.JumpOnImpulse(forcePositionVector);
            // baseScript.ApplyRotation();
            baseScript.StartTeleportation(forcePositionVector);
            if (_collider != null && oneWayTeleport)
            {
                other.gameObject.layer = previosLayer;
            }
            else if(!OneColorTeleport)
            {
                other.gameObject.layer = previosLayer;
            }
        }
        
    }

    
}
