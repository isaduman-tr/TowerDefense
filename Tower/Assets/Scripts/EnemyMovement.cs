using UnityEngine;
using System.Collections;

// EnemyMovement ad�nda bir s�n�f tan�mlan�r ve bu s�n�f MonoBehaviour'dan t�retilmi�tir
public class EnemyMovement : MonoBehaviour
{
    // D��man�n izleyece�i yol noktalar�n� tutan bir dizi
    public Transform[] waypoints;
    // D��man�n hareket h�z�n� belirten kayan nokta de�i�keni
    public float speed = 5f;
    // �u anki yol noktas�n�n indeksini saklayan tam say� de�i�keni
    private int currentWaypointIndex = 0;
    // D��man�n durup durmad���n� belirten mant�ksal de�i�ken
    private bool isStopped = false;
    // Durma s�resi i�in bir zamanlay�c� de�i�keni
    private float stopTimer = 0f;
    // D��man�n duraca�� s�reyi belirten kayan nokta de�i�keni, varsay�lan 2 saniye
    public float stopDuration = 2f; // 2 saniye

    // Unity taraf�ndan her karede bir kez �a�r�lan fonksiyon
    void Update()
    {
        // E�er yol noktalar� yoksa veya d��man duruyorsa, fonksiyondan ��k
        if (waypoints.Length == 0 || isStopped)
            return;

        // Hedef yol noktas� belirlenir
        Transform targetWaypoint = waypoints[currentWaypointIndex];
        // Hedefe do�ru y�n belirlenir
        Vector3 direction = targetWaypoint.position - transform.position;
        // D��man, belirlenen y�ne do�ru belirtilen h�zla hareket ettirilir
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);

        // E�er d��man hedef yol noktas�na yeterince yak�nsa
        if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.1f)
        {
            // E�er hedef nokta "point" etiketine sahipse
            if (targetWaypoint.CompareTag("point"))
            {
                // D��man�n durma durumu ayarlan�r
                isStopped = true;
                // Belirtilen s�re kadar bekleyip ard�ndan kaleyi yok etme i�lemi ba�lat�l�r
                StartCoroutine(DestroyKaleAfterDelay(stopDuration));
            }
            else
            {
                // Bir sonraki yol noktas�na ge�ilir
                currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
            }
        }
    }

    // Belirli bir s�re bekledikten sonra "kale" etiketli objeleri yok eden fonksiyon
    private IEnumerator DestroyKaleAfterDelay(float delay)
    {
        // Belirtilen s�re kadar beklenir
        yield return new WaitForSeconds(delay);

        // "kale" etiketli t�m oyun objeleri bulunur
        GameObject[] kaleObjects = GameObject.FindGameObjectsWithTag("kale");
        // Her bir kale objesi yok edilir
        foreach (GameObject kale in kaleObjects)
        {
            Destroy(kale);
        }
        // Oyun zaman� durdurulur
        Time.timeScale = 0; // Oyun zaman�n� durdurur
    }

    // Yol noktas�n� s�f�rlayan ve d��man� tekrar hareket ettiren fonksiyon
    public void ResetWaypointIndex()
    {
        // Yol noktas� indeksi s�f�rlan�r
        currentWaypointIndex = 0;
        // Durma durumu s�f�rlan�r
        isStopped = false;
        // Durma zamanlay�c�s� s�f�rlan�r
        stopTimer = 0f;
    }
}