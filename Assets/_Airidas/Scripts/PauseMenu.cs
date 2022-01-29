using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseGO;

    void Start()
    {
        pauseGO.SetActive(false);
    }

    void Update()
    {
        var keyboard = Keyboard.current;
        if (keyboard.escapeKey.wasPressedThisFrame && SceneManager.GetActiveScene().buildIndex != 0)
        {
            pauseGO.SetActive(true);
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }
}
