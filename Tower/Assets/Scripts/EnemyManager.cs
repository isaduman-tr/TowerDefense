using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class EnemyManager : MonoBehaviour 
{
    public GameObject enemy1Prefab;
    public GameObject enemy2Prefab;
    public GameObject enemy3Prefab;
    public Transform spawnPoint; // Düþmanlarýn ortaya çýkacaðý konumu belirten bir Transform
    public Transform spawnPoint2; // Düþmanlarýn ortaya çýkacaðý konumu belirten bir Transform
    public Transform waypointHolder1;
    public Transform waypointHolder2;



    private int currentEnemyType = 0; // Sýrasýyla hangi düþman türünün spawn edileceðini belirler
    private int[] enemyCounts = { 3, 4, 5 }; // Her düþman türünden kaç tane spawn edileceðini belirten dizisi
    private GameObject[] enemyPrefabs; // Tüm düþman prefablarýný içeren bir dizi

    private List<GameObject> currentEnemies = new List<GameObject>(); // Þu anki aktif düþmanlarý takip eden bir liste

    void Start() // Unity'nin baþlangýç fonksiyonu
    {
        enemyPrefabs = new GameObject[] { enemy1Prefab, enemy2Prefab, enemy3Prefab }; // Düþman prefab dizisini doldurur
       
    }
    public void SpawnEnemy()
    {
         StartCoroutine(SpawnEnemies()); // Düþmanlarý spawn etmeye baþlar
    }
    IEnumerator SpawnEnemies() // Düþmanlarý sýrayla spawn eden bir coroutine
    {
        yield return new WaitForSeconds(2f); // 2 saniye bekle
        while (currentEnemyType < enemyPrefabs.Length) // Tüm düþman türleri spawn edilene kadar devam eder
        {
            yield return SpawnEnemyType(enemyPrefabs[currentEnemyType], enemyCounts[currentEnemyType]); // Belirli türde düþman spawn eder
            yield return new WaitUntil(() => currentEnemies.Count == 0); // Tüm düþmanlar yok edilene kadar bekler
            currentEnemyType++; // Bir sonraki düþman türüne geçer
        }
    }

    IEnumerator SpawnEnemyType(GameObject enemyPrefab, int count) // Belirli bir türde ve sayýda düþman spawn eden coroutine
    {
        for (int i = 0; i < count; i++)
        {
            Transform spawnPos;
            Transform assignedWaypointHolder;

            if (currentEnemyType == 0)
            {
                spawnPos = spawnPoint;
                assignedWaypointHolder = waypointHolder1; // 1. yol
            }
            else if (currentEnemyType == 1)
            {
                spawnPos = spawnPoint2;
                assignedWaypointHolder = waypointHolder2; // 2. yol
            }
            else
            {
                bool fromFirst = i % 2 == 0;
                spawnPos = fromFirst ? spawnPoint : spawnPoint2;
                assignedWaypointHolder = fromFirst ? waypointHolder1 : waypointHolder2;
            }

            GameObject newEnemy = Instantiate(enemyPrefab, spawnPos.position, Quaternion.identity);
            newEnemy.GetComponent<Enemy>().OnDeath += HandleEnemyDeath;

            // Burada güzergahý atýyoruz:
            newEnemy.GetComponent<EnemyMovement2>().InitializeWaypoints(assignedWaypointHolder);

            currentEnemies.Add(newEnemy);
            yield return new WaitForSeconds(1f);
        }
    }



    private void HandleEnemyDeath(GameObject enemy) // Düþman öldüðünde çaðrýlan metod
    {
        currentEnemies.Remove(enemy);

        Enemy enemyScript = enemy.GetComponent<Enemy>(); // Enemy bileþenini al

        if (enemyScript != null)
        {
            FindFirstObjectByType<GameManager>().AddCoins(enemyScript.coinReward);
        }
    }
   
}