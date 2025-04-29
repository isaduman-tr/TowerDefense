using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement2 : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int damageAmount = 1;

    private Transform[] firstPathPoints;       // İlk 4 sabit nokta
    private Transform[] randomTargetPoints;    // 4'ten sonraki 16 rastgele hedef

    private int currentIndex = 0;
    private bool goingToRandomPoint = false;
    private Transform randomTarget;
    public float speed = 2f;
    private bool saldiriBasladi = false; // 🔥 kontrol için flag
    public Transform waypointHolder; // Bu düşmana özel waypoint objesi



    void Start()
    {
        // WaypointHolder tag'lı objeyi bul
       // GameObject waypointHolder = GameObject.FindGameObjectWithTag("WaypointHolder");

        if (waypointHolder != null)
        {
            int total = waypointHolder.transform.childCount;

            // İlk 4 noktayı al
            firstPathPoints = new Transform[4];
            for (int i = 0; i < 4; i++)
            {
                firstPathPoints[i] = waypointHolder.transform.GetChild(i);
            }

            // Geriye kalanları rastgele hedefler olarak ayarla
            int randomCount = total - 4;
            randomTargetPoints = new Transform[randomCount];
            for (int i = 0; i < randomCount; i++)
            {
                randomTargetPoints[i] = waypointHolder.transform.GetChild(i + 4);
            }
        }
        else
        {
            Debug.LogError("WaypointHolder tag'lı obje bulunamadı!");
        }
    }

    void Update()
    {
        if (firstPathPoints == null || randomTargetPoints == null)
            return;

        if (!goingToRandomPoint && currentIndex < firstPathPoints.Length)
        {
            MoveTowards(firstPathPoints[currentIndex]);

            if (Vector3.Distance(transform.position, firstPathPoints[currentIndex].position) < 0.2f)
            {
                currentIndex++;
                if (currentIndex >= firstPathPoints.Length)
                {
                    // İlk 4 geçildi, rastgele hedef seç
                    goingToRandomPoint = true;
                    randomTarget = randomTargetPoints[Random.Range(0, randomTargetPoints.Length)];
                }
            }
        }
        else if (goingToRandomPoint && randomTarget != null)
        {
            MoveTowards(randomTarget);

            if (Vector3.Distance(transform.position, randomTarget.position) < 0.2f && !saldiriBasladi)
            {
                Debug.Log("Düşman rastgele hedefe ulaştı");
                saldiriBasladi = true;
                InvokeRepeating("Saldiri", 1f, 1f);   
            }
        }
    }
    void MoveTowards(Transform target)
    {
        Vector3 dir = (target.position - transform.position).normalized;
        transform.position += dir * speed * Time.deltaTime;
        Vector3 lookDir = new Vector3(dir.x, 0f, dir.z);
        if (lookDir != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(lookDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, Time.deltaTime * 5f);
        }

    }
    public void InitializeWaypoints(Transform holder)
    {
        waypointHolder = holder;

        int total = waypointHolder.childCount;

        firstPathPoints = new Transform[4];
        for (int i = 0; i < 4; i++)
        {
            firstPathPoints[i] = waypointHolder.GetChild(i);
        }

        int randomCount = total - 4;
        randomTargetPoints = new Transform[randomCount];
        for (int i = 0; i < randomCount; i++)
        {
            randomTargetPoints[i] = waypointHolder.GetChild(i + 4);
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
                kaleHealth.TakeDamage(damageAmount); // Her bir düşman sabit hasar verir
            }
        }
    }
    
}