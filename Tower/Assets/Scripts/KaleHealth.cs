// 10.02.2025 AI-Tag
// This was created with assistance from Muse, a Unity Artificial Intelligence product

using UnityEngine;

public class KaleHealth : MonoBehaviour
{
    public int health = 100; // Kaleye baþlangýç caný verilir

    // Kale hasar aldýðýnda bu fonksiyon çaðrýlýr
    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Destroy(gameObject); // Can sýfýr veya altýna inerse kale yok edilir
        }
    }
}