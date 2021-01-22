using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu2 : MonoBehaviour
{
    public static bool IsGamePaused = false;
    public static bool IsMapOpen = false;

    public GameObject PauseMenuUI;
    public Button exitGame;
    public Button exitGame2;

    public GameObject MapUI;


    void Start()
    {
        exitGame.onClick.AddListener(delegate () {
            ExitGame();
        });

        exitGame2.onClick.AddListener(delegate () {
            ExitGame();
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsGamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        if(Input.GetKeyDown(KeyCode.M))
        {
            if(IsMapOpen)
            {
                CloseMap();
            }
            else
            {
                OpenMap();
            }
        }
    }

    void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        IsGamePaused = false;
    }

    void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        IsGamePaused = true;
    }

    void OpenMap()
    {
        MapUI.SetActive(true);
        Time.timeScale = 0f;
        IsMapOpen = true;
    }

    void CloseMap()
    {
        MapUI.SetActive(false);
        Time.timeScale = 1f;
        IsMapOpen = false;
    }

    public void ExitGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        SaveSystem.doLoadFromFile = false;
    }
}
