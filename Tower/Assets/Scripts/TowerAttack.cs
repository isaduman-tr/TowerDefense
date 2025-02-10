// 10.02.2025 AI-Tag
// This was created with assistance from Muse, a Unity Artificial Intelligence product

using UnityEngine;
using System.Collections.Generic;

public class TowerAttack : MonoBehaviour
{
    public GameObject shotPrefab;
    public Transform firePoint;
    public float attackInterval = 1f;

    private float attackTimer;
    private List<GameObject> enemiesInRange = new List<GameObject>();

    void Start()
    {
        attackTimer = attackInterval;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("enemy"))
        {
            enemiesInRange.Add(other.gameObject);
        }
    }

    void Update()
    {
        if (enemiesInRange.Count > 0)
        {
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0f)
            {
                FireShot();
                attackTimer = attackInterval; // Atýþ yaptýktan sonra zamanlayýcýyý sýfýrla
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("enemy"))
        {
            enemiesInRange.Remove(other.gameObject);
        }
    }

    void FireShot()
    {
        // Null referanslarý temizleyin
        enemiesInRange.RemoveAll(enemy => enemy == null);

        if (enemiesInRange.Count > 0)
        {
            GameObject targetEnemy = enemiesInRange[0];
            if (targetEnemy != null)
            {
                GameObject shot = Instantiate(shotPrefab, firePoint.position, firePoint.rotation);
                shot.GetComponent<Shot>().Initialize(targetEnemy);
            }
        }
    }
}