// 11.02.2025 AI-Tag
// This was created with assistance from Muse, a Unity Artificial Intelligence product

using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int totalCoins = 0;

    void Start()
    {
    }

    void Update()
    {
    }

    public void AddCoins(int amount)
    {
        totalCoins += amount;
        Debug.Log("Toplam Coin: " + totalCoins);
    }
}