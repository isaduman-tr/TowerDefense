

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