using UnityEngine;

// Shot adýnda bir sýnýf tanýmlanýr ve bu sýnýf MonoBehaviour'dan türetilmiþtir
public class Shot : MonoBehaviour
{
    // Merminin hýzýný temsil eden bir kayan nokta deðiþkeni
    public float speed = 10f;
    // Hedef objeyi saklamak için bir GameObject referansý
    private GameObject target;

    // Merminin hedefine atanmasýný saðlayan fonksiyon
    public void Initialize(GameObject target)
    {
        // Hedef GameObject atanýr
        this.target = target;
    }

    // Unity tarafýndan her karede bir kez çaðrýlan bir fonksiyon
    void Update()
    {
        // Eðer bir hedef varsa
        if (target != null)
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