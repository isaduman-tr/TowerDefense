// 9.02.2025 AI-Tag
// This was created with assistance from Muse, a Unity Artificial Intelligence product

using UnityEngine;

public class TowerAttack : MonoBehaviour
{
    public GameObject shotPrefab;
    public Transform firePoint;
    public float attackInterval = 1f;
    private float attackTimer;
    private int enemyHitCount = 0;
    private GameObject enemy;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("enemy"))
        {
            enemy = other.gameObject;
            attackTimer = attackInterval;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (enemy != null && other.gameObject == enemy)
        {
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0f)
            {
                FireShot();
                attackTimer = attackInterval;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == enemy)
        {
            enemy = null;
        }
    }

    void FireShot()
    {
        GameObject shot = Instantiate(shotPrefab, firePoint.position, firePoint.rotation);
        shot.GetComponent<Shot>().Initialize(enemy);
    }
}