// 10.02.2025 AI-Tag
// This was created with assistance from Muse, a Unity Artificial Intelligence product

using UnityEngine;

public class KaleHealth : MonoBehaviour
{
    public int health = 100; // Kaleye ba�lang�� can� verilir

    // Kale hasar ald���nda bu fonksiyon �a�r�l�r
    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Destroy(gameObject); // Can s�f�r veya alt�na inerse kale yok edilir
        }
    }
}