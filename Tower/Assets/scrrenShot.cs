using System.Collections;
using System.IO;
using UnityEngine;

public class TowerScreenshot : MonoBehaviour
{
    public Camera renderCamera; // PNG almak için kullanılacak kamera
    public string fileName = "TowerImage"; // PNG dosya adı
    public int resolution = 4096; // Çözünürlüğü artırdım

    void Start()
    {
        StartCoroutine(CaptureScreenshot());
    }

    IEnumerator CaptureScreenshot()
    {
        yield return new WaitForEndOfFrame();

        // Render Texture oluştur (Yüksek çözünürlük ve Anti-Aliasing eklendi)
        RenderTexture rt = new RenderTexture(resolution, resolution, 32);
        rt.antiAliasing = 8; // Kenar yumuşatma açıldı

        renderCamera.targetTexture = rt;
        Texture2D screenShot = new Texture2D(resolution, resolution, TextureFormat.RGBA32, false);
        renderCamera.Render();
        RenderTexture.active = rt;
        screenShot.ReadPixels(new Rect(0, 0, resolution, resolution), 0, 0);
        screenShot.Apply();

        // PNG olarak kaydet (Tarih-saat ekleyerek çakışmayı önlüyor)
        string path = Application.dataPath + "/" + fileName + "_" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".png";
        File.WriteAllBytes(path, screenShot.EncodeToPNG());

        Debug.Log("Kule PNG olarak kaydedildi: " + path);

        // Bellek temizleme
        renderCamera.targetTexture = null;
        RenderTexture.active = null;
        Destroy(rt);
        Destroy(screenShot);
    }
}
