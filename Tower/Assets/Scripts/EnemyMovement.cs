using UnityEngine;
using System.Collections;

// EnemyMovement adýnda bir sýnýf tanýmlanýr ve bu sýnýf MonoBehaviour'dan türetilmiþtir
public class EnemyMovement : MonoBehaviour
{
    // Düþmanýn izleyeceði yol noktalarýný tutan bir dizi
    public Transform[] waypoints;
    // Düþmanýn hareket hýzýný belirten kayan nokta deðiþkeni
    public float speed = 5f;
    // Þu anki yol noktasýnýn indeksini saklayan tam sayý deðiþkeni
    private int currentWaypointIndex = 0;
    // Düþmanýn durup durmadýðýný belirten mantýksal deðiþken
    private bool isStopped = false;
    // Durma süresi için bir zamanlayýcý deðiþkeni
    private float stopTimer = 0f;
    // Düþmanýn duracaðý süreyi belirten kayan nokta deðiþkeni, varsayýlan 2 saniye
    public float stopDuration = 2f; // 2 saniye

    // Unity tarafýndan her karede bir kez çaðrýlan fonksiyon
    void Update()
    {
        // Eðer yol noktalarý yoksa veya düþman duruyorsa, fonksiyondan çýk
        if (waypoints.Length == 0 || isStopped)
            return;

        // Hedef yol noktasý belirlenir
        Transform targetWaypoint = waypoints[currentWaypointIndex];
        // Hedefe doðru yön belirlenir
        Vector3 direction = targetWaypoint.position - transform.position;
        // Düþman, belirlenen yöne doðru belirtilen hýzla hareket ettirilir
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);

        // Eðer düþman hedef yol noktasýna yeterince yakýnsa
        if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.1f)
        {
            // Eðer hedef nokta "point" etiketine sahipse
            if (targetWaypoint.CompareTag("point"))
            {
                // Düþmanýn durma durumu ayarlanýr
                isStopped = true;
                // Belirtilen süre kadar bekleyip ardýndan kaleyi yok etme iþlemi baþlatýlýr
                StartCoroutine(DestroyKaleAfterDelay(stopDuration));
            }
            else
            {
                // Bir sonraki yol noktasýna geçilir
                currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
            }
        }
    }

    // Belirli bir süre bekledikten sonra "kale" etiketli objeleri yok eden fonksiyon
    private IEnumerator DestroyKaleAfterDelay(float delay)
    {
        // Belirtilen süre kadar beklenir
        yield return new WaitForSeconds(delay);

        // "kale" etiketli tüm oyun objeleri bulunur
        GameObject[] kaleObjects = GameObject.FindGameObjectsWithTag("kale");
        // Her bir kale objesi yok edilir
        foreach (GameObject kale in kaleObjects)
        {
            Destroy(kale);
        }
        // Oyun zamaný durdurulur
        Time.timeScale = 0; // Oyun zamanýný durdurur
    }

    // Yol noktasýný sýfýrlayan ve düþmaný tekrar hareket ettiren fonksiyon
    public void ResetWaypointIndex()
    {
        // Yol noktasý indeksi sýfýrlanýr
        currentWaypointIndex = 0;
        // Durma durumu sýfýrlanýr
        isStopped = false;
        // Durma zamanlayýcýsý sýfýrlanýr
        stopTimer = 0f;
    }
}