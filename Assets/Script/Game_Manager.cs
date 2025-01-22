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
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject ScoreBoard;
    [SerializeField] private GameObject GameoverPanel;
    [Header("Stored Data")]
    public int Score { get; private set; } = 0;
    public bool Disable_Spawning = false;

    private void Awake()
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

    private void Start()
    {
        LoadGamemode();
    }


    public void GameOver()
    {
        Time.timeScale = 0;
        GameoverPanel.SetActive(true);
    }

    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
        Time.timeScale = 1;
    }

    private void LoadGamemode()
    {
        switch (GamemodeManager.Instance.gamemode)
        {
            case Gamemode.Time:
                StartCoroutine(TimedAddScore());
                break;
            case Gamemode.Score:
                player.GetComponent<Player_Controller>().OnEnemyKilled.AddListener(AddScore);
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
