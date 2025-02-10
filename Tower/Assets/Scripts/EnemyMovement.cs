using UnityEngine; 
using System.Collections; 

public class EnemyMovement : MonoBehaviour
{
    public Transform[] waypoints; // Düþmanýn izleyeceði yol noktalarýnýn bir dizisi
    public float speed = 5f; // Düþmanýn hareket hýzý
    private int currentWaypointIndex = 0; // Þu anda hedeflenen yol noktasýnýn indexi
    private bool isStopped = false; // Düþmanýn durup durmadýðýný kontrol eden bayrak
    public float stopDuration = 1f; // Düþmanýn durup hasar vereceði süre
    public int damageAmount = 1; // Her düþmanýn kaleye vereceði hasar miktarý

    void Update() // Her karede çaðrýlan Unity fonksiyonu
    {
        if (waypoints.Length == 0 || isStopped) // Eðer yol noktalarý yoksa veya düþman duruyorsa
            return; // Fonksiyondan çýkýlýr

        Transform targetWaypoint = waypoints[currentWaypointIndex]; // Hedef yol noktasýný alýr
        Vector3 direction = targetWaypoint.position - transform.position; // Hedefe doðru yön vektörü hesaplar
        // Düþmaný hedefe doðru hareket ettirir
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);

        // Düþman hedef yol noktasýna ulaþtýðýnda
        if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.1f)
        {
            if (targetWaypoint.CompareTag("point")) // Eðer yol noktasý "point" etiketine sahipse
            {
                isStopped = true; // Düþman durur
                StartCoroutine(DamageKaleAfterDelay(stopDuration)); // Belirli bir süre sonra kaleye hasar vermeye baþlar
            }
            else
            {
                // Bir sonraki yol noktasýna geçer
                currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
            }
        }
    }

    private IEnumerator DamageKaleAfterDelay(float delay) // Hasar verme iþlemini belirli bir süre sonra baþlatan coroutine
    {
        yield return new WaitForSeconds(delay); // Verilen süre kadar bekler

        GameObject[] kaleObjects = GameObject.FindGameObjectsWithTag("kale"); // "kale" etiketli tüm nesneleri bul
        foreach (GameObject kale in kaleObjects)
        {
            KaleHealth kaleHealth = kale.GetComponent<KaleHealth>(); // KaleHealth bileþenini al
            if (kaleHealth != null)
            {
                int totalDamage = damageAmount * GetEnemiesAtPointCount(); // Toplam hasarý hesapla
                kaleHealth.TakeDamage(totalDamage); // Kaleye hasar ver
            }
        }
        isStopped = false; // Hasar verdikten sonra hareket devam eder
    }

    private int GetEnemiesAtPointCount() // Belirli bir noktadaki düþman sayýsýný hesaplayan fonksiyon
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 0.1f); // Düþmanýn etrafýndaki çarpanlarý kontrol eder
        int enemyCount = 0;
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("enemy")) // Eðer çarpan "enemy" etiketine sahipse
            {
                enemyCount++; // Düþman sayýsýný artýr
            }
        }
        return enemyCount; // Toplam düþman sayýsýný döndür
    }

    public void ResetWaypointIndex() // Düþmanýn yol noktasý indeksini sýfýrlayan fonksiyon
    {
        currentWaypointIndex = 0; // Yol noktasý indeksini baþlangýca ayarla
        isStopped = false; // Düþmanýn durmasýný iptal et
    }
}