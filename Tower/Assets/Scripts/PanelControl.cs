using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PanelControl : MonoBehaviour
{
    public GameObject settingsPanel;
    public GameObject[] panels;
    public Button[] panelButtons; // T�m butonlar
    private Vector3 defaultScale = new Vector3(1f, 1f, 1f);
    private Vector3 highlightedScale = new Vector3(1.4f, 1.4f, 4f); // Buton b�y�kl���

    public int coinSayisi=0;
    public TextMeshProUGUI coinText;
    public int diamondSayisi=0;
    public TextMeshProUGUI diamondText;
    public int cardSayisi = 0;
    public TextMeshProUGUI cardText;
    public Button unlockButton; // Butonlar� a�an ana buton
    public Button[] battlePassButtonsL; // Sol 30 butonu i�eren dizi
    public Button[] battlePassButtonsR; // sa� 30 butonu i�eren dizi
    private List<Button> lockedLeft = new List<Button>(); // Kilitli sol butonlar listesi
    private List<Button> lockedRight = new List<Button>(); // Kilitli sa� butonlar listesi
    private int unlockCounter = 0; // T�klama sayac�
    private Dictionary<Button, Color> originalColors = new Dictionary<Button, Color>(); // Butonlar�n orijinal renkleri

    public Slider slider;   // Ba�lanacak Slider
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

    //men�leri a��p kapatma
    public void ShowPanel(int index)
    {
        for (int i = 0; i < panels.Length; i++)
        {
            bool isActive = (i == index);
            panels[i].SetActive(i == index); // Sadece se�ilen panel aktif, di�erleri kapal�
            if (isActive) panelButtons[i].transform.localScale = highlightedScale; // Butonu b�y�t
            else panelButtons[i].transform.localScale = defaultScale; // Eski haline getir
        }
    }   
    public void ShowSettings()  {settingsPanel.SetActive(true); Time.timeScale = 0f; }                                                       
    public void CloseSettings()  {settingsPanel.SetActive(false); Time.timeScale = 1f; }

//BattlePass butonlar�
    public void ButtonClicked(int buttonIndex)
    {
        int diamondsGained = 0;
        int cardsGained = 0;
        switch (buttonIndex)
        {
            case 0:
                diamondsGained = 1;  // b0 ve b2 butonlar� 1 elmas kazand�r�r
                break;
            case 1: 
                diamondsGained = 2;  // b1, b3, b13, b16, b23, b27 butonlar� 2 elmas kazand�r�r
                break;
            case 2:
                diamondsGained = 3;  // b4, b7, b11, b15, b20, b26 butonlar� 3 elmas kazand�r�r
                break;
            case 3:
                diamondsGained = 4;  // b6, b10, b17, b22, b25 butonlar� 4 elmas kazand�r�r
                break;
            case 4:
                diamondsGained = 5;  // b8, b12, b18, b21, b28, b31, b33 butonlar� 5 elmas kazand�r�r
                break;
            case 5:
                diamondsGained = 10; // b32, b43, b48, b55 butonlar� 10 elmas kazand�r�r
                break;
            case 6:
                diamondsGained = 15; // b34, b36, b40, b47, b50, b57 butonlar� 15 elmas kazand�r�r
                break;
            case 7:
                diamondsGained = 20; // b35, b38, b41, b44, b46, b51, b53, b56 butonlar� 20 elmas kazand�r�r
                break;
            case 8:
                diamondsGained = 25; // b39, b45, b52, b28 butonlar� 25 elmas kazand�r�r
                break;
            case 9:
                cardsGained = 1;    // b5 ve b14 butonlar� 1 kart kazand�r�r
                break;
            case 10:
                cardsGained = 2;    // b9 ve b19 butonlar� 2 kart kazand�r�r
                break;
            case 11:
                cardsGained = 3;    // b24 ve b29 butonlar� 3 kart kazand�r�r
                break;
            case 12:
                cardsGained = 5;    // b30, b37, b42, b49, b54, b59 butonlar� 5 kart kazand�r�r
                break;
            default:
                diamondsGained = 0; // Ge�ersiz bir buton numaras�
                break;
        }
        diamondSayisi += diamondsGained;
        diamondText.text=diamondSayisi.ToString();
        cardSayisi += cardsGained;
        cardText.text=cardSayisi.ToString(); 
    }   
    void LockButton(Button btn, List<Button> lockedList)
    {
        btn.interactable = false; // T�klamay� engelle
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
        unlockCounter++; // Her t�klamada sayac� art�r

        if (unlockCounter % 3 == 0) // Her 3 t�klamada bir �ift buton a�
        {
            if (lockedLeft.Count > 0)
                UnlockButton(lockedLeft);

            if (lockedRight.Count > 0)
                UnlockButton(lockedRight);
        }
    }
    void UnlockButton(List<Button> lockedList)
    {
        Button buttonToUnlock = lockedList[0]; // �lk kilitli butonu se�
        lockedList.RemoveAt(0); // Listeden ��kar (a��ld��� i�in art�k kilitli de�il)

        buttonToUnlock.interactable = true; // T�klanabilir yap

        // Orijinal rengine geri d�nd�r
        ColorBlock cb = buttonToUnlock.colors;
        cb.disabledColor = originalColors[buttonToUnlock]; // Normal rengine d�nd�r
        buttonToUnlock.colors = cb;
        if (buttonToUnlock.image != null)
        {
            buttonToUnlock.image.color = new Color(0.333f, 0.627f, 0.992f); // #55A0FD

        }
    }

//Sat�n alma Men�s�
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

//Sava� alan� men�s�
    public void Production()
    {
        timer += Time.deltaTime*proTime; // Zaman ilerliyor
        slider.value = timer; // Slider�� g�ncelle

        if (timer >= 1)
        {
            productionCount++; // +1 �retim
            productionText.text = productionCount.ToString();
            timer = 0f; // S�f�rla           
        }
    }
    public void StartBattle()
    {
        battle = true;
        battleButton.SetActive(false);
        slider.gameObject.SetActive(true);
        
    }
    //Upgrade Men�s�
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
