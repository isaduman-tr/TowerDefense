using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public Enemy enemy;                  // Enemy scriptine referans
    public Image healthBarImage;         // UI Image referansý (saðlýk çubuðu)
    public Canvas healthBarCanvas;       // Sadece saðlýk barýnýn bulunduðu Canvas
    public float smoothSpeed = 5f;       // Akýcý geçiþ hýzý
    private float targetHealth;
    private float maxHealth;
    private Transform mainCamera;

    void Start()
    {
        if (enemy != null && healthBarImage != null)
        {
            maxHealth = enemy.StartingHealth;
            healthBarImage.fillAmount = 1f;
            targetHealth = enemy.CurrentHealth;
        }
        mainCamera = Camera.main.transform;
    }

    void LateUpdate()
    {
        if (enemy != null && healthBarImage != null)
        {
            targetHealth = enemy.CurrentHealth / maxHealth;
            healthBarImage.fillAmount = Mathf.Lerp(healthBarImage.fillAmount, targetHealth, smoothSpeed * Time.deltaTime);
            UpdateHealthBarColor();
            FaceCamera();
        }
    }

    void UpdateHealthBarColor()
    {
        if (targetHealth < 0.3f)
            healthBarImage.color = Color.red;
        else if (targetHealth < 0.6f)
            healthBarImage.color = Color.yellow;
        else
            healthBarImage.color = Color.green;
    }

    void FaceCamera()
    {
        if (healthBarCanvas != null && mainCamera != null)
        {
            // Sadece Canvas'ý kameraya döndür
            healthBarCanvas.transform.rotation = Quaternion.LookRotation(healthBarCanvas.transform.position - mainCamera.position);
        }
    }
}
