using UnityEngine;

// TowerAttack adýnda bir sýnýf tanýmlanýr ve bu sýnýf MonoBehaviour'dan türetilmiþtir
public class TowerAttack : MonoBehaviour
{
    // Atýlan merminin prefab'ini temsil eden bir GameObject
    public GameObject shotPrefab;
    // Merminin ateþleneceði noktayý temsil eden bir Transform
    public Transform firePoint;
    // Saldýrýlarýn arasýndaki süreyi temsil eden bir kayan nokta deðiþkeni
    public float attackInterval = 1f;
    // Saldýrý zamanlayýcýsýný saklayan bir kayan nokta deðiþkeni
    private float attackTimer;
    // Vurulan düþman sayýsýný sayan bir tam sayý deðiþkeni
    private int enemyHitCount = 0;
    // Hedef düþmaný saklayan bir GameObject deðiþkeni
    private GameObject enemy;

    // Diðer bir obje bu objenin trigger alanýna girdiðinde çaðrýlan fonksiyon
    void OnTriggerEnter(Collider other)
    {
        // Eðer obje "enemy" etiketi taþýyorsa
        if (other.CompareTag("enemy"))
        {
            // Hedef düþman olarak bu obje atanýr
            enemy = other.gameObject;
            // Saldýrý zamanlayýcýsý, saldýrý aralýðýna ayarlanýr
            attackTimer = attackInterval;
        }
    }

    // Diðer bir obje bu objenin trigger alanýnda kaldýðýnda her karede bir kez çaðrýlan fonksiyon
    void OnTriggerStay(Collider other)
    {
        // Eðer bir hedef düþman varsa ve bu obje hedef düþmansa
        if (enemy != null && other.gameObject == enemy)
        {
            // Saldýrý zamanlayýcýsý, geçen süre kadar azaltýlýr
            attackTimer -= Time.deltaTime;
            // Zamanlayýcý sýfýra ulaþtýysa veya daha düþükse
            if (attackTimer <= 0f)
            {
                // Ateþ etme iþlemi gerçekleþtirilir
                FireShot();
                // Saldýrý zamanlayýcýsý tekrar saldýrý aralýðýna ayarlanýr
                attackTimer = attackInterval;
            }
        }
    }

    // Diðer bir obje bu objenin trigger alanýndan çýktýðýnda çaðrýlan fonksiyon
    void OnTriggerExit(Collider other)
    {
        // Eðer obje hedef düþmansa
        if (other.gameObject == enemy)
        {
            // Hedef düþman null olarak ayarlanýr
            enemy = null;
        }
    }

    // Mermiyi ateþleyen fonksiyon
    void FireShot()
    {
        // Merminin prefab'inden bir örnek oluþturulur ve ateþleme noktasýnda konumlandýrýlýr
        GameObject shot = Instantiate(shotPrefab, firePoint.position, firePoint.rotation);
        // Oluþturulan mermiye hedef düþman bilgisi iletilir
        shot.GetComponent<Shot>().Initialize(enemy);
    }
}