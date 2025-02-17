using UnityEngine; // Unity motorunun ana sýnýflarýný kullanabilmek için gerekli olan kütüphaneyi yükler
using System; // Sistem sýnýflarýný kullanmak için gerekli kütüphaneyi yükler

public class Enemy : MonoBehaviour // MonoBehaviour sýnýfýndan türetilen bir Enemy sýnýfý tanýmlar
{
    [Header("Unity Setup")]
    public ParticleSystem deathParticles;

    public int health = 10; // Düþmanýn baþlangýç saðlýðýný 10 olarak ayarlar

    public int coinReward; // Düþman öldüðünde kazandýracaðý coin miktarý

    public void TakeDamage() // Düþmanýn hasar almasý için bir metod tanýmlar


    {
        health--; // Saðlýk deðerini bir azaltýr
        if (health <= 0) // Saðlýk deðeri 0 veya daha düþükse
        {
            OnDeath?.Invoke(gameObject); // OnDeath olayý varsa tetikler ve gameObject (düþmaný) olayla birlikte gönderir
            Instantiate(deathParticles, transform.position, Quaternion.identity);
            Destroy(gameObject); // Düþman oyun nesnesini yok eder
        }

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Patlama efekti atanmadý! Lütfen explosionEffect prefab'ýný Inspector'da kontrol et.");
        }

        Destroy(gameObject);
    }

}