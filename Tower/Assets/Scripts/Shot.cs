using UnityEngine; // Unity motorunun ana s�n�flar�n� kullanabilmek i�in gerekli k�t�phaneyi y�kler

public class Shot : MonoBehaviour // MonoBehaviour s�n�f�ndan t�retilen bir Shot s�n�f� tan�mlar
{
    public float speed = 5f; // Merminin h�z�n� temsil eden bir kayan nokta de�i�keni
    private GameObject target; // Hedef objeyi saklamak i�in bir GameObject referans�

    // Merminin hedefine atanmas�n� sa�layan fonksiyon
    public void Initialize(GameObject target)
    {
        this.target = target; // Hedef GameObject atan�r
    }

    void Update() // Her karede �a�r�lan Unity fonksiyonu
    {
        if (target != null) // E�er bir hedef varsa
        {
            // Merminin pozisyonunu hedefe do�ru hareket ettirir
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);

            // Mermi hedefe yeterince yakla�t�ysa
            if (Vector3.Distance(transform.position, target.transform.position) < 0.1f)
            {
                // Hedefin Enemy scriptinde bulunan TakeDamage fonksiyonunu �a��r�r
                target.GetComponent<Enemy>().TakeDamage();
                // Mermiyi sahneden siler
                Destroy(gameObject);
            }
        }
        else // E�er hedef yoksa
        {
            // Mermiyi sahneden siler
            Destroy(gameObject);
        }
    }
}