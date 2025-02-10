using UnityEngine;

// Enemy adýnda bir sýnýf tanýmlanýr ve bu sýnýf MonoBehaviour'dan türetilmiþtir
public class Enemy : MonoBehaviour
{
    // Düþmanýn baþlangýç pozisyonunu saklamak için bir Vector3 deðiþkeni tanýmlanýr
    private Vector3 startPosition;

    // Düþmanýn saðlýðýný temsil eden bir tam sayý deðiþkeni, baþlangýç deðeri 10
    public int health = 10;

    // EnemyMovement türünde bir referans saklamak için bir deðiþken tanýmlanýr
    private EnemyMovement movementScript;

    // Unity tarafýndan çaðrýlan ve obje baþlatýldýðýnda çalýþan bir fonksiyon
    void Start()
    {
        // Objenin baþlangýç pozisyonu kaydedilir
        startPosition = transform.position;

        // Oyun objesi üzerinde bulunan EnemyMovement scriptine eriþim saðlanýr
        movementScript = GetComponent<EnemyMovement>(); // Movement scriptini elde et
    }

    // Düþman hasar aldýðýnda çaðrýlan bir fonksiyon
    public void TakeDamage()
    {
        // Saðlýk deðeri bir azaltýlýr
        health--;
        // Saðlýk sýfýr veya daha düþükse, düþmaný yok eder
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    // Düþmanýn baþlangýç pozisyonuna dönmesini saðlayan bir fonksiyon
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