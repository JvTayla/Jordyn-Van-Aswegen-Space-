 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Menu : MonoBehaviour
{
    public void PlayGame()
    {
        // Load the Timer scene additively
        SceneManager.LoadScene("Timer", LoadSceneMode.Additive);

        // Add a callback to be called when the Timer scene is fully loaded
        SceneManager.sceneLoaded += OnTimerSceneLoaded;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    private void OnTimerSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Find the Timer GameObject and start the timer
        Timer timer = GameObject.FindObjectOfType<Timer>();
        if (timer != null)
        {
            timer.StartTimer();
        }
        else
        {
            Debug.LogWarning("Timer script not found in the Timer scene.");
        }

        // Remove the callback to avoid multiple calls
        SceneManager.sceneLoaded -= OnTimerSceneLoaded;
    }
}
