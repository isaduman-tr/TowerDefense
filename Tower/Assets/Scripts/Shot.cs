using UnityEngine;

// Shot ad�nda bir s�n�f tan�mlan�r ve bu s�n�f MonoBehaviour'dan t�retilmi�tir
public class Shot : MonoBehaviour
{
    // Merminin h�z�n� temsil eden bir kayan nokta de�i�keni
    public float speed = 10f;
    // Hedef objeyi saklamak i�in bir GameObject referans�
    private GameObject target;

    // Merminin hedefine atanmas�n� sa�layan fonksiyon
    public void Initialize(GameObject target)
    {
        // Hedef GameObject atan�r
        this.target = target;
    }

    // Unity taraf�ndan her karede bir kez �a�r�lan bir fonksiyon
    void Update()
    {
        // E�er bir hedef varsa
        if (target != null)
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