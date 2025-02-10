using UnityEngine;


public class Enemy : MonoBehaviour
{
    
    private Vector3 startPosition; // D��man�n ba�lang�� pozisyonunu saklamak i�in bir Vector3 de�i�keni tan�mlan�r

    public int health = 10;// D��man�n sa�l���n� temsil eden bir tam say� de�i�keni, ba�lang�� de�eri 10

    private EnemyMovement movementScript;// EnemyMovement t�r�nde bir referans saklamak i�in bir de�i�ken tan�mlan�r

    void Start()
    {
        
        startPosition = transform.position;// Objenin ba�lang�� pozisyonu kaydedilir

        
        movementScript = GetComponent<EnemyMovement>(); // Movement scriptini elde et
    }

    public void TakeDamage()// D��man hasar ald���nda �a�r�lan bir fonksiyon
    {
        
        health--;// Sa�l�k de�eri bir azalt�l�r
        // Sa�l�k s�f�r veya daha d���kse, d��man� yok eder
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    // D��man�n ba�lang�� pozisyonuna d�nmesini sa�layan bir fonksiyon �imdilik deaktif
    void ReturnToStart()
    {
        // D��man�n pozisyonu ba�lang�� pozisyonuna ayarlan�r
        transform.position = startPosition;
        // Sa�l�k de�eri varsay�lan de�ere s�f�rlan�r
        health = 10; // Sa�l��� s�f�rlar
        // E�er hareket scripti mevcutsa
        if (movementScript != null)
        {
            // Waypoint indeksini s�f�rlar
            movementScript.ResetWaypointIndex(); // Waypoint indeksini s�f�rlar
        }
    }
}