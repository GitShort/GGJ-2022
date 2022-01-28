using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
public class InputWraper : MonoBehaviour
{
    [SerializeField] private UnityEvent<Vector2> onMoveInput;
    [SerializeField] private UnityEvent<float> onUpInput;
    [SerializeField] private UnityEvent<float> onDownInput;
    
    private void OnDown(InputValue movementValue)
    {
        float movementVector = movementValue.Get<float>();

        onDownInput.Invoke(movementVector);

    }

    private void OnMove(InputValue movementValue)
    {

        Vector2 movementVector = movementValue.Get<Vector2>();
        onMoveInput.Invoke(movementVector);

    }
    
    private void OnUp(InputValue movementValue)
    {
        float movementVector = movementValue.Get<float>();
        onUpInput.Invoke(movementVector);
    }
}
