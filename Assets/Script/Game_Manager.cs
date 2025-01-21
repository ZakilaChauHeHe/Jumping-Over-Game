using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Game_Manager : MonoBehaviour
{
    public static Game_Manager Instance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("References")]
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject ScoreBoard;
    [SerializeField] private GameObject GameoverPanel;
    [Header("Stored Data")]
    public int score = 0;
    //[SerializeField] private bool GodMode = false;
    public bool Disable_Spawning = false;

    private void Start()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        GameoverPanel.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("InGame");
        Time.timeScale = 1;
    }

    public void UpdateScore()
    {
        score++;
        ScoreBoard.GetComponent<TextMeshProUGUI>().text = "Score: " + score;
    }
}
