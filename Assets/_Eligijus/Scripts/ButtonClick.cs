using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class ButtonClick : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private bool buttonActivated = false;
    [SerializeField] private UnityEvent<bool> buttonPressEvent;
    [SerializeField] private bool enableCameraShake = false;
    [SerializeField] private float cameraShakeDuration = 0.05f;
    [SerializeField] private float cameraShakeAmount = 0.06f;
    
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
        if (!buttonActivated)
        {
            _animator.SetBool("Button", true);
            buttonActivated = true;
            buttonPressEvent.Invoke(true);
            if (enableCameraShake)
            {
                CameraShake.Shake(cameraShakeDuration, cameraShakeAmount);
            }
        }
    }
}
