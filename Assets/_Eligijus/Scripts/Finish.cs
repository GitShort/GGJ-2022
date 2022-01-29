using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    [SerializeField] private Players playersEnum;
    [SerializeField] private LayerMask _layerMask;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_layerMask == (_layerMask | (1 << other.gameObject.layer)))
        {
            if (playersEnum == Players.Player1)
            {
                GameManager.instance.FirstFinish();
            }
            else if(playersEnum == Players.Player2)
            {
                GameManager.instance.SecondFinish();
            }
        }
    }
}
