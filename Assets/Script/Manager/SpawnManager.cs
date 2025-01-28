using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class SpawnEntry
{
    public GameObject Prefab;
    public int weight;
}


public class SpawnManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [Header("References")]
    [SerializeField] private Game_Manager GameManager;
    [SerializeField] private GameObject GameBoarder;
    [SerializeField] private List<SpawnEntry> SpawnTable;
    [HideInInspector] public List<Action<GameObject>> PreSpawnEffects;
    [SerializeField] private float TimeBetweenSpawn = 1f;

    private float last_Spawn;
    private Transform TopBoarder;
    [HideInInspector] public UnityEvent BeforeSpawn;
    void Start()
    {
        last_Spawn = Time.time;
        TopBoarder = GameBoarder.transform.Find("TopBoarder").transform;
        PreSpawnEffects = new List<Action<GameObject>>();
    }

    private void FixedUpdate()
    {
        if (Time.time >= last_Spawn + TimeBetweenSpawn && !GameManager.Disable_Spawning)
        {
            last_Spawn = Time.time;
            SpawnEnemy();
        }
    }

    private GameObject GetRandomEnemyInTable()         // rng enemy to spawn
    {
        int total_Weight = 0;
        foreach (SpawnEntry entry in SpawnTable) total_Weight += entry.weight;
        int randomIndex = UnityEngine.Random.Range(0, total_Weight);
        foreach (SpawnEntry entry in SpawnTable)
        {
            randomIndex -= entry.weight;
            if (randomIndex < 0)
            {
                return entry.Prefab;
            }
        }
        return SpawnTable[0].Prefab;
    } 

    private void SpawnEnemy()
    {
        Vector3 spawnPos = new(UnityEngine.Random.value * 3, TopBoarder.position.y - TopBoarder.lossyScale.y, 0); //spawn posiiton

        GameObject EnemyToSpawn = GetRandomEnemyInTable();
        GameObject enemy = Instantiate(EnemyToSpawn, spawnPos, Quaternion.identity);
        ApplyAllEffects(enemy,PreSpawnEffects);
        Enemy_Controller enemy_Controller = enemy.GetComponent<Enemy_Controller>();
        Vector3 downwardDirection = UnityEngine.Random.insideUnitCircle.normalized;
        if (downwardDirection.y > 0) downwardDirection *= -1;
        enemy_Controller.ApplyForce(downwardDirection);
    }
    private void ApplyAllEffects(GameObject target, List<Action<GameObject>> ApplyEffects)
    {
        foreach (Action<GameObject> effect in ApplyEffects)
        {
            effect.Invoke(target);
        }
    }
}
