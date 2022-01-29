using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private bool firstCheck = false;
    private bool secondCheck = false;
    
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
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
}
