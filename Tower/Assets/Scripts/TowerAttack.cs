using UnityEngine;
using System.Collections.Generic;

// TowerAttack s�n�f�, Unity'nin MonoBehaviour s�n�f�ndan t�retilmi� bir s�n�ft�r
public class TowerAttack : MonoBehaviour
{
    public GameObject shotPrefab; // At�� i�in kullan�lacak prefab nesnesi
    public Transform firePoint; // At���n ba�layaca�� nokta
    public float attackInterval = 1f; // At��lar aras�ndaki s�re

    private float attackTimer; // At��lar aras�ndaki zamanlay�c�
    private List<GameObject> enemiesInRange = new List<GameObject>(); // Menzile giren d��manlar�n listesi

    void Start()
    {
        attackTimer = attackInterval; // Zamanlay�c�y� ba�lang��ta at�� aral���na ayarla
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("enemy")) // E�er di�er nesne "enemy" etiketiyle etiketlenmi�se
        {
            enemiesInRange.Add(other.gameObject); // D��man� menzil listesine ekle
        }
    }

    void Update()
    {
        if (enemiesInRange.Count > 0) // E�er menzilde d��man varsa
        {
            attackTimer -= Time.deltaTime; // Zamanlay�c�y� azalt
            if (attackTimer <= 0f) // Zamanlay�c� s�f�r�n alt�na d��t�yse
            {
                FireShot(); // At�� yap
                attackTimer = attackInterval; // At�� yapt�ktan sonra zamanlay�c�y� s�f�rla
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("enemy")) // E�er di�er nesne menzilden ��k�yorsa
        {
            enemiesInRange.Remove(other.gameObject); // D��man� menzil listesinden ��kar
        }
    }

    void FireShot()
    {
        enemiesInRange.RemoveAll(enemy => enemy == null); // Null referanslar� temizleyin

        if (enemiesInRange.Count > 0) // E�er menzilde hala d��man varsa
        {
            GameObject targetEnemy = enemiesInRange[0]; // �lk d��man� hedef al
            if (targetEnemy != null)
            {
                GameObject shot = Instantiate(shotPrefab, firePoint.position, firePoint.rotation); // At��� olu�tur
                shot.GetComponent<Shot>().Initialize(targetEnemy); // At��� hedef d��mana y�nlendir
            }
        }
    }
    public void UpgradeTower()
    {
        attackInterval *= 0.8f; // Her y�kseltmede %20 h�zlan�yor
    }

}