using UnityEngine;
using System.Collections;

public class EnemyDamageFlash : MonoBehaviour
{
    private MeshRenderer meshRenderer; // Enemy'nin MeshRenderer bile�eni
    private Color originalColor; // Orijinal rengini saklamak i�in
    private float flashDuration = 0.25f; // 0.25 saniye boyunca beyaz olacak

    private void Start()
    {
        // MeshRenderer bile�enini al ve ba�lang�� rengini kaydet
        meshRenderer = GetComponent<MeshRenderer>();
        originalColor = meshRenderer.material.color;
    }

    private void OnTriggerEnter(Collider other)
    {
        // E�er �arpan nesne "shot" tag'ine sahipse renk de�i�tir
        if (other.CompareTag("shot"))
        {
            StartCoroutine(FlashWhite());
        }
    }

    private IEnumerator FlashWhite()
    {
        meshRenderer.material.color = Color.white; // Rengi beyaza d�nd�r
        yield return new WaitForSeconds(flashDuration); // 0.25 saniye bekle
        meshRenderer.material.color = originalColor; // Eski renge d�n
    }
}
