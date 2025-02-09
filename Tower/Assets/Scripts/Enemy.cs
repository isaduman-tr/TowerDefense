// 9.02.2025 AI-Tag
// This was created with assistance from Muse, a Unity Artificial Intelligence product

using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Vector3 startPosition;
    private int health = 3;
    private EnemyMovement movementScript;

    void Start()
    {
        startPosition = transform.position;
        movementScript = GetComponent<EnemyMovement>(); // Movement scriptini elde et
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
        health = 3; // Saðlýðý sýfýrlar
        if (movementScript != null)
        {
            movementScript.ResetWaypointIndex(); // Waypoint indeksini sýfýrlar
        }
    }
}