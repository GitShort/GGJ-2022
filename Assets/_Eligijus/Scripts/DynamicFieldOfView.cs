using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicFieldOfView : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int fieldOfView = (1080 * 60) / Screen.currentResolution.height;
        Camera mainCamera = gameObject.GetComponent<Camera>();
        mainCamera.fieldOfView = fieldOfView;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
