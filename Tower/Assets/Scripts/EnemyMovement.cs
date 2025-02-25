using UnityEngine; 
using System.Collections; 

public class EnemyMovement : MonoBehaviour
{
    public Transform[] waypoints; // Düşmanın izleyeceği yol noktalarının bir dizisi
    public Transform[] waitingPoints;
    private Transform targetPoint; // Seçilen hedef nokta

    public float speed = 5f; // Düşmanın hareket hızı 
    private int currentWaypointIndex = 0; // Şu anda hedeflenen yol noktasının indexi
    private bool isStopped = false; // Düşmanın durup durmadığını kontrol eden bayrak
    public float stopDuration = 1f; // Düşmanın durup hasar vereceği süre
    public int damageAmount = 1; // Her düşmanın kaleye vereceği hasar miktarı
    internal object occupiedRandomPoints;

    void Update() // Her karede çağrılan Unity fonksiyonu
    {
        if (waypoints.Length == 0 || isStopped) // Eğer yol noktaları yoksa veya düşman duruyorsa
            return; // Fonksiyondan çıkılır

        Transform targetWaypoint = waypoints[currentWaypointIndex]; // Hedef yol noktasını alır
        Vector3 direction = targetWaypoint.position - transform.position; // Hedefe doğru yön vektörü hesaplar

        // Gemi yönünü hedefe doğru yavaşça döndür
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
        }

        // Düşmanı hedefe doğru hareket ettir
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);

        // Düşman hedef yol noktasına ulaştığında
        if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.1f)
        {
            if (targetWaypoint.CompareTag("point")) // Eğer yol noktası "point" etiketine sahipse
            {
               
                isStopped = true; // Düşman durur
                StartCoroutine(DamageKaleAfterDelay(stopDuration)); // Belirli bir süre sonra kaleye hasar vermeye başlar
            }
            else
            {        
                currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;       // Bir sonraki yol noktasına geçer
            }
        }
    }

    private IEnumerator DamageKaleAfterDelay(float delay) // Hasar verme işlemini belirli bir süre sonra başlatan coroutine
    {
        yield return new WaitForSeconds(delay);

        GameObject[] kaleObjects = GameObject.FindGameObjectsWithTag("kale");
        foreach (GameObject kale in kaleObjects)
        {
            KaleHealth kaleHealth = kale.GetComponent<KaleHealth>();
            if (kaleHealth != null)
            {
                kaleHealth.TakeDamage(damageAmount); // Her bir düşman sabit hasar verir
            }
        }
        isStopped = false;
    }

    //private int GetEnemiesAtPointCount() // Belirli bir noktadaki düşman sayısını hesaplayan fonksiyon
    //{
    //    Collider[] colliders = Physics.OverlapSphere(transform.position, 0.1f); // Düşmanın etrafındaki çarpanları kontrol eder
    //    int enemyCount = 0;
    //    foreach (Collider collider in colliders)
    //    {
    //        if (collider.CompareTag("enemy")) // Eğer çarpan "enemy" etiketine sahipse
    //        {
    //            enemyCount++; // Düşman sayısını artır
    //        }
    //    }
    //    return enemyCount; // Toplam düşman sayısını döndür
    //}

    public void ResetWaypointIndex() // Düşmanın yol noktası indeksini sıfırlayan fonksiyon
    {
        currentWaypointIndex = 0; // Yol noktası indeksini başlangıca ayarla
        isStopped = false; // Düşmanın durmasını iptal et
    }
}