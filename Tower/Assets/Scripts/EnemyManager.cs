using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class EnemyManager : MonoBehaviour 
{
    public GameObject enemy1Prefab;
    public GameObject enemy2Prefab;
    public GameObject enemy3Prefab;
    public Transform spawnPoint; // D��manlar�n ortaya ��kaca�� konumu belirten bir Transform

    private int currentEnemyType = 0; // S�ras�yla hangi d��man t�r�n�n spawn edilece�ini belirler
    private int[] enemyCounts = { 3, 4, 5 }; // Her d��man t�r�nden ka� tane spawn edilece�ini belirten dizisi
    private GameObject[] enemyPrefabs; // T�m d��man prefablar�n� i�eren bir dizi

    private List<GameObject> currentEnemies = new List<GameObject>(); // �u anki aktif d��manlar� takip eden bir liste

    void Start() // Unity'nin ba�lang�� fonksiyonu
    {
        enemyPrefabs = new GameObject[] { enemy1Prefab, enemy2Prefab, enemy3Prefab }; // D��man prefab dizisini doldurur
        StartCoroutine(SpawnEnemies()); // D��manlar� spawn etmeye ba�lar
    }

    IEnumerator SpawnEnemies() // D��manlar� s�rayla spawn eden bir coroutine
    {
        while (currentEnemyType < enemyPrefabs.Length) // T�m d��man t�rleri spawn edilene kadar devam eder
        {
            yield return SpawnEnemyType(enemyPrefabs[currentEnemyType], enemyCounts[currentEnemyType]); // Belirli t�rde d��man spawn eder
            yield return new WaitUntil(() => currentEnemies.Count == 0); // T�m d��manlar yok edilene kadar bekler
            currentEnemyType++; // Bir sonraki d��man t�r�ne ge�er
        }
    }

    IEnumerator SpawnEnemyType(GameObject enemyPrefab, int count) // Belirli bir t�rde ve say�da d��man spawn eden coroutine
    {
        for (int i = 0; i < count; i++) // Belirtilen say�da d�ng� yapar
        {
            GameObject newEnemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity); // Yeni d��man� olu�turur
            newEnemy.GetComponent<Enemy>().OnDeath += HandleEnemyDeath; // D��man �ld���nde bir olay i�leyici ekler
            currentEnemies.Add(newEnemy); // Yeni d��man� aktif listeye ekler
            yield return new WaitForSeconds(1f); // Spawnlar aras�nda 1 saniye bekler
        }
    }

    private void HandleEnemyDeath(GameObject enemy) // D��man �ld���nde �a�r�lan metod
    {
        currentEnemies.Remove(enemy);

        Enemy enemyScript = enemy.GetComponent<Enemy>(); // Enemy bile�enini al

        if (enemyScript != null)
        {
            FindFirstObjectByType<GameManager>().AddCoins(enemyScript.coinReward);
        }
    }
   
}