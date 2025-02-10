using UnityEngine;

public class KaleHealth : MonoBehaviour
{
    public int health = 300; // Kaleye baþlangýç caný verilir

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