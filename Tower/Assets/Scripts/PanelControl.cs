using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PanelControl : MonoBehaviour
{
    public GameObject settingsPanel;
    public GameObject[] panels;
    public Button[] panelButtons; // Tüm butonlar
    private Vector3 defaultScale = new Vector3(1f, 1f, 1f);
    private Vector3 highlightedScale = new Vector3(1.4f, 1.4f, 4f); // Buton büyüklüðü

    public int coinSayisi=0;
    public TextMeshProUGUI coinText;
    public int diamondSayisi=0;
    public TextMeshProUGUI diamondText;
    public int cardSayisi = 0;
    public TextMeshProUGUI cardText;
    public Button unlockButton; // Butonlarý açan ana buton
    public Button[] battlePassButtonsL; // Sol 30 butonu içeren dizi
    public Button[] battlePassButtonsR; // sað 30 butonu içeren dizi
    private List<Button> lockedLeft = new List<Button>(); // Kilitli sol butonlar listesi
    private List<Button> lockedRight = new List<Button>(); // Kilitli sað butonlar listesi
    private int unlockCounter = 0; // Týklama sayacý
    private Dictionary<Button, Color> originalColors = new Dictionary<Button, Color>(); // Butonlarýn orijinal renkleri

    public Slider slider;   // Baðlanacak Slider
    public GameObject battleButton;
    private bool battle = false;
    private float timer = 0f;
    private int productionCount = 0;
    public TextMeshProUGUI productionText;
    public TextMeshProUGUI productionTime;
    public TextMeshProUGUI productionLevel;
    public TextMeshProUGUI productionCost;
    public TextMeshProUGUI castleHealth;
    public TextMeshProUGUI castleLevel;
    public TextMeshProUGUI castleCost;
    public float proTime=0.2f;
    public int proLevel=1;
    public float casHealth=5;
    public int casLevel=1;
    public int x = 1;
    public int y = 1;

    private void Start()
    {
        productionCost.text = x.ToString();
        castleCost.text = y.ToString();
        panels[2].SetActive(true);
        slider.gameObject.SetActive(false);
        panelButtons[2].transform.localScale = highlightedScale;

        foreach (Button btn in battlePassButtonsL)
            LockButton(btn, lockedLeft);
        foreach (Button btn in battlePassButtonsR)
            LockButton(btn, lockedRight);
        unlockButton.onClick.AddListener(UnlockButtons);

    }
    private void Update()
    {
        if (battle == true)
        {
            Production();
        }
    }

    //menüleri açýp kapatma
    public void ShowPanel(int index)
    {
        for (int i = 0; i < panels.Length; i++)
        {
            bool isActive = (i == index);
            panels[i].SetActive(i == index); // Sadece seçilen panel aktif, diðerleri kapalý
            if (isActive) panelButtons[i].transform.localScale = highlightedScale; // Butonu büyüt
            else panelButtons[i].transform.localScale = defaultScale; // Eski haline getir
        }
    }   
    public void ShowSettings()  {settingsPanel.SetActive(true); Time.timeScale = 0f; }                                                       
    public void CloseSettings()  {settingsPanel.SetActive(false); Time.timeScale = 1f; }

//BattlePass butonlarý
    public void ButtonClicked(int buttonIndex)
    {
        int diamondsGained = 0;
        int cardsGained = 0;
        switch (buttonIndex)
        {
            case 0:
                diamondsGained = 1;  // b0 ve b2 butonlarý 1 elmas kazandýrýr
                break;
            case 1: 
                diamondsGained = 2;  // b1, b3, b13, b16, b23, b27 butonlarý 2 elmas kazandýrýr
                break;
            case 2:
                diamondsGained = 3;  // b4, b7, b11, b15, b20, b26 butonlarý 3 elmas kazandýrýr
                break;
            case 3:
                diamondsGained = 4;  // b6, b10, b17, b22, b25 butonlarý 4 elmas kazandýrýr
                break;
            case 4:
                diamondsGained = 5;  // b8, b12, b18, b21, b28, b31, b33 butonlarý 5 elmas kazandýrýr
                break;
            case 5:
                diamondsGained = 10; // b32, b43, b48, b55 butonlarý 10 elmas kazandýrýr
                break;
            case 6:
                diamondsGained = 15; // b34, b36, b40, b47, b50, b57 butonlarý 15 elmas kazandýrýr
                break;
            case 7:
                diamondsGained = 20; // b35, b38, b41, b44, b46, b51, b53, b56 butonlarý 20 elmas kazandýrýr
                break;
            case 8:
                diamondsGained = 25; // b39, b45, b52, b28 butonlarý 25 elmas kazandýrýr
                break;
            case 9:
                cardsGained = 1;    // b5 ve b14 butonlarý 1 kart kazandýrýr
                break;
            case 10:
                cardsGained = 2;    // b9 ve b19 butonlarý 2 kart kazandýrýr
                break;
            case 11:
                cardsGained = 3;    // b24 ve b29 butonlarý 3 kart kazandýrýr
                break;
            case 12:
                cardsGained = 5;    // b30, b37, b42, b49, b54, b59 butonlarý 5 kart kazandýrýr
                break;
            default:
                diamondsGained = 0; // Geçersiz bir buton numarasý
                break;
        }
        diamondSayisi += diamondsGained;
        diamondText.text=diamondSayisi.ToString();
        cardSayisi += cardsGained;
        cardText.text=cardSayisi.ToString(); 
    }   
    void LockButton(Button btn, List<Button> lockedList)
    {
        btn.interactable = false; // Týklamayý engelle
        originalColors[btn] = btn.colors.normalColor;   // Orijinal rengini sakla
        ColorBlock cb = btn.colors;
        cb.disabledColor = new Color(0.75f, 0.75f, 0.77f); // BFBFC5 rengi
        btn.colors = cb;
        if (btn.image != null)
        {
            btn.image.color = new Color(0.75f, 0.75f, 0.77f); // BFBFC5 rengi
        }
        lockedList.Add(btn); // Butonu kilitli listeye ekle
    }
    void UnlockButtons()
    {
        unlockCounter++; // Her týklamada sayacý artýr

        if (unlockCounter % 3 == 0) // Her 3 týklamada bir çift buton aç
        {
            if (lockedLeft.Count > 0)
                UnlockButton(lockedLeft);

            if (lockedRight.Count > 0)
                UnlockButton(lockedRight);
        }
    }
    void UnlockButton(List<Button> lockedList)
    {
        Button buttonToUnlock = lockedList[0]; // Ýlk kilitli butonu seç
        lockedList.RemoveAt(0); // Listeden çýkar (açýldýðý için artýk kilitli deðil)

        buttonToUnlock.interactable = true; // Týklanabilir yap

        // Orijinal rengine geri döndür
        ColorBlock cb = buttonToUnlock.colors;
        cb.disabledColor = originalColors[buttonToUnlock]; // Normal rengine döndür
        buttonToUnlock.colors = cb;
        if (buttonToUnlock.image != null)
        {
            buttonToUnlock.image.color = new Color(0.333f, 0.627f, 0.992f); // #55A0FD

        }
    }

//Satýn alma Menüsü
    public void StoreCoinEarn(int diamond)
    {
        switch (diamond)
        {
            case 0:
                if (diamondSayisi >= 5)
                {
                    diamondSayisi -= 5;
                    diamondText.text=diamondSayisi.ToString();
                    coinSayisi += 2500;
                    coinText.text=coinSayisi.ToString();
                }
            break;
            case 1:
                if (diamondSayisi >= 7)
                {
                    diamondSayisi -= 7;
                    diamondText.text = diamondSayisi.ToString();
                    coinSayisi += 7500;
                    coinText.text = coinSayisi.ToString();
                }
            break;
            case 2:
                if (diamondSayisi >= 30)
                {
                    diamondSayisi -= 30;
                    diamondText.text = diamondSayisi.ToString();
                    coinSayisi += 35500;
                    coinText.text = coinSayisi.ToString();
                }
            break;
            case 3:
                if (diamondSayisi >= 100)
                {
                    diamondSayisi -= 100;
                    diamondText.text = diamondSayisi.ToString();
                    coinSayisi += 150000;
                    coinText.text = coinSayisi.ToString();
                }
            break;
        }
    }

//Savaþ alaný menüsü
    public void Production()
    {
        timer += Time.deltaTime*proTime; // Zaman ilerliyor
        slider.value = timer; // Slider’ý güncelle

        if (timer >= 1)
        {
            productionCount++; // +1 üretim
            productionText.text = productionCount.ToString();
            timer = 0f; // Sýfýrla           
        }
    }
    public void StartBattle()
    {
        battle = true;
        battleButton.SetActive(false);
        slider.gameObject.SetActive(true);
        
    }
    //Upgrade Menüsü
    public void UpgradeMenu(int up)
    { 
        if (up == 0 && x<=coinSayisi)
        {
            proTime +=0.02f;
            proLevel += 1;
            productionTime.text = proTime.ToString() + "/s";
            productionLevel.text = proLevel.ToString();

            coinSayisi -= x;
            x += 1;  
            productionCost.text=x.ToString();
            coinText.text = coinSayisi.ToString();

        }
        else if (up == 1 && y <= coinSayisi) 
        {
            casHealth += 1;
            casLevel += 1;
            castleHealth.text = casHealth.ToString();
            castleLevel.text = casLevel.ToString();

            coinSayisi -= y;
            y += 1;
            castleCost.text=y.ToString();
            coinText.text=coinSayisi.ToString();

        }
    }

}
