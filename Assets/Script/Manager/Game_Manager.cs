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
    [SerializeField] private GameStateManager GSManager;
    [SerializeField] private GameObject ScoreBoard;
    [SerializeField] private GameObject GameoverPanel;
    [Header("Stored Data")]

    [SerializeField] private bool DisableBuff = false;

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
                StartCoroutine(TimedAddScore());
                break;
            case Gamemode.Score:
                Player.GetComponent<Player_Controller>().OnEnemyKilled.AddListener(GainScore);
                break;
            case Gamemode.Stage:
                break;
        }
    }

    private IEnumerator TimedAddScore()
    {
        yield return new WaitUntil(() => GSManager.CurrentState == GameState.InGame);
        while (true) {
            yield return new WaitForSeconds(1);
            GainScore();
        }
    }

    private void GainScore()
    {
        dataManager.AddScore(1);
        ScoreBoard.GetComponent<ScoreboardController>().UpdateDisplay();
        if(!DisableBuff && dataManager.Score % 10 == 0)
        {
            LevelLoader.instance.LoadRewardBuffScene();
        }
    }
}
