using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayerMovementBase : MonoBehaviour
{
    
    [SerializeField] protected Transform hoverPosition;
    [SerializeField] protected float hoverRadius = 1f;
    [SerializeField] protected LayerMask hoverLayerMask = -1;
    protected Collider[] overlappingColliders;
    public float movementIntensity;
    public float maximumSpeed = 10f;
    public float jumpingOppositeForce = 0.6f;
    public Vector3 jump;
    public float jumpForce = 2.0f;
    protected Rigidbody rb;
    private float playerHeight;
    
    private Quaternion angleToRotate;
    private bool startRotate = false;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        playerHeight = gameObject.GetComponent<BoxCollider>().size.y;
        overlappingColliders = new Collider[32];
        rb = gameObject.GetComponent<Rigidbody>();
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

    protected virtual void FixedUpdate()
    {
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
        Gizmos.DrawWireSphere(hoverPosition.position, hoverRadius);
    }
    
}
