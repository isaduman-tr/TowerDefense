using UnityEngine;

// Enemy ad�nda bir s�n�f tan�mlan�r ve bu s�n�f MonoBehaviour'dan t�retilmi�tir
public class Enemy : MonoBehaviour
{
    // D��man�n ba�lang�� pozisyonunu saklamak i�in bir Vector3 de�i�keni tan�mlan�r
    private Vector3 startPosition;

    // D��man�n sa�l���n� temsil eden bir tam say� de�i�keni, ba�lang�� de�eri 10
    public int health = 10;

    // EnemyMovement t�r�nde bir referans saklamak i�in bir de�i�ken tan�mlan�r
    private EnemyMovement movementScript;

    // Unity taraf�ndan �a�r�lan ve obje ba�lat�ld���nda �al��an bir fonksiyon
    void Start()
    {
        // Objenin ba�lang�� pozisyonu kaydedilir
        startPosition = transform.position;

        // Oyun objesi �zerinde bulunan EnemyMovement scriptine eri�im sa�lan�r
        movementScript = GetComponent<EnemyMovement>(); // Movement scriptini elde et
    }

    // D��man hasar ald���nda �a�r�lan bir fonksiyon
    public void TakeDamage()
    {
        // Sa�l�k de�eri bir azalt�l�r
        health--;
        // Sa�l�k s�f�r veya daha d���kse, d��man� yok eder
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    // D��man�n ba�lang�� pozisyonuna d�nmesini sa�layan bir fonksiyon
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