// 9.02.2025 AI-Tag
// This was created with assistance from Muse, a Unity Artificial Intelligence product

using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Vector3 startPosition;
    private int health = 3;

    void Start()
    {
        startPosition = transform.position;
    }

    public void TakeDamage()
    {
        health--;
        if (health <= 0)
        {
            ReturnToStart();
        }
    }

    void ReturnToStart()
    {
        transform.position = startPosition;
        health = 3; // Saðlýðý sýfýrlar, oyun yeniden baþlar
    }
}