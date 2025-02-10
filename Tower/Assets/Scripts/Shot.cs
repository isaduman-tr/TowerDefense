using UnityEngine; // Unity motorunun ana sýnýflarýný kullanabilmek için gerekli kütüphaneyi yükler

public class Shot : MonoBehaviour // MonoBehaviour sýnýfýndan türetilen bir Shot sýnýfý tanýmlar
{
    public float speed = 5f; // Merminin hýzýný temsil eden bir kayan nokta deðiþkeni
    private GameObject target; // Hedef objeyi saklamak için bir GameObject referansý

    // Merminin hedefine atanmasýný saðlayan fonksiyon
    public void Initialize(GameObject target)
    {
        this.target = target; // Hedef GameObject atanýr
    }

    void Update() // Her karede çaðrýlan Unity fonksiyonu
    {
        if (target != null) // Eðer bir hedef varsa
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
        else // Eðer hedef yoksa
        {
            // Mermiyi sahneden siler
            Destroy(gameObject);
        }
    }
}