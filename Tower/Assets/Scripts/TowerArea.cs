using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TowerArea : MonoBehaviour
{
    //yorum satýrý
    public GameObject[] towers;
    public TextMeshProUGUI attackText;
    public float attack = 1;
    public TextMeshProUGUI cooldownText;
    public float cooldown = 1;
    public TextMeshProUGUI upgradeCostText;
    public int upgradeCost = 3;
    public TextMeshProUGUI towerLevelText;
    public int towerLevel = 1;

    public GameObject TowerPanel;
    public GameObject UpgradePanel;
    public Scrollbar scrollbar; // Scrollbar referansý
    private bool towerchoose = true;

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) // Kule kurmak için TowerArea objesine týklayýnca açýlan paneller
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.transform == transform)
            {
                if (towerchoose)    //Kule seçimi yaptðýmýz paneli açmak için kullandýðýmýz döngü
                {
                    Transform panel = transform.Find("TowerPanel"); // Kendi panelini bul
                    if (panel != null)
                        panel.gameObject.SetActive(!panel.gameObject.activeSelf); // Açýk/kapalý toggle
                }
                else    //Kule geliþtirme yaptðýmýz paneli açmak için kullandýðýmýz döngü
                {
                    Transform panel = transform.Find("UpgradePanel"); // Kendi panelini bul
                    if (panel != null)
                        panel.gameObject.SetActive(!panel.gameObject.activeSelf); // Açýk/kapalý toggle
                }
            }
        }
    }
    public void KuleSecimi(int kuleNumarasi)
    {
        TowerPanel.SetActive(false);
        towerchoose = false;
        switch (kuleNumarasi)
        {
            case 0: attackText.text = "x1.0"; cooldownText.text = "1.0sn"; towers[0].SetActive(true); break;
            case 1: attackText.text = "x5.0"; cooldownText.text = "5.0sn"; towers[1].SetActive(true); break;
            case 2: attackText.text = "x3.0"; cooldownText.text = "1.0sn"; towers[2].SetActive(true); break;
            case 3: attackText.text = "x9.0"; cooldownText.text = "5.0sn"; towers[3].SetActive(true); break;
            default:break;
        }
    }


    public void UpgradeButton()
    {
        if (scrollbar.size <= 0.98f)    //upgrade panelindeki bilgiler güncelleniyor
        {
            upgradeCost++;
            upgradeCostText.text = upgradeCost.ToString();
            attack++;
            attack = Mathf.Round(attack * 10f) / 10f;
            attackText.text = "x" + attack;
            towerLevel++;
            towerLevelText.text = "Level " + towerLevel.ToString();
            scrollbar.size += 0.05f; // Yeni size deðeri (0.0 ile 1.0 arasýnda)
        }
        if (scrollbar.size > 0.99f)     //scroll bar dolduktan sonra upgrade butonu iþlevi durduruyor ve paneldeki yýldýz mavi oluyor
        {
            GameObject clickedobject = EventSystem.current.currentSelectedGameObject;
            Transform imageTransform = clickedobject.transform.Find("Star");
            imageTransform.GetComponent<Image>().color = new Color(0.281f, 0.541f, 0.898f); // Rengi deðiþtir
            upgradeCostText.text = "Max";
        }
    }
}
