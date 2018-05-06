using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuContainer;
    public Button mainMenuButton;
    public Button quitGameButton;
    public bool isPaused = false;

    private void Awake()
    {
        mainMenuButton.onClick.AddListener(LoadMainMenu);
        quitGameButton.onClick.AddListener(QuitGame);
    }

    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            TogglePause(!isPaused);
        }
    }

    public void TogglePause(bool pause)
    {
        Time.timeScale = pause ? 0.0f : 1.0f;
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
