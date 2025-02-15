using UnityEngine;
using System.Collections.Generic;

// TowerAttack sýnýfý, Unity'nin MonoBehaviour sýnýfýndan türetilmiþ bir sýnýftýr
public class TowerAttack : MonoBehaviour
{
    public GameObject shotPrefab; // Atýþ için kullanýlacak prefab nesnesi
    public Transform firePoint; // Atýþýn baþlayacaðý nokta
    public float attackInterval = 1f; // Atýþlar arasýndaki süre

    private float attackTimer; // Atýþlar arasýndaki zamanlayýcý
    private List<GameObject> enemiesInRange = new List<GameObject>(); // Menzile giren düþmanlarýn listesi

    void Start()
    {
        attackTimer = attackInterval; // Zamanlayýcýyý baþlangýçta atýþ aralýðýna ayarla
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("enemy")) // Eðer diðer nesne "enemy" etiketiyle etiketlenmiþse
        {
            enemiesInRange.Add(other.gameObject); // Düþmaný menzil listesine ekle
        }
    }

    void Update()
    {
        if (enemiesInRange.Count > 0) // Eðer menzilde düþman varsa
        {
            attackTimer -= Time.deltaTime; // Zamanlayýcýyý azalt
            if (attackTimer <= 0f) // Zamanlayýcý sýfýrýn altýna düþtüyse
            {
                FireShot(); // Atýþ yap
                attackTimer = attackInterval; // Atýþ yaptýktan sonra zamanlayýcýyý sýfýrla
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("enemy")) // Eðer diðer nesne menzilden çýkýyorsa
        {
            enemiesInRange.Remove(other.gameObject); // Düþmaný menzil listesinden çýkar
        }
    }

    void FireShot()
    {
        enemiesInRange.RemoveAll(enemy => enemy == null); // Null referanslarý temizleyin

        if (enemiesInRange.Count > 0) // Eðer menzilde hala düþman varsa
        {
            GameObject targetEnemy = enemiesInRange[0]; // Ýlk düþmaný hedef al
            if (targetEnemy != null)
            {
                GameObject shot = Instantiate(shotPrefab, firePoint.position, firePoint.rotation); // Atýþý oluþtur
                shot.GetComponent<Shot>().Initialize(targetEnemy); // Atýþý hedef düþmana yönlendir
            }
        }
    }
    public void UpgradeTower()
    {
        attackInterval *= 0.8f; // Her yükseltmede %20 hýzlanýyor
    }

}