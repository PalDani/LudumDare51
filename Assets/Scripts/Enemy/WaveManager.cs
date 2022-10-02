using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{

    [Header("Spawner settings")]
    public Transform spawnPointParent;
    public Transform enemyParent;
    public GameObject[] enemies;

    private List<Transform> spawnPoints;

    [Header("Wave settings")]
    public int minSpawnTime = 5;
    public int maxSpawnTime = 10;
    public int minSpawnAmount = 1;
    public int maxSpawnAmount = 5;
    public float waveMultiplier = 1.25f;

    [Header("Wave info")]
    public int waveCount = 1;


    private void Awake()
    {
        spawnPoints = new List<Transform>();
    }

    void Start()
    {
        LoadEnemies();
        if (enemies.Length == 0)
        {
            print("No enemies found.");
        }
        GetSpawnPoints();

        StartCoroutine(WaveCooldown(0.1f));
    }

    public void LoadEnemies()
    {
        enemies = Resources.LoadAll<GameObject>("Enemies");
    }

    public void GetSpawnPoints()
    {
        foreach (Transform spawnpoint in spawnPointParent)
        {
            spawnPoints.Add(spawnpoint);
        }
        print("Loaded " + spawnPoints.Count + " spawn points.");
    }

    public void SpawnWave()
    {
        int enemyAmount = Mathf.CeilToInt(Random.Range(minSpawnAmount, maxSpawnAmount) * (waveCount * waveMultiplier));
        print("Spawning " + enemyAmount + " enemies.");

        for (int i = 0; i < enemyAmount; i++)
        {
            int enemyIndex = Random.Range(0, enemies.Length);
            GameObject enemy = enemies[enemyIndex];

            int spawnPointIndex = Random.Range(0, spawnPoints.Count);
            Vector2 spawnPos = new Vector2(spawnPoints[spawnPointIndex].position.x, spawnPoints[spawnPointIndex].position.y);

            Vector2 spawnOffset = new Vector2(Random.Range(0.1f, 1), Random.Range(0.1f, 1));

            GameObject spawnedEnemy = Instantiate(enemy, spawnPos + spawnOffset, Quaternion.identity, enemyParent ?? null);
            spawnedEnemy.name = enemy.name;
        }

        waveCount++;
        StartCoroutine(WaveCooldown());
    }

    public IEnumerator WaveCooldown(float wait = 0)
    {
        yield return new WaitUntil(() => !PauseStatus.Instance.IsPaused);


        if (wait == 0)
            wait = Random.Range(minSpawnTime, maxSpawnTime);

        print("Next wave in " + wait + " seconds.");
        yield return new WaitForSeconds(wait);

        SpawnWave();
    }
}
