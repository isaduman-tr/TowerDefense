// 9.02.2025 AI-Tag
// This was created with assistance from Muse, a Unity Artificial Intelligence product

using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 5f;
    private int currentWaypointIndex = 0;

    void Update()
    {
        if (waypoints.Length == 0)
            return;

        Transform targetWaypoint = waypoints[currentWaypointIndex];
        Vector3 direction = targetWaypoint.position - transform.position;
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.1f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
    }

    public void ResetWaypointIndex()
    {
        currentWaypointIndex = 0; // Ýndeksi sýfýrlar
    }
}