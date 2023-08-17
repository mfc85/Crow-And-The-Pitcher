using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class qScripts : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject PauseMenuUI;

    //Drag panel or canvas into this gameobject in the inspector tab. 

    void Start()
    {
        Time.timeScale = 1;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))  
        {
            if (GameIsPaused)
            {
                Resume();
                //can be removed and left only as a button onclick, but its an extra means to resuming game. 
            } else
            {
                Pause();
            }

        }
    }
    //Assign to a button onclick()
    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    //a couple extra scripts I frequently use, in case you need it
    //QuitGame: Uncomment out the unityeditor function when testing, but needs to be re-commented before build
    public void QuitGame()
    {
        Application.Quit();
        //UnityEditor.EditorApplication.isPlaying = false;
    }

    
}
