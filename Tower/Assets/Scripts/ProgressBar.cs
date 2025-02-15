using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProgressBar : MonoBehaviour
{
    public Scrollbar progressBar; // Unity UI Scrollbar
    public TMP_Text progressText;  // UI TextMeshPro nesnesi
    public float fillTime = 3f; // Ka� saniyede dolaca��n� belirleyen de�i�ken

    private int count = 0;
    private float timer = 0f;
    public int GetCount()
    {
        return count;
    }

    public void UsePoints(int amount)
    {
        if (count >= amount)
        {
            count -= amount;
            progressText.text = count.ToString();
        }
    }

    void Awake()
    {
        progressBar.size = 0f; progressBar.value = 0f;
        progressText.text = count.ToString();
        StartCoroutine(FillBar());
    }

    IEnumerator FillBar()
    {
        while (true)
        {
            timer = 0f;
            while (timer < fillTime)
            {
                timer += Time.deltaTime;
                progressBar.size = timer / fillTime; // Bar� doldur
                yield return null;
            }

            progressBar.size = 0f;
            progressBar.value = 0f; // Bar� s�f�rla ve handle'� en ba�a �ek
            count++;
            progressText.text = count.ToString(); // Say�y� art�r
        }
    }
}
