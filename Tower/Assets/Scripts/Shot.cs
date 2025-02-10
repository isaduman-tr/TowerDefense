using UnityEngine;


public class Shot : MonoBehaviour
{
    
    public float speed = 10f;     // Merminin hýzýný temsil eden bir kayan nokta deðiþkeni
    private GameObject target;    // Hedef objeyi saklamak için bir GameObject referansý


    
    public void Initialize(GameObject target)// Merminin hedefine atanmasýný saðlayan fonksiyon
    {
        this.target = target;// Hedef GameObject atanýr
    }

    void Update()
    {
        if (target != null)        // Eðer bir hedef varsa

        {
            // Merminin pozisyonunu hedefe doðru hareket ettirir
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);

            // Mermi hedefe yeterince yaklaþtýysa
            if (Vector3.Distance(transform.position, target.transform.position) < 0.1f)
            {
                // Hedefin Enemy scriptinde bulunan TakeDamage fonksiyonunu çaðýrýr
                target.GetComponent<Enemy>().TakeDamage();
                // Mermiyi sahneden siler
                Destroy(gameObject);
            }
        }
        // Eðer hedef yoksa
        else
        {
            // Mermiyi sahneden siler
            Destroy(gameObject);
        }
    }
}