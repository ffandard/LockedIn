using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuContainer;
    public bool isPaused = false;

    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            TogglePause(!isPaused);
        }
    }

    public void TogglePause(bool pause)
    {
        isPaused = pause;
        Cursor.visible = pause;
        pauseMenuContainer.SetActive(pause);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
