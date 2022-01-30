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
    private Collider[] _colliders;
    private int[] layers;
    private void Start()
    {
        _colliders = new Collider[2];
        layers = new int[2];
    }

    private void OnTriggerEnter(Collider other)
    {

        if (_collider != null && oneWayTeleport)
        {
            if (_colliders[0] == null)
            {
                _colliders[0] = other;
                layers[0] = other.gameObject.layer;
                
            }
            else if (_colliders[0] != null && _colliders[0] != other && _colliders[1] == null)
            {
                _colliders[1] = other;
                layers[1] = other.gameObject.layer;
            }
            
            other.gameObject.layer = 8;
        }
        else if(!OneColorTeleport)
        {
            if (_colliders[0] == null)
            {
                _colliders[0] = other;
                layers[0] = other.gameObject.layer;
                
            }
            else if (_colliders[0] != null && _colliders[0] != other && _colliders[1] == null)
            {
                _colliders[1] = other;
                layers[1] = other.gameObject.layer;
            }
            
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
                if (_colliders[0] != null && _colliders[0] == other)
                {
                    other.gameObject.layer = layers[0];
                    _colliders[0] = null;
                }
                else if (_colliders[1] != null && _colliders[1] == other)
                {
                    other.gameObject.layer = layers[1];
                    _colliders[1] = null;
                }

                
            }
            else if(!OneColorTeleport)
            {
                if (_colliders[0] != null && _colliders[0] == other)
                {
                    other.gameObject.layer = layers[0];
                    _colliders[0] = null;
                }
                else if (_colliders[1] != null && _colliders[1] == other)
                {
                    other.gameObject.layer = layers[1];
                    _colliders[1] = null;
                }
            }
        }
        
    }

    
}
