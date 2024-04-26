using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public float timeLeft = 5.0f;
    public Text startText;

    private bool timerStarted = false;

    private void Update()
    {
        if (timerStarted)
        {
            timeLeft -= Time.deltaTime;
            startText.text = Mathf.Max(0, timeLeft).ToString("0");

            if (timeLeft <= 0)
            {
                SceneManager.LoadScene("Game");
            }
        }
    }

    // Public method to start the timer
    public void StartTimer()
    {
        timerStarted = true;
    }
}