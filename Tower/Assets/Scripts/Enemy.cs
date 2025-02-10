using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Vector3 startPosition; // Ba�lang�� pozisyonunu saklar
    public int health = 10; // D��man�n sa�l���n� temsil eder
    private EnemyMovement movementScript; // Hareket scripti referans�

    // Yeni d��man prefab'i i�in GameObject referans�
    public GameObject enemyPrefab;

    // Yeni d��man�n do�aca�� pozisyon
    public Vector3 spawnPoint;

    void Start()
    {
        startPosition = transform.position; // Ba�lang�� pozisyonunu kaydeder
        movementScript = GetComponent<EnemyMovement>(); // Hareket scriptini al�r
    }

    public void TakeDamage() // D��man hasar ald���nda �a�r�l�r
    {
        health--; // Sa�l��� bir azalt�r
        if (health <= 0) // Sa�l�k s�f�r veya daha az ise
        {
            GameObject newEnemy = Instantiate(enemyPrefab, spawnPoint, Quaternion.identity);

            // Yeni d��man�n sa�l�k de�erini Inspector'da ayarlad���n�z public 'health' de�i�kenine g�re ayarlay�n
            newEnemy.GetComponent<Enemy>().health = 10;

            Destroy(gameObject); // Mevcut d��man� yok eder
            ReturnToStart(); // Ba�lang�� pozisyonuna d�ner
        }
    }

    void ReturnToStart() // Ba�lang�� pozisyonuna d�ner
    {
        transform.position = startPosition; // Pozisyonu ba�lang�ca ayarlar
        health = 10; // Sa�l��� s�f�rlar
        if (movementScript != null) // E�er hareket scripti varsa
        {
            movementScript.ResetWaypointIndex(); // Waypoint indeksini s�f�rlar
        }
    }
}