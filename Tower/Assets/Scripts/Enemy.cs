// 10.02.2025 AI-Tag
// This was created with assistance from Muse, a Unity Artificial Intelligence product

using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    public event Action<GameObject> OnDeath;

    public int health = 10;

    public void TakeDamage()
    {
        health--;
        if (health <= 0)
        {
            OnDeath?.Invoke(gameObject);
            Destroy(gameObject);
        }
    }
}