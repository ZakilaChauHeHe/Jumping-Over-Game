using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game_Manager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("References")]
    [SerializeField] private GameObject ememyPrefab;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject ScoreBoard;
    [SerializeField] private GameObject GameoverPanel;
    [SerializeField] private float TimeBetweenSpawn = 3f;
    [Header("Stored Data")]
    public int score = 0;
    [SerializeField] private bool GodMode = false;
    [SerializeField] private bool Disable_Spawning = false;

    public Action<GameObject> player_Died;
    private float last_Spawn;
    private Vector3 TopRightScreen;

    private void Start()    
    {
        last_Spawn = Time.time;
        TopRightScreen = TopRightScreen = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        if (!GodMode) player_Died += Handle_player_Died;

    }

    private void FixedUpdate()
    {
        if (Time.time >= last_Spawn + TimeBetweenSpawn && !Disable_Spawning)
        {
            last_Spawn = Time.time;
            SpawnEnemy();
        }
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

    private void SpawnEnemy()
    {
        Vector3 spawnPos = new(UnityEngine.Random.value*3, TopRightScreen.y, 0);
        GameObject enemy = Instantiate(ememyPrefab, spawnPos, Quaternion.identity);
        Enemy_Controller enemy_Controller = enemy.GetComponent<Enemy_Controller>();

        Vector3 downwardDirection = UnityEngine.Random.insideUnitCircle.normalized;
        if (downwardDirection.y > 0) downwardDirection *= -1;
        enemy_Controller.ApplyForce(downwardDirection);
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
