using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderJumpPlatform : MonoBehaviour
{
    [SerializeField] Vector3 cubeSize = Vector3.one;
    [SerializeField] Vector3 cubeLocalPosition = Vector3.one;
    [SerializeField] protected LayerMask hoverLayerMask = -1;
    [SerializeField] private bool downPlatform = false;
    private Collider[] overlappingColliders;
    private Collider _collider;
    private float timeCheck = 0;
    private bool triggerExit = false;
    // Start is called before the first frame update
    void Start()
    {
        _collider = gameObject.GetComponent<Collider>();
        _collider.isTrigger = true;
        overlappingColliders = new Collider[32];
    }
    
    private void Update()
    {
        Array.Clear(overlappingColliders, 0, overlappingColliders.Length);
        Vector3 wPos = transform.TransformPoint(cubeLocalPosition);
        int numColliding = Physics.OverlapBoxNonAlloc(wPos, cubeSize, overlappingColliders,
            gameObject.transform.rotation, hoverLayerMask, QueryTriggerInteraction.Ignore);
        if (triggerExit)
        {
            
            if (numColliding <= 0)
            {
                timeCheck += Time.deltaTime;
                if (timeCheck > 0.3)
                {
                    _collider.isTrigger = true;
                    triggerExit = false;
                }

            }
            
        }

        if (numColliding > 0)
        {
            
            
            if (overlappingColliders[0].gameObject != null && overlappingColliders[0].gameObject.TryGetComponent(out GamePlayerMovementBase movementBase))
            {
                GameObject player = overlappingColliders[0].gameObject;
                if (!downPlatform)
                {
                    if (player.transform.position.y - movementBase.GetPlayerHeight() * 0.4f >
                        gameObject.transform.position.y)
                    {
                        triggerExit = true;
                        _collider.isTrigger = false;
                        timeCheck = 0f;
                    }
                }
                else
                {
                    if (player.transform.position.y + movementBase.GetPlayerHeight() * 0.4f <
                        gameObject.transform.position.y)
                    {
                        triggerExit = true;
                        _collider.isTrigger = false;
                        timeCheck = 0f;
                    }
                }
            }
        }


    }
    
    private void OnDrawGizmos() {
        Gizmos.color = Color.cyan;
        Vector3 wPos = transform.TransformPoint(cubeLocalPosition);
        Gizmos.DrawCube(wPos, cubeSize*2);
    }
}
