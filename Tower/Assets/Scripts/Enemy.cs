using UnityEngine;


public class Enemy : MonoBehaviour
{
    
    private Vector3 startPosition; // Düþmanýn baþlangýç pozisyonunu saklamak için bir Vector3 deðiþkeni tanýmlanýr

    public int health = 10;// Düþmanýn saðlýðýný temsil eden bir tam sayý deðiþkeni, baþlangýç deðeri 10

    private EnemyMovement movementScript;// EnemyMovement türünde bir referans saklamak için bir deðiþken tanýmlanýr

    void Start()
    {
        
        startPosition = transform.position;// Objenin baþlangýç pozisyonu kaydedilir

        
        movementScript = GetComponent<EnemyMovement>(); // Movement scriptini elde et
    }

    public void TakeDamage()// Düþman hasar aldýðýnda çaðrýlan bir fonksiyon
    {
        
        health--;// Saðlýk deðeri bir azaltýlýr
        // Saðlýk sýfýr veya daha düþükse, düþmaný yok eder
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    // Düþmanýn baþlangýç pozisyonuna dönmesini saðlayan bir fonksiyon þimdilik deaktif
    void ReturnToStart()
    {
        // Düþmanýn pozisyonu baþlangýç pozisyonuna ayarlanýr
        transform.position = startPosition;
        // Saðlýk deðeri varsayýlan deðere sýfýrlanýr
        health = 10; // Saðlýðý sýfýrlar
        // Eðer hareket scripti mevcutsa
        if (movementScript != null)
        {
            // Waypoint indeksini sýfýrlar
            movementScript.ResetWaypointIndex(); // Waypoint indeksini sýfýrlar
        }
    }
}