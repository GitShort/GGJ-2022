using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private bool firstCheck = false;
    private bool secondCheck = false;
    private float lastWidth;
    private float lastHeight;
    public Vector2[] resolutions;
    private Keyboard _keyboard;
    void OnEnable()
    {
        _keyboard = Keyboard.current;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(1920, 1080, FullScreenMode.FullScreenWindow);
        instance = this;
    }

    public void SetResolution(int index)
    {
        Screen.SetResolution((int)resolutions[index].y, (int)resolutions[index].y, FullScreenMode.FullScreenWindow);
    }


    public void FirstFinish()
    {
        firstCheck = true;
        if (secondCheck)
        {
            NextLevel();
        }
        
    }

    public void SecondFinish()
    {
        secondCheck = true;
        if (firstCheck)
        {
            NextLevel();
        }

    }
    
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        firstCheck = false;
        secondCheck = false;
    }


    public void NextLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex + 1 < SceneManager.sceneCountInBuildSettings)
        {

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    private void Update()
    {
        
        if (_keyboard.rKey.wasPressedThisFrame && SceneManager.GetActiveScene().buildIndex != 0 && SceneManager.GetActiveScene().buildIndex != SceneManager.sceneCountInBuildSettings-1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
        }
    }
}
