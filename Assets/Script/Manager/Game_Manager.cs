using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class Game_Manager : MonoBehaviour
{
    public static Game_Manager Instance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("References")]
    [SerializeField] private DataManager dataManager;
    [SerializeField] private GameObject ScoreBoard;
    [SerializeField] private GameObject GameoverPanel;
    [Header("Stored Data")]
    public int Score { get; private set; } = 0;
    public bool Disable_Spawning = false;

    private GameObject Player;
    //private bool Mutator = true;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Player = GameObject.Find("Player");
        LoadGamemode();
    }


    public void GameOver()
    {
        Time.timeScale = 0;
        GameoverPanel.SetActive(true);
    }


    public void RestartGame()
    {
        dataManager.ResetPlayerProfile();
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }

    private void LoadGamemode()
    {
        switch (dataManager.Gamemode)
        {
            case Gamemode.Time:
                Score -= 1;
                StartCoroutine(TimedAddScore());
                break;
            case Gamemode.Score:
                Player.GetComponent<Player_Controller>().OnEnemyKilled.AddListener(AddScore);
                break;
            case Gamemode.Stage:
                break;
        }
    }

    private IEnumerator TimedAddScore()
    {
        while (true) {
            AddScore();
            yield return new WaitForSeconds(1);
        }
    }

    private void AddScore()
    {
        Score++;
        ScoreBoard.GetComponent<ScoreboardController>().UpdateDisplay();
    }
}
