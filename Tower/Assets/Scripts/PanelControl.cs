using UnityEngine;
using UnityEngine.UI;

public class PanelControl : MonoBehaviour
{
    public GameObject settingsPanel;
    public GameObject[] panels;
    public Button[] buttons; // Tüm butonlar

    private Vector3 defaultScale = new Vector3(1f, 1f, 1f);
    private Vector3 highlightedScale = new Vector3(1.4f, 1.4f, 4f); // Buton büyüklüðü

    private void Start()
    {
        panels[2].SetActive(true);
        buttons[2].transform.localScale = highlightedScale;
    }
    public void ShowPanel(int index)
    {
        for (int i = 0; i < panels.Length; i++)
        {
            bool isActive = (i == index);
            panels[i].SetActive(i == index); // Sadece seçilen panel aktif, diðerleri kapalý
            if (isActive)   buttons[i].transform.localScale = highlightedScale; // Butonu büyüt
            else   buttons[i].transform.localScale = defaultScale; // Eski haline getir
        }
    }
    public void ShowSettings()  {settingsPanel.SetActive(true); Time.timeScale = 0f; }                                                       
    public void CloseSettings()  {settingsPanel.SetActive(false); Time.timeScale = 1f; }
}
