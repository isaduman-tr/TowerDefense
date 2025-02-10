using UnityEngine; 
using System.Collections; 

public class EnemyMovement : MonoBehaviour
{
    public Transform[] waypoints; // D��man�n izleyece�i yol noktalar�n�n bir dizisi
    public float speed = 5f; // D��man�n hareket h�z�
    private int currentWaypointIndex = 0; // �u anda hedeflenen yol noktas�n�n indexi
    private bool isStopped = false; // D��man�n durup durmad���n� kontrol eden bayrak
    public float stopDuration = 1f; // D��man�n durup hasar verece�i s�re
    public int damageAmount = 1; // Her d��man�n kaleye verece�i hasar miktar�

    void Update() // Her karede �a�r�lan Unity fonksiyonu
    {
        if (waypoints.Length == 0 || isStopped) // E�er yol noktalar� yoksa veya d��man duruyorsa
            return; // Fonksiyondan ��k�l�r

        Transform targetWaypoint = waypoints[currentWaypointIndex]; // Hedef yol noktas�n� al�r
        Vector3 direction = targetWaypoint.position - transform.position; // Hedefe do�ru y�n vekt�r� hesaplar
        // D��man� hedefe do�ru hareket ettirir
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);

        // D��man hedef yol noktas�na ula�t���nda
        if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.1f)
        {
            if (targetWaypoint.CompareTag("point")) // E�er yol noktas� "point" etiketine sahipse
            {
                isStopped = true; // D��man durur
                StartCoroutine(DamageKaleAfterDelay(stopDuration)); // Belirli bir s�re sonra kaleye hasar vermeye ba�lar
            }
            else
            {
                // Bir sonraki yol noktas�na ge�er
                currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
            }
        }
    }

    private IEnumerator DamageKaleAfterDelay(float delay) // Hasar verme i�lemini belirli bir s�re sonra ba�latan coroutine
    {
        yield return new WaitForSeconds(delay); // Verilen s�re kadar bekler

        GameObject[] kaleObjects = GameObject.FindGameObjectsWithTag("kale"); // "kale" etiketli t�m nesneleri bul
        foreach (GameObject kale in kaleObjects)
        {
            KaleHealth kaleHealth = kale.GetComponent<KaleHealth>(); // KaleHealth bile�enini al
            if (kaleHealth != null)
            {
                int totalDamage = damageAmount * GetEnemiesAtPointCount(); // Toplam hasar� hesapla
                kaleHealth.TakeDamage(totalDamage); // Kaleye hasar ver
            }
        }
        isStopped = false; // Hasar verdikten sonra hareket devam eder
    }

    private int GetEnemiesAtPointCount() // Belirli bir noktadaki d��man say�s�n� hesaplayan fonksiyon
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 0.1f); // D��man�n etraf�ndaki �arpanlar� kontrol eder
        int enemyCount = 0;
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("enemy")) // E�er �arpan "enemy" etiketine sahipse
            {
                enemyCount++; // D��man say�s�n� art�r
            }
        }
        return enemyCount; // Toplam d��man say�s�n� d�nd�r
    }

    public void ResetWaypointIndex() // D��man�n yol noktas� indeksini s�f�rlayan fonksiyon
    {
        currentWaypointIndex = 0; // Yol noktas� indeksini ba�lang�ca ayarla
        isStopped = false; // D��man�n durmas�n� iptal et
    }
}