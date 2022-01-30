using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    [SerializeField] private Players playersEnum;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float timePass = 2f;
    private float timer = 0f;
    private bool isOnFinish = false;
    private bool finished = false;
    private Collider playerCollider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isOnFinish && !finished)
        {
            
            if (timer >= timePass)
            {
                if (playersEnum == Players.Player1)
                {
                    GameManager.instance.FirstFinish();
                }
                else if(playersEnum == Players.Player2)
                {
                    GameManager.instance.SecondFinish();
                }
                playerCollider.GetComponent<GamePlayerMovementBase>().DisableMovement();
                finished = true;
            }
            else
            {
                timer += Time.deltaTime;
            }
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_layerMask == (_layerMask | (1 << other.gameObject.layer)))
        {
            playerCollider = other;
            timer = 0f;
            isOnFinish = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isOnFinish = false;
    }
}
