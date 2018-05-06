using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadGame()
    {
        StartCoroutine(LoadGameAsync());
    }

    public IEnumerator LoadGameAsync()
    {
        AsyncOperation asyncGame = SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
        AsyncOperation asyncPauseMenu = SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive);

        while (!asyncGame.isDone && !asyncPauseMenu.isDone)
        {
            yield return null;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
