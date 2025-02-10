using UnityEngine; // Unity motorunun ana s�n�flar�n� kullanabilmek i�in gerekli olan k�t�phaneyi y�kler

public class KaleHealth : MonoBehaviour // MonoBehaviour s�n�f�ndan t�retilen bir KaleHealth s�n�f� tan�mlar
{
    public int health = 300; // Kaleye ba�lang�� sa�l��� 300 olarak atan�r

    // Kale hasar ald���nda bu fonksiyon �a�r�l�r
    public void TakeDamage(int amount) // Hasar miktar�n� belirten bir parametre al�r
    {
        health -= amount; // Kale sa�l���n� hasar miktar� kadar azalt�r
        if (health <= 0) // Kale sa�l��� s�f�r veya daha d���kse
        {
            Destroy(gameObject); // Kale oyun nesnesini yok eder
        }
    }
}