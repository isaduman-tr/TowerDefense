// 10.02.2025 AI-Tag
// This was created with assistance from Muse, a Unity Artificial Intelligence product

using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Vector3 startPosition; // Baþlangýç pozisyonunu saklar
    public int health = 10; // Düþmanýn saðlýðýný temsil eder
    private EnemyMovement movementScript; // Hareket scripti referansý

    // Yeni düþman prefab'i için GameObject referansý
    public GameObject enemyPrefab;

    // Yeni düþmanýn doðacaðý pozisyon
    public Vector3 spawnPoint;

    void Start()
    {
        startPosition = transform.position; // Baþlangýç pozisyonunu kaydeder
        movementScript = GetComponent<EnemyMovement>(); // Hareket scriptini alýr
    }

    public void TakeDamage() // Düþman hasar aldýðýnda çaðrýlýr
    {
        health--; // Saðlýðý bir azaltýr
        if (health <= 0) // Saðlýk sýfýr veya daha az ise
        {
            GameObject newEnemy = Instantiate(enemyPrefab, spawnPoint, Quaternion.identity);

            // Yeni düþmanýn saðlýk deðerini ayarlayýn
            newEnemy.GetComponent<Enemy>().health = 10; // veya baþlangýç için istediðiniz deðer


            Destroy(gameObject); // Mevcut düþmaný yok eder
            ReturnToStart(); // Baþlangýç pozisyonuna döner
        }
    }

    void ReturnToStart() // Baþlangýç pozisyonuna döner
    {
        transform.position = startPosition; // Pozisyonu baþlangýca ayarlar
        health = 10; // Saðlýðý sýfýrlar
        if (movementScript != null) // Eðer hareket scripti varsa
        {
            movementScript.ResetWaypointIndex(); // Waypoint indeksini sýfýrlar
        }
    }
}