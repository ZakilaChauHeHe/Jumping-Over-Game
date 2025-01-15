using System;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [Header("References")]
    [SerializeField] private Game_Manager GameManager;
    [SerializeField] private GameObject GameBoarder;
    [SerializeField] private GameObject ememyPrefab;
    [SerializeField] private GameObject SpawnObject;
    [SerializeField] private float TimeBetweenSpawn = 1f;

    private float last_Spawn;
    private Transform TopBoarder;

    void Start()
    {
        last_Spawn = Time.time;
        TopBoarder = GameBoarder.transform.Find("TopBoarder").transform;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (Time.time >= last_Spawn + TimeBetweenSpawn && !GameManager.Disable_Spawning)
        {
            last_Spawn = Time.time;
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        Vector3 spawnPos = new(UnityEngine.Random.value * 3, TopBoarder.position.y - TopBoarder.lossyScale.y, 0);
        GameObject enemy = Instantiate(ememyPrefab, spawnPos, Quaternion.identity, SpawnObject.transform);
        Enemy_Controller enemy_Controller = enemy.GetComponent<Enemy_Controller>();

        Vector3 downwardDirection = UnityEngine.Random.insideUnitCircle.normalized;
        if (downwardDirection.y > 0) downwardDirection *= -1;
        enemy_Controller.ApplyForce(downwardDirection);
    }
}
