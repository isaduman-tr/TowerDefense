using UnityEngine; // Unity motorunun ana s�n�flar�n� kullanabilmek i�in gerekli olan k�t�phaneyi y�kler
using System; // Sistem s�n�flar�n� kullanmak i�in gerekli k�t�phaneyi y�kler

public class Enemy : MonoBehaviour // MonoBehaviour s�n�f�ndan t�retilen bir Enemy s�n�f� tan�mlar
{
    public event Action<GameObject> OnDeath; // 'Enemy' �ld���nde tetiklenecek bir olay (event) tan�mlar

    public int health = 10; // D��man�n ba�lang�� sa�l���n� 10 olarak ayarlar

    public int coinReward; // D��man �ld���nde kazand�raca�� coin miktar�

    public void TakeDamage() // D��man�n hasar almas� i�in bir metod tan�mlar
    {
        health--; // Sa�l�k de�erini bir azalt�r
        if (health <= 0) // Sa�l�k de�eri 0 veya daha d���kse
        {
            OnDeath?.Invoke(gameObject); // OnDeath olay� varsa tetikler ve gameObject (d��man�) olayla birlikte g�nderir
            Destroy(gameObject); // D��man oyun nesnesini yok eder
        }
    }
}