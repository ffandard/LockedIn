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
        if (!isPaused && Input.GetButtonDown("Pause"))
        {
            Debug.Log("YOLO");
            TogglePause(true);
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
        Debug.Log("TogglePause:" + pause);
        isPaused = pause;
        Cursor.visible = pause;
        pauseMenuContainer.SetActive(pause);
    }
}
