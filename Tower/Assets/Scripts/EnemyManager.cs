using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class EnemyManager : MonoBehaviour 
{
    public GameObject enemy1Prefab;
    public GameObject enemy2Prefab;
    public GameObject enemy3Prefab;
    public Transform spawnPoint; // Düþmanlarýn ortaya çýkacaðý konumu belirten bir Transform

    private int currentEnemyType = 0; // Sýrasýyla hangi düþman türünün spawn edileceðini belirler
    private int[] enemyCounts = { 3, 4, 5 }; // Her düþman türünden kaç tane spawn edileceðini belirten dizisi
    private GameObject[] enemyPrefabs; // Tüm düþman prefablarýný içeren bir dizi

    private List<GameObject> currentEnemies = new List<GameObject>(); // Þu anki aktif düþmanlarý takip eden bir liste

    void Start() // Unity'nin baþlangýç fonksiyonu
    {
        enemyPrefabs = new GameObject[] { enemy1Prefab, enemy2Prefab, enemy3Prefab }; // Düþman prefab dizisini doldurur
        StartCoroutine(SpawnEnemies()); // Düþmanlarý spawn etmeye baþlar
    }

    IEnumerator SpawnEnemies() // Düþmanlarý sýrayla spawn eden bir coroutine
    {
        while (currentEnemyType < enemyPrefabs.Length) // Tüm düþman türleri spawn edilene kadar devam eder
        {
            yield return SpawnEnemyType(enemyPrefabs[currentEnemyType], enemyCounts[currentEnemyType]); // Belirli türde düþman spawn eder
            yield return new WaitUntil(() => currentEnemies.Count == 0); // Tüm düþmanlar yok edilene kadar bekler
            currentEnemyType++; // Bir sonraki düþman türüne geçer
        }
    }

    IEnumerator SpawnEnemyType(GameObject enemyPrefab, int count) // Belirli bir türde ve sayýda düþman spawn eden coroutine
    {
        for (int i = 0; i < count; i++) // Belirtilen sayýda döngü yapar
        {
            GameObject newEnemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity); // Yeni düþmaný oluþturur
            newEnemy.GetComponent<Enemy>().OnDeath += HandleEnemyDeath; // Düþman öldüðünde bir olay iþleyici ekler
            currentEnemies.Add(newEnemy); // Yeni düþmaný aktif listeye ekler
            yield return new WaitForSeconds(1f); // Spawnlar arasýnda 1 saniye bekler
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