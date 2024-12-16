using System.Collections;
using UnityEngine;

public class ItemSpawner : MonoBehaviour {
    public GameObject DamageItem;
    public GameObject HealthItem;
    public GameObject ArmorItem;
    
    public int InitiateItemCount = 3;
    public float SpawnInterval = 10f;

    public float[] RangeSpawnDown = { 9.64f, 4.5f, -3.6f, -3.6f };
    public float[] RangeSpawnUp = { 14f, 8f, 12f, -3.6f };

    public void Start() {
        SpawnItems(InitiateItemCount); 
        StartCoroutine(SpawnItemPeriodically(SpawnInterval));
    }

    private void SpawnItems(int itemCount) {
        for (int i = 0; i < itemCount; i++) {
            Instantiate(DamageItem, GetSafeRandomPositionWithinMap(), Quaternion.identity);
            Instantiate(HealthItem, GetSafeRandomPositionWithinMap(), Quaternion.identity);
            Instantiate(ArmorItem, GetSafeRandomPositionWithinMap(), Quaternion.identity);
        }
    }

    private Vector2 GetSafeRandomPositionWithinMap() {
        Vector2 spawnPosition;
        int maxAttempts = 100;

        float[] chosenRange = Random.value < 0.7f ? RangeSpawnDown : RangeSpawnUp;

        do {
            float randomX = Random.Range(chosenRange[3], chosenRange[1]); 
            float randomY = Random.Range(chosenRange[2], chosenRange[0]);
            spawnPosition = new Vector2(randomX, randomY);

            maxAttempts--;
        } while (Physics2D.OverlapCircle(spawnPosition, 0.5f) != null && maxAttempts > 0);

        return spawnPosition;
    }

    private IEnumerator SpawnItemPeriodically(float interval) {
        while (true) {
            yield return new WaitForSeconds(interval); 
            SpawnItems(1); 
        }
    }
}
