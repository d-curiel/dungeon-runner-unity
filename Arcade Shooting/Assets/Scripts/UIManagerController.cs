using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManagerController : MonoBehaviour
{
    public static UIManagerController Instance;

    [SerializeField]
    PlayerData playerData;
    [SerializeField]
    Text scoreText;
    [SerializeField]
    Text timeText;
    [SerializeField]
    Image pausePanel;
    [SerializeField]
    Text countDownToPlay;
    [SerializeField]
    Text scoreTextDefeat;
    [SerializeField]
    Text timeTextDefeat;
    [SerializeField]
    Image defeatPanel;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {

    }
    private void FixedUpdate()
    {
        scoreText.text = playerData.CurrentScore.ToString();
        timeText.text = playerData.CurrentDistance.ToString("F2");
        if (!playerData.IsAlive)
        {
            Defeat();
        }
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void Defeat()
    {
        Time.timeScale = 0f;
        scoreTextDefeat.text = playerData.CurrentScore.ToString();
        timeTextDefeat.text = playerData.CurrentDistance.ToString("F2");
        playerData.MaxScore = playerData.MaxScore < playerData.CurrentScore ? playerData.CurrentScore : playerData.MaxScore;
        playerData.MaxDistance = playerData.MaxDistance < playerData.CurrentDistance ? playerData.CurrentDistance : playerData.MaxDistance;
        pausePanel.gameObject.SetActive(true);

    }

    public void PauseGame()
    {
        if(Time.timeScale > 0f)
        {
            Time.timeScale = 0f;
            pausePanel.gameObject.SetActive(true);
        } else
        {
            StartCoroutine(ReturnToGame());
        }
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    IEnumerator ReturnToGame()
    {
        pausePanel.gameObject.SetActive(false);
        countDownToPlay.gameObject.SetActive(true);

        float duration = 3f; 
        while (duration > 0)
        {
            countDownToPlay.text = duration.ToString();
            duration--;
            yield return new WaitForSecondsRealtime(1);
        }
        Time.timeScale = 1f;
        countDownToPlay.gameObject.SetActive(false);
        
    }
}
