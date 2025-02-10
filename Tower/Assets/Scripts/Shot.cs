using UnityEngine;


public class Shot : MonoBehaviour
{
    
    public float speed = 10f;     // Merminin h�z�n� temsil eden bir kayan nokta de�i�keni
    private GameObject target;    // Hedef objeyi saklamak i�in bir GameObject referans�


    
    public void Initialize(GameObject target)// Merminin hedefine atanmas�n� sa�layan fonksiyon
    {
        this.target = target;// Hedef GameObject atan�r
    }

    void Update()
    {
        if (target != null)        // E�er bir hedef varsa

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
        // E�er hedef yoksa
        else
        {
            // Mermiyi sahneden siler
            Destroy(gameObject);
        }
    }
}