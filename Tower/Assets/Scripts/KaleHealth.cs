using UnityEngine; // Unity motorunun ana sýnýflarýný kullanabilmek için gerekli olan kütüphaneyi yükler

public class KaleHealth : MonoBehaviour // MonoBehaviour sýnýfýndan türetilen bir KaleHealth sýnýfý tanýmlar
{
    public float health = 300; // Kaleye baþlangýç saðlýðý 300 olarak atanýr

    public void KaleStart()
    {
        health = PanelControl.casHealth;
        health *= PanelControl.totalHealth;
    }
    // Kale hasar aldýðýnda bu fonksiyon çaðrýlýr
    public void TakeDamage(int amount) // Hasar miktarýný belirten bir parametre alýr
    {
        health -= amount; // Kale saðlýðýný hasar miktarý kadar azaltýr
        if (health <= 0) // Kale saðlýðý sýfýr veya daha düþükse
        {
            Destroy(gameObject); // Kale oyun nesnesini yok eder
        }
    }
}