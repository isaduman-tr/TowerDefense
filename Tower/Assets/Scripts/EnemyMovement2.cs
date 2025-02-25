using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement2 : MonoBehaviour
{
    
    public Transform[] waypoints = new Transform[4];    // 4 nokta için bir liste oluþturun 
    public Transform[] randomPoints = new Transform[16]; // 16 nokta için bir liste oluþturun 
    public float speed = 2.0f;  // Hareket hýzý
    private int currentWaypointIndex = 0;   // Þu anki hedef nokta

    public GameObject enemyPrefab;
    public int damageAmount = 1;
    private bool isStopped = false;

    private void Start()
    {       
        currentWaypointIndex = 0;   // Ýlk hedef nokta olarak 0. nokta seçilir     
        StartCoroutine(MoveToWaypoint());   // Hareketi baþlat
    }

    private IEnumerator MoveToWaypoint()
    {
        
        while (currentWaypointIndex < waypoints.Length)     // 4 nokta ziyaret edene kadar hareket et
        {      
            Transform targetWaypoint = waypoints[currentWaypointIndex];     // Hedef nokta olarak currentWaypointIndex'i seç
                                                                            // 
            while (Vector3.Distance(transform.position, targetWaypoint.position) > 0.1f)    // Hedef nokta doðru yönde hareket et
            {
                float step = speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, step);
                transform.rotation = Quaternion.LookRotation(targetWaypoint.position - transform.position); // Düþmanlarýn yönünü güncelle
                yield return null;
            }          
            currentWaypointIndex++; // Sonraki hedef nokta olarak currentWaypointIndex + 1'i seç

            if (currentWaypointIndex == waypoints.Length)   // 4. noktaya geldikten sonra random bir noktaya git
            {         
                Transform randomPoint = randomPoints[Random.Range(0, randomPoints.Length)]; // Random bir nokta seç

                while (Vector3.Distance(transform.position, randomPoint.position) > 0.1f)   // Random noktaya git
                {
                    float step = speed * Time.deltaTime;
                    transform.position = Vector3.MoveTowards(transform.position, randomPoint.position, step);
                    transform.rotation = Quaternion.LookRotation(randomPoint.position - transform.position); // Düþmanlarýn yönünü güncelle
                    yield return null;
                }
                InvokeRepeating("Saldiri", 1f, 1f);
                yield break;
            }
        }
    }
    public void Saldiri()
    {
        GameObject[] kaleObjects = GameObject.FindGameObjectsWithTag("kale");
        foreach (GameObject kale in kaleObjects)
        {
            KaleHealth kaleHealth = kale.GetComponent<KaleHealth>();
            if (kaleHealth != null)
            {
                kaleHealth.TakeDamage(damageAmount); // Her bir düþman sabit hasar verir
            }
        }
    }
    
}