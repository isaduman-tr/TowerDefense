using UnityEngine; // Unity motorunun ana sýnýflarýný kullanabilmek için gerekli olan kütüphaneyi yükler
using System; // Sistem sýnýflarýný kullanmak için gerekli kütüphaneyi yükler

public class Enemy : MonoBehaviour // MonoBehaviour sýnýfýndan türetilen bir Enemy sýnýfý tanýmlar
{
    public event Action<GameObject> OnDeath; // 'Enemy' öldüðünde tetiklenecek bir olay (event) tanýmlar

    public int health = 10; // Düþmanýn baþlangýç saðlýðýný 10 olarak ayarlar

    public int coinReward; // Düþman öldüðünde kazandýracaðý coin miktarý

    public void TakeDamage() // Düþmanýn hasar almasý için bir metod tanýmlar
    {
        health--; // Saðlýk deðerini bir azaltýr
        if (health <= 0) // Saðlýk deðeri 0 veya daha düþükse
        {
            OnDeath?.Invoke(gameObject); // OnDeath olayý varsa tetikler ve gameObject (düþmaný) olayla birlikte gönderir
            Destroy(gameObject); // Düþman oyun nesnesini yok eder
        }
    }
}