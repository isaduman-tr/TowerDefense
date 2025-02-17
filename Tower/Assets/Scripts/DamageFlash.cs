using UnityEngine;
using System.Collections;

public class EnemyDamageFlash : MonoBehaviour
{
    private MeshRenderer meshRenderer; // Enemy'nin MeshRenderer bileþeni
    private Color originalColor; // Orijinal rengini saklamak için
    private float flashDuration = 0.25f; // 0.25 saniye boyunca beyaz olacak

    private void Start()
    {
        // MeshRenderer bileþenini al ve baþlangýç rengini kaydet
        meshRenderer = GetComponent<MeshRenderer>();
        originalColor = meshRenderer.material.color;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Eðer çarpan nesne "shot" tag'ine sahipse renk deðiþtir
        if (other.CompareTag("shot"))
        {
            StartCoroutine(FlashWhite());
        }
    }

    private IEnumerator FlashWhite()
    {
        meshRenderer.material.color = Color.white; // Rengi beyaza döndür
        yield return new WaitForSeconds(flashDuration); // 0.25 saniye bekle
        meshRenderer.material.color = originalColor; // Eski renge dön
    }
}
