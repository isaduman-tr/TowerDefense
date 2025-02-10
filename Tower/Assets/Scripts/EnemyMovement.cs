// 10.02.2025 AI-Tag
// This was created with assistance from Muse, a Unity Artificial Intelligence product

using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 5f;
    private int currentWaypointIndex = 0;
    private bool isStopped = false;
    public float stopDuration = 1f; // 1 saniye bekleyip hasar vermesi
    public int damageAmount = 10; // Kaleye verilecek hasar miktarý

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
                StartCoroutine(DamageKaleAfterDelay(stopDuration));
            }
            else
            {
                currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
            }
        }
    }

    private IEnumerator DamageKaleAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        GameObject[] kaleObjects = GameObject.FindGameObjectsWithTag("kale");
        foreach (GameObject kale in kaleObjects)
        {
            KaleHealth kaleHealth = kale.GetComponent<KaleHealth>();
            if (kaleHealth != null)
            {
                kaleHealth.TakeDamage(damageAmount);
            }
        }
        isStopped = false; // Hasar verdikten sonra hareket devam eder
    }

    public void ResetWaypointIndex()
    {
        currentWaypointIndex = 0;
        isStopped = false;
    }
}