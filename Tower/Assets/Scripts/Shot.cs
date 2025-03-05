using Unity.VisualScripting;
using UnityEngine; // Unity motorunun ana sýnýflarýný kullanabilmek için gerekli kütüphaneyi yükler

public class Shot : MonoBehaviour // MonoBehaviour sýnýfýndan türetilen bir Shot sýnýfý tanýmlar
{
    public float speed = 5f; // Merminin hýzýný temsil eden bir kayan nokta deðiþkeni
    private GameObject target; // Hedef objeyi saklamak için bir GameObject referansý
    private GameObject hasar11;
    private GameObject hasar12;
    private GameObject hasar13;

    // Merminin hedefine atanmasýný saðlayan fonksiyon
    public void Initialize(GameObject target)
    {
        this.target = target; // Hedef GameObject atanýr
        this.hasar11 = target;
        this.hasar12 = target;
        this.hasar13 = target;
    }
    
    void Start()
    {
        //hasar11=GameObject.Find("GameManager").GetComponent<Enemy>();
        //hasar12=GameObject.Find("GameManager").GetComponent<Enemy>();
        //hasar13=GameObject.Find("GameManager").GetComponent<Enemy>();
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
                string prefabName = gameObject.name;
                if (prefabName == "Arrow 1(Clone)")
                {
                    hasar11.GetComponent<Enemy>().Hasar();
                    target.GetComponent<Enemy>().TakeDamage();
                }
                else if (prefabName == "Arrow 2(Clone)")
                {
                    hasar12.GetComponent<Enemy>().Hasar2();
                    target.GetComponent<Enemy>().TakeDamage();
                }
                else if (prefabName == "Arrow 3(Clone)")
                {
                    hasar13.GetComponent<Enemy>().Hasar3();
                    target.GetComponent<Enemy>().TakeDamage();
                }
                else if (prefabName == "Boulder 1(Clone)")
                {
                    hasar11.GetComponent<Enemy>().Hasar();
                    target.GetComponent<Enemy>().TakeDamage();
                }
                else if (prefabName == "Boulder 2(Clone)")
                {
                    hasar12.GetComponent<Enemy>().Hasar2();
                    target.GetComponent<Enemy>().TakeDamage();
                }
                else if (prefabName == "Boulder 3(Clone)")
                {
                    hasar13.GetComponent<Enemy>().Hasar3();
                    target.GetComponent<Enemy>().TakeDamage();
                }
                else if (prefabName == "Bullet 1(Clone)")
                {
                    hasar11.GetComponent<Enemy>().Hasar();
                    target.GetComponent<Enemy>().TakeDamage();
                }
                else if (prefabName == "Bullet 2(Clone)")
                {
                    hasar12.GetComponent<Enemy>().Hasar2();
                    target.GetComponent<Enemy>().TakeDamage();
                }
                else if (prefabName == "Bullet 3(Clone)")
                {
                    hasar13.GetComponent<Enemy>().Hasar3();
                    target.GetComponent<Enemy>().TakeDamage();
                }
                else if (prefabName == "Cannonball 1(Clone)")
                {
                    hasar11.GetComponent<Enemy>().Hasar();
                    target.GetComponent<Enemy>().TakeDamage();
                }
                else if (prefabName == "Cannonball 2(Clone)")
                {
                    hasar12.GetComponent<Enemy>().Hasar2();
                    target.GetComponent<Enemy>().TakeDamage();
                }
                else if (prefabName == "Cannonball 3(Clone)")
                {
                    hasar13.GetComponent<Enemy>().Hasar3();
                    target.GetComponent<Enemy>().TakeDamage();
                }
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