using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    [SerializeField]
    Text highScoreText;
    [SerializeField]
    Text maxTimeAlive;
    [SerializeField]
    PlayerData playerData;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        highScoreText.text = playerData.MaxScore.ToString();
        maxTimeAlive.text = playerData.MaxDistance.ToString();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}
