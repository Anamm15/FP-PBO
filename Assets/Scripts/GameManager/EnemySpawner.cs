using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public GameObject Skeleton;
    public GameObject ArmoredSkeleton;
    public GameObject GreatSwordSkeleton;
    public GameObject Orc;
    public GameObject OrcRider;
    public GameObject EliteOrc;
    public GameObject Wizard;
    public Transform player;

    public float[] RangeSpawnDown = { 11f, 7f, -8.5f, -8.5f };
    public float[] RangeSpawnUp = { 14f, 8f, 11.5f, -5.8f };

    public float randomSpawnInterval = 4f; 
    public float eliteSpawnInterval = 25f;
    public float specialSpawnInterval = 40f;

    private float randomSpawnTimer;
    private float eliteSpawnTimer;
    private float specialSpawnTimer;

    public float minSpawnDistance = 5f;

    private List<GameObject> activeEnemies = new List<GameObject>();

    void Start()
    {
            SpawnSpecificEnemy(Orc);
            SpawnSpecificEnemy(Skeleton);
            SpawnSpecificEnemy(Wizard);

        randomSpawnTimer = randomSpawnInterval;
        eliteSpawnTimer = eliteSpawnInterval;
        specialSpawnTimer = specialSpawnInterval;
    }

    void Update()
    {
        randomSpawnTimer -= Time.deltaTime;
        eliteSpawnTimer -= Time.deltaTime;
        specialSpawnTimer -= Time.deltaTime;

        if (randomSpawnTimer <= 0f)
        {
            SpawnRandomEnemy();
            randomSpawnTimer = randomSpawnInterval;
        }

        if (eliteSpawnTimer <= 0f)
        {
            if (Random.value < 0.5f)
                SpawnSpecificEnemy(Orc);
            else
                SpawnSpecificEnemy(EliteOrc);
            eliteSpawnTimer = eliteSpawnInterval;
        }

        if (specialSpawnTimer <= 0f)
        {
            if (Random.value < 0.5f)
                SpawnSpecificEnemy(OrcRider);
            else
                SpawnSpecificEnemy(GreatSwordSkeleton);
            specialSpawnTimer = specialSpawnInterval;
        }

        activeEnemies.RemoveAll(enemy => enemy == null);
    }

    private void SpawnRandomEnemy()
    {
        GameObject[] randomEnemies = { Orc, Skeleton, Wizard };
        GameObject enemyPrefab = randomEnemies[Random.Range(0, randomEnemies.Length)];
        SpawnSpecificEnemy(enemyPrefab);
    }

    private void SpawnSpecificEnemy(GameObject enemyPrefab)
    {
        Vector2 spawnPosition = GetRandomSpawnPosition();
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        activeEnemies.Add(newEnemy);
    }

    private Vector2 GetRandomSpawnPosition()
    {
        float[] chosenRange = Random.value < 0.7f ? RangeSpawnDown : RangeSpawnUp;

        float randomX = Random.Range(chosenRange[3], chosenRange[1]); 
        float randomY = Random.Range(chosenRange[2], chosenRange[0]);

        Vector2 spawnPosition = new Vector2(randomX, randomY);

        while (Vector2.Distance(spawnPosition, player.position) < minSpawnDistance)
        {
            randomX = Random.Range(chosenRange[3], chosenRange[1]); 
            randomY = Random.Range(chosenRange[2], chosenRange[0]);
            spawnPosition = new Vector2(randomX, randomY);
        }

        return spawnPosition;
    }
}
