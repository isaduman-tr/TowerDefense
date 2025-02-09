// 9.02.2025 AI-Tag
// This was created with assistance from Muse, a Unity Artificial Intelligence product

using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 5f;
    private int currentWaypointIndex = 0;
    private bool isStopped = false;
    private float stopTimer = 0f;
    public float stopDuration = 2f; // 2 saniye

    void Update()
    {
        if (waypoints.Length == 0 || isStopped)
            return;

        Transform targetWaypoint = waypoints[currentWaypointIndex];
        Vector3 direction = targetWaypoint.position - transform.position;
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.1f)
        {
            if (targetWaypoint.CompareTag("point"))
            {
                isStopped = true;
                StartCoroutine(DestroyKaleAfterDelay(stopDuration));
            }
            else
            {
                currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
            }
        }
    }

    private IEnumerator DestroyKaleAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        GameObject[] kaleObjects = GameObject.FindGameObjectsWithTag("kale");
        foreach (GameObject kale in kaleObjects)
        {
            Destroy(kale);
        }
        // Oyun durdurma
        Time.timeScale = 0; // Oyun zamanýný durdurur
    }

    public void ResetWaypointIndex()
    {
        currentWaypointIndex = 0;
        isStopped = false;
        stopTimer = 0f;
    }
}