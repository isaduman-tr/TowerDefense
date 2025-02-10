// 10.02.2025 AI-Tag
// This was created with assistance from Muse, a Unity Artificial Intelligence product

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
    public GameObject enemy1Prefab;
    public GameObject enemy2Prefab;
    public GameObject enemy3Prefab;
    public Transform spawnPoint;

    private int currentEnemyType = 0;
    private int[] enemyCounts = { 3, 4, 5 };
    private GameObject[] enemyPrefabs;

    private List<GameObject> currentEnemies = new List<GameObject>();

    void Start()
    {
        enemyPrefabs = new GameObject[] { enemy1Prefab, enemy2Prefab, enemy3Prefab };
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (currentEnemyType < enemyPrefabs.Length)
        {
            yield return SpawnEnemyType(enemyPrefabs[currentEnemyType], enemyCounts[currentEnemyType]);
            yield return new WaitUntil(() => currentEnemies.Count == 0);
            currentEnemyType++;
        }
    }

    IEnumerator SpawnEnemyType(GameObject enemyPrefab, int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject newEnemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
            newEnemy.GetComponent<Enemy>().OnDeath += HandleEnemyDeath;
            currentEnemies.Add(newEnemy);
            yield return new WaitForSeconds(1f); // Spawnlar arasýnda bekleme süresi
        }
    }

    private void HandleEnemyDeath(GameObject enemy)
    {
        currentEnemies.Remove(enemy);
    }
}