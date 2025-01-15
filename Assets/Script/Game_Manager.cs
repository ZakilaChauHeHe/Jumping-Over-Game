using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game_Manager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("References")]
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject ScoreBoard;
    [SerializeField] private GameObject GameoverPanel;
    [Header("Stored Data")]
    public int score = 0;
    [SerializeField] private bool GodMode = false;
    public bool Disable_Spawning = false;

    public Action<GameObject> player_Died;

    private void Start()    
    {
        if (!GodMode) player_Died += Handle_player_Died;

    }

    private void FixedUpdate()
    {
        if (player.GetComponent<Player_Controller>().onGround)
        {
            CleanEnemy();
        }
    }

    private void Handle_player_Died(GameObject _player)
    {
       _player.SetActive(false);
        Time.timeScale = 0;
        GameoverPanel.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("InGame");
        Time.timeScale = 1;
    }


    private void CleanEnemy()
    {
        GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag("Tagged");
        foreach (GameObject taggedObject in taggedObjects)
        {
            UpdateScore();
            taggedObject.GetComponent<Enemy_Controller>()?.FireDestroy();
        }
    }
    private void UpdateScore()
    {
        score++;
        ScoreBoard.GetComponent<TextMeshProUGUI>().text = "Score: " + score;
    }
}
