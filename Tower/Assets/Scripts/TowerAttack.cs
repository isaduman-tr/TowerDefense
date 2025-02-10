using UnityEngine;

// TowerAttack ad�nda bir s�n�f tan�mlan�r ve bu s�n�f MonoBehaviour'dan t�retilmi�tir
public class TowerAttack : MonoBehaviour
{
    // At�lan merminin prefab'ini temsil eden bir GameObject
    public GameObject shotPrefab;
    // Merminin ate�lenece�i noktay� temsil eden bir Transform
    public Transform firePoint;
    // Sald�r�lar�n aras�ndaki s�reyi temsil eden bir kayan nokta de�i�keni
    public float attackInterval = 1f;
    // Sald�r� zamanlay�c�s�n� saklayan bir kayan nokta de�i�keni
    private float attackTimer;
    // Vurulan d��man say�s�n� sayan bir tam say� de�i�keni
    private int enemyHitCount = 0;
    // Hedef d��man� saklayan bir GameObject de�i�keni
    private GameObject enemy;

    // Di�er bir obje bu objenin trigger alan�na girdi�inde �a�r�lan fonksiyon
    void OnTriggerEnter(Collider other)
    {
        // E�er obje "enemy" etiketi ta��yorsa
        if (other.CompareTag("enemy"))
        {
            // Hedef d��man olarak bu obje atan�r
            enemy = other.gameObject;
            // Sald�r� zamanlay�c�s�, sald�r� aral���na ayarlan�r
            attackTimer = attackInterval;
        }
    }

    // Di�er bir obje bu objenin trigger alan�nda kald���nda her karede bir kez �a�r�lan fonksiyon
    void OnTriggerStay(Collider other)
    {
        // E�er bir hedef d��man varsa ve bu obje hedef d��mansa
        if (enemy != null && other.gameObject == enemy)
        {
            // Sald�r� zamanlay�c�s�, ge�en s�re kadar azalt�l�r
            attackTimer -= Time.deltaTime;
            // Zamanlay�c� s�f�ra ula�t�ysa veya daha d���kse
            if (attackTimer <= 0f)
            {
                // Ate� etme i�lemi ger�ekle�tirilir
                FireShot();
                // Sald�r� zamanlay�c�s� tekrar sald�r� aral���na ayarlan�r
                attackTimer = attackInterval;
            }
        }
    }

    // Di�er bir obje bu objenin trigger alan�ndan ��kt���nda �a�r�lan fonksiyon
    void OnTriggerExit(Collider other)
    {
        // E�er obje hedef d��mansa
        if (other.gameObject == enemy)
        {
            // Hedef d��man null olarak ayarlan�r
            enemy = null;
        }
    }

    // Mermiyi ate�leyen fonksiyon
    void FireShot()
    {
        // Merminin prefab'inden bir �rnek olu�turulur ve ate�leme noktas�nda konumland�r�l�r
        GameObject shot = Instantiate(shotPrefab, firePoint.position, firePoint.rotation);
        // Olu�turulan mermiye hedef d��man bilgisi iletilir
        shot.GetComponent<Shot>().Initialize(enemy);
    }
}