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

    public void LoadMainMenu()
    {
        StartCoroutine(LoadMainMenuAsync());
    }

    public IEnumerator LoadMainMenuAsync()
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);

        while (!async.isDone)
        {
            yield return null;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void TogglePause(bool pause)
    {
        isPaused = pause;
        Cursor.visible = pause;
        pauseMenuContainer.SetActive(pause);
    }
}
