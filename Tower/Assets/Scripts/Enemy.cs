using UnityEngine;                                   // Unity motorunun ana sýnýflarýný kullanabilmek için gerekli olan kütüphaneyi yükler
using System;                                        // Sistem sýnýflarýný kullanmak için gerekli kütüphaneyi yükler

public class Enemy : MonoBehaviour                   // MonoBehaviour sýnýfýndan türetilen bir Enemy sýnýfý tanýmlar
{
    [Header("Unity Setup")]
    public ParticleSystem deathParticles;

    public GameObject FloatingTextPrefab;

    public event Action<GameObject> OnDeath;           // 'Enemy' öldüðünde tetiklenecek bir olay (event) tanýmlar
    public int StartingHealth = 2;                    // Düþmanýn baþlangýç saðlýðýný 2 olarak ayarlar
    public int CurrentHealth;                          //the current health the enemy has
    public int coinReward;                             // Düþman öldüðünde kazandýracaðý coin miktarý

	private void Awake()
	{
        CurrentHealth = StartingHealth;
    }
	public void TakeDamage()                         // Düþmanýn hasar almasý için bir metod tanýmlar
     {
        if (FloatingTextPrefab)
        {
            ShowFloatingText();
        }

        CurrentHealth--;                               // Saðlýk deðerini bir azaltýr
        if (CurrentHealth <= 0)                            // Saðlýk deðeri 0 veya daha düþükse
        {
            OnDeath?.Invoke(gameObject);           // OnDeath olayý varsa tetikler ve gameObject (düþmaný) olayla birlikte gönderir
            Instantiate(deathParticles, transform.position, Quaternion.identity);
            Destroy(gameObject);                  // Düþman oyun nesnesini yok eder
        }

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }
    void ShowFloatingText()
    {
        var go = Instantiate(FloatingTextPrefab, transform.position, Quaternion.identity, transform);
        go.GetComponent<TextMesh>().text = CurrentHealth.ToString();
    }
    void Die()
    {
        if (deathParticles != null)
        {
            Instantiate(deathParticles, transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Patlama efekti atanmadý! Lütfen explosionEffect prefab'ýný Inspector'da kontrol et.");
            Destroy(gameObject);
        }      
    }
}
