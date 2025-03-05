using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TowerArea : MonoBehaviour
{
    public static int[,] kuleler = new int[12, 2] {{0, 1}, {1, 5}, {2, 3}, {3, 9}, {4, 1}, {5, 5}, {6, 3}, {7, 9}, {8, 1}, {9, 5}, {10, 3}, {11, 9}}; 

    public GameObject[] towers;
    public GameObject towerRange;
    public TextMeshProUGUI attackText;
    public TextMeshProUGUI attackText2;
    public TextMeshProUGUI attackText3;
    public float attack = 0.2f;
    public static float attack2;
    public static float secilenKuleHasarDeðeri=0;
    public static float secilenKuleHasarDeðeri2=0;
    public static float secilenKuleHasarDeðeri3=0;
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
    void Start()
    {
        attack2 = TowerAttack.towerDamage;  
    }
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
                        towerRange.gameObject.SetActive(!towerRange.gameObject.activeSelf);
                }
            }
        }
    }
    public void KuleSecimi(int kuleNumarasi)
    {
        if (PanelControl.productionCount >= 3)
        {
            TowerPanel.SetActive(false);
            towerchoose = false;
        }
          int kuleHasarDeðeri = 0; 
        switch (kuleNumarasi)
        {
            case 0: 
                if (PanelControl.productionCount>= 3)
                {
                    attackText.text = "x1.0";cooldownText.text = "1.0sn"; towers[PanelControl.evolveCount - 2].SetActive(true);
                    kuleHasarDeðeri = kuleler[kuleNumarasi, 1];
                    secilenKuleHasarDeðeri = kuleHasarDeðeri * PanelControl.totalDmg;
                    attackText.text = "x" + secilenKuleHasarDeðeri.ToString("F1");
                    PanelControl.productionCount -= 3;
                    
                } break;
            case 1:
                if (PanelControl.productionCount >= 3)
                {
                    attackText.text = "x5.0"; cooldownText.text = "5.0sn"; towers[PanelControl.evolveCount + 1].SetActive(true);
                    kuleHasarDeðeri = kuleler[kuleNumarasi, 1];
                    secilenKuleHasarDeðeri = kuleHasarDeðeri * PanelControl.totalDmg;
                    attackText.text = "x" + secilenKuleHasarDeðeri.ToString("F1");
                    PanelControl.productionCount -= 3;
                    
                } break;
            case 2:
                if (PanelControl.productionCount >= 3)
                {
                    attackText.text = "x3.0"; cooldownText.text = "1.0sn"; towers[PanelControl.evolveCount + 4].SetActive(true);
                    kuleHasarDeðeri = kuleler[kuleNumarasi, 1];
                    secilenKuleHasarDeðeri = kuleHasarDeðeri * PanelControl.totalDmg;
                    attackText.text = "x" + secilenKuleHasarDeðeri.ToString("F1");
                    PanelControl.productionCount -= 3;
                    
                }
                break;
            case 3:
                if (PanelControl.productionCount >= 3)
                {
                    attackText.text = "x9.0"; cooldownText.text = "5.0sn"; towers[PanelControl.evolveCount + 7].SetActive(true);
                    kuleHasarDeðeri = kuleler[kuleNumarasi, 1];
                    secilenKuleHasarDeðeri = kuleHasarDeðeri * PanelControl.totalDmg;
                    attackText.text = "x" + secilenKuleHasarDeðeri.ToString("F1");
                    PanelControl.productionCount -= 3;
                    
                }
                break;
            case 4:
                if (PanelControl.productionCount >= 3)
                {
                    attackText2.text = "x1.0"; cooldownText.text = "1.0sn"; towers[PanelControl.evolveCount - 2].SetActive(true);
                    kuleHasarDeðeri = kuleler[kuleNumarasi, 1];
                    secilenKuleHasarDeðeri2 = kuleHasarDeðeri * PanelControl.totalDmg;
                    attackText2.text = "x" + secilenKuleHasarDeðeri2.ToString("F1");
                    PanelControl.productionCount -= 3;
                    
                }
                break;
            case 5:
                if (PanelControl.productionCount >= 3)
                {
                    attackText2.text = "x5.0"; cooldownText.text = "5.0sn"; towers[PanelControl.evolveCount + 1].SetActive(true);
                    kuleHasarDeðeri = kuleler[kuleNumarasi, 1];
                    secilenKuleHasarDeðeri2 = kuleHasarDeðeri * PanelControl.totalDmg;
                    attackText2.text = "x" + secilenKuleHasarDeðeri2.ToString("F1");
                    PanelControl.productionCount -= 3;
                    
                }
                break;
            case 6:
                if (PanelControl.productionCount >= 3)
                {
                    attackText2.text = "x3.0"; cooldownText.text = "1.0sn"; towers[PanelControl.evolveCount + 4].SetActive(true);
                    kuleHasarDeðeri = kuleler[kuleNumarasi, 1];
                    secilenKuleHasarDeðeri2 = kuleHasarDeðeri * PanelControl.totalDmg;
                    attackText2.text = "x" + secilenKuleHasarDeðeri2.ToString("F1");
                    PanelControl.productionCount -= 3;
                    
                }
                break;
            case 7:
                if (PanelControl.productionCount >= 3)
                {
                    attackText2.text = "x9.0"; cooldownText.text = "5.0sn"; towers[PanelControl.evolveCount + 7].SetActive(true);
                    kuleHasarDeðeri = kuleler[kuleNumarasi, 1];
                    secilenKuleHasarDeðeri2 = kuleHasarDeðeri * PanelControl.totalDmg;
                    attackText2.text = "x" + secilenKuleHasarDeðeri2.ToString("F1");
                    PanelControl.productionCount -= 3;
                    
                }
                break;
            case 8:
                if (PanelControl.productionCount >= 3)
                {
                    attackText3.text = "x1.0"; cooldownText.text = "1.0sn"; towers[PanelControl.evolveCount - 2].SetActive(true);
                    kuleHasarDeðeri = kuleler[kuleNumarasi, 1];
                    secilenKuleHasarDeðeri3 = kuleHasarDeðeri * PanelControl.totalDmg;
                    attackText3.text = "x" + secilenKuleHasarDeðeri3.ToString("F1");
                    PanelControl.productionCount -= 3;
                    
                }
                break;
            case 9:
                if (PanelControl.productionCount >= 3)
                {
                    attackText3.text = "x5.0"; cooldownText.text = "5.0sn"; towers[PanelControl.evolveCount + 1].SetActive(true);
                    kuleHasarDeðeri = kuleler[kuleNumarasi, 1];
                    secilenKuleHasarDeðeri3 = kuleHasarDeðeri * PanelControl.totalDmg;
                    attackText3.text = "x" + secilenKuleHasarDeðeri3.ToString("F1");
                    PanelControl.productionCount -= 3;
                    
                }
                break;
            case 10:
                if (PanelControl.productionCount >= 3)
                {
                    attackText3.text = "x3.0"; cooldownText.text = "1.0sn"; towers[PanelControl.evolveCount + 4].SetActive(true);
                    kuleHasarDeðeri = kuleler[kuleNumarasi, 1];
                    secilenKuleHasarDeðeri3 = kuleHasarDeðeri * PanelControl.totalDmg;
                    attackText3.text = "x" + secilenKuleHasarDeðeri3.ToString("F1");
                    PanelControl.productionCount -= 3;
                    
                }
                break;
            case 11:
                if (PanelControl.productionCount >= 3)
                {
                    attackText3.text = "x9.0"; cooldownText.text = "5.0sn"; towers[PanelControl.evolveCount + 7].SetActive(true);
                    kuleHasarDeðeri = kuleler[kuleNumarasi, 1];
                    secilenKuleHasarDeðeri3 = kuleHasarDeðeri * PanelControl.totalDmg;
                    attackText3.text = "x" + secilenKuleHasarDeðeri3.ToString("F1");
                    PanelControl.productionCount -= 3;
                    
                }
                break;
            default:break;
        }
    }

    public void UpgradeButton(int towerarea)
    {
        if (towerarea == 0)
        {
            if (scrollbar.size <= 0.98f)    //upgrade panelindeki bilgiler güncelleniyor
            {
                if (upgradeCost <= PanelControl.productionCount)
                {
                    PanelControl.productionCount -= upgradeCost;
                    upgradeCost++;
                    upgradeCostText.text = upgradeCost.ToString();
                    secilenKuleHasarDeðeri += 0.2f;
                    secilenKuleHasarDeðeri = Mathf.Round(secilenKuleHasarDeðeri * 10f) / 10f;
                    attackText.text = "x" + secilenKuleHasarDeðeri;
                    towerLevel++;
                    towerLevelText.text = "Level " + towerLevel.ToString();
                    scrollbar.size += 0.05f; // Yeni size deðeri (0.0 ile 1.0 arasýnda)
                }
            }
            if (scrollbar.size > 0.99f)     //scroll bar dolduktan sonra upgrade butonu iþlevi durduruyor ve paneldeki yýldýz mavi oluyor
            {
                GameObject clickedobject = EventSystem.current.currentSelectedGameObject;
                Transform imageTransform = clickedobject.transform.Find("Star");
                imageTransform.GetComponent<Image>().color = new Color(0.281f, 0.541f, 0.898f); // Rengi deðiþtir
                upgradeCostText.text = "Max";
            }
        }
        else if (towerarea == 1)
        {
            if (scrollbar.size <= 0.98f)    //upgrade panelindeki bilgiler güncelleniyor
            {
                if (upgradeCost <= PanelControl.productionCount)
                {
                    PanelControl.productionCount -= upgradeCost;
                    upgradeCost++;
                    upgradeCostText.text = upgradeCost.ToString();
                    secilenKuleHasarDeðeri2 += 0.2f;
                    secilenKuleHasarDeðeri2 = Mathf.Round(secilenKuleHasarDeðeri2 * 10f) / 10f;
                    attackText2.text = "x" + secilenKuleHasarDeðeri2;
                    towerLevel++;
                    towerLevelText.text = "Level " + towerLevel.ToString();
                    scrollbar.size += 0.05f; // Yeni size deðeri (0.0 ile 1.0 arasýnda)
                }              
            }
            if (scrollbar.size > 0.99f)     //scroll bar dolduktan sonra upgrade butonu iþlevi durduruyor ve paneldeki yýldýz mavi oluyor
            {
                GameObject clickedobject = EventSystem.current.currentSelectedGameObject;
                Transform imageTransform = clickedobject.transform.Find("Star");
                imageTransform.GetComponent<Image>().color = new Color(0.281f, 0.541f, 0.898f); // Rengi deðiþtir
                upgradeCostText.text = "Max";
            }
        }
        else if (towerarea == 2)
        {
            if (scrollbar.size <= 0.98f)    //upgrade panelindeki bilgiler güncelleniyor
            {
                if (upgradeCost <= PanelControl.productionCount)
                {
                    PanelControl.productionCount -= upgradeCost;
                    upgradeCost++;
                    upgradeCostText.text = upgradeCost.ToString();
                    secilenKuleHasarDeðeri3 += 0.2f;
                    secilenKuleHasarDeðeri3 = Mathf.Round(secilenKuleHasarDeðeri3 * 10f) / 10f;
                    attackText3.text = "x" + secilenKuleHasarDeðeri3;
                    towerLevel++;
                    towerLevelText.text = "Level " + towerLevel.ToString();
                    scrollbar.size += 0.05f; // Yeni size deðeri (0.0 ile 1.0 arasýnda)
                }
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
}
