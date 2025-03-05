using UnityEngine;
using UnityEngine.UI;

public class KaleHealthBar : MonoBehaviour
{
    public KaleHealth kaleHealth;       // KaleHealth scriptine referans
    public Image healthBarImage;        // UI Image referansý (saðlýk çubuðu)
    public float smoothSpeed = 5f;      // Akýcý geçiþ hýzý

    private float targetHealth;
    private float maxHealth;

    void Start()
    {
        if (kaleHealth != null && healthBarImage != null)
        {
            maxHealth = kaleHealth.health ; // Baþlangýçta maksimum saðlýk deðerini al
            healthBarImage.fillAmount = 1f; // Saðlýk çubuðunu tam dolu olarak baþlat
            targetHealth = kaleHealth.health ; // Hedef saðlýk deðerini güncelle
        }
    }

    void Update()
    {
        if (kaleHealth != null && healthBarImage != null)
        {
            // Hedef saðlýk deðerini güncelle (0 ile 1 arasýnda bir oran)
            targetHealth = (float)kaleHealth.health / maxHealth;

            // Yumuþak bir geçiþ için Lerp kullanarak fillAmount'u güncelle
            healthBarImage.fillAmount = Mathf.Lerp(healthBarImage.fillAmount, targetHealth, smoothSpeed * Time.deltaTime);

            // Saðlýk durumuna göre renk deðiþtir
            UpdateHealthBarColor();
        }
    }

    void UpdateHealthBarColor()
    {
        // Saðlýk düþükse kýrmýzý, orta düzeydeyse sarý, yüksekse yeþil yap
        if (targetHealth < 0.3f)
        {
            healthBarImage.color = Color.red; // Saðlýk düþükse kýrmýzý
        }
        else if (targetHealth < 0.6f)
        {
            healthBarImage.color = Color.yellow; // Saðlýk orta düzeydeyse sarý
        }
        else
        {
            healthBarImage.color = Color.green; // Saðlýk yüksekse yeþil
        }
    }
}