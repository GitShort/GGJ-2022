using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayerMovementBase : MonoBehaviour
{
    
    [SerializeField] protected Transform hoverPosition;
    [SerializeField] protected Vector3 cubeSize = Vector3.one;
    [SerializeField] protected LayerMask hoverLayerMask = -1;
    [SerializeField] protected LayerMask rayHitMask = -1;
    protected Collider[] overlappingColliders;
    public float movementIntensity;
    public float maximumSpeed = 10f;
    public float jumpingOppositeForce = 0.6f;
    public Vector3 jump;
    public float jumpForce = 2.0f;
    [SerializeField] private Animator _animator;
    protected Rigidbody rb;
    private float playerHeight;
    private Gravity _gravity;
    private bool teleport = false;
    private Vector3 forcePositionVector;
    
    private Quaternion angleToRotate;
    private bool startRotate = false;
    protected bool movementEnabled = true;

    
    // Start is called before the first frame update
    protected virtual void Start()
    {
        _gravity = gameObject.GetComponent<Gravity>();
        playerHeight = gameObject.GetComponent<BoxCollider>().size.y;
        overlappingColliders = new Collider[32];
        rb = gameObject.GetComponent<Rigidbody>();
        // rb.centerOfMass = Vector3.zero;
        
        angleToRotate = transform.rotation;
    }

    public float GetPlayerHeight()
    {
        return playerHeight;
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
            angleToRotate.w = 0;
        }
        else
        {
            angleToRotate.z = 0;
            angleToRotate.w = 1;
        }
    }

    public void StartTeleportation(Vector3 forcePositionVector)
    {
        teleport = true;
        this.forcePositionVector = forcePositionVector;
        AudioManager.instance.PlaySound("Portal", this.gameObject);
    }

    public void DisableMovement()
    {
        movementEnabled = false;
    }

    RaycastHit hitBottom;
    RaycastHit hitTop;
    

    protected virtual void FixedUpdate()
    {
        if (_animator != null)
        {
            if (rb.velocity.magnitude >= 0.01f)
            {
                _animator.SetBool("isWalking", true);
            }
            else
            {
                _animator.SetBool("isWalking", false);
            }
        }

        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hitBottom, 0.5f))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hitBottom.distance, Color.yellow);
        }
        
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), out hitTop, 0.5f))
        {
            if (teleport)
            {
                jump = jump * -1;
                _gravity.gravityForce = _gravity.gravityForce * -1;
                JumpOnImpulse(forcePositionVector);
                ApplyRotation();
                teleport = false;
            }

            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.up) * hitTop.distance, Color.blue); // jei virsus hitina galima atlikti teleportavima
        }
        
        if (startRotate)
        {

            if (Vector3.Distance(transform.eulerAngles, angleToRotate.eulerAngles) > 0.01f)
            {
                transform.rotation = Lerp(transform.rotation, angleToRotate, 3.0f * Time.deltaTime, false);

            }
            else
            {
                transform.rotation = angleToRotate;
                startRotate = false;
            }
        }
    }
    
    public static Quaternion Lerp(Quaternion p, Quaternion q, float t, bool shortWay)
    {
        if (shortWay)
        {
            float dot = Quaternion.Dot(p, q);
            if (dot < 0.0f)
                return Lerp(ScalarMultiply(p, -1.0f), q, t, true);
        }
 
        Quaternion r = Quaternion.identity;
        r.x = p.x * (1f - t) + q.x * (t);
        r.y = p.y * (1f - t) + q.y * (t);
        r.z = p.z * (1f - t) + q.z * (t);
        r.w = p.w * (1f - t) + q.w * (t);
        return r;
    }
    
    public static Quaternion ScalarMultiply(Quaternion input, float scalar)
    {
        return new Quaternion(input.x * scalar, input.y * scalar, input.z * scalar, input.w * scalar);
    }
    private void OnDrawGizmos() {
        Gizmos.color = Color.cyan;
        Gizmos.DrawCube(hoverPosition.transform.position, cubeSize*2);
    }
    
}
