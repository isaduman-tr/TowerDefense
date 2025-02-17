using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PanelControl : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider MusicSlider, SFXSlider;
    public GameObject settingsPanel;
    public GameObject buttonPanel;
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
    private float proTime=0.2f;
    private int proLevel=1;
    private float casHealth=5;
    private int casLevel=1;
    private int x = 1;
    private int y = 1;
    private int tower1dmg = 1;
    public GameObject tower1Attack, tower1CoolDown, tower1Lv1, tower1Lv2, tower1Lv3;
    private int tower2=2000, tower2dmg=5;
    public GameObject tower2Button, tower2Attack, tower2CoolDown, tower2Lv1, tower2Lv2, tower2Lv3;
    private int tower3=5000, tower3dmg=3;
    public GameObject tower3Button, tower3Attack, tower3CoolDown, tower3Lv1, tower3Lv2, tower3Lv3;
    private int tower4=7000, tower4dmg=9;
    public GameObject tower4Button, tower4Attack, tower4CoolDown, tower4Lv1, tower4Lv2, tower4Lv3;
    private int evolveCount=2;


    public List<GameObject> deckPrefabs;  // 12 kartl�k prefab listesi (Inspector'dan ekle)
    public Transform contentTransform;    // Scroll View i�indeki Content objesi
    public float totalDmg = 1.0f;
    public TextMeshProUGUI totalDmgText;
    public float totalHealth = 1.0f;
    public TextMeshProUGUI totalHealthText;

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
        if (battle == true) {Production();}
        MusicSlider.onValueChanged.AddListener(SetMusicVolume);
        SFXSlider.onValueChanged.AddListener(SetSFXVolume);
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
        panels[2].SetActive(false);
        buttonPanel.SetActive(false);
    }

//Geli�tirme Men�s�
    public void UpgradeMenu(int up)
    {
        if (up == 0 && x <= coinSayisi)
        {
            proTime += 0.02f;
            proTime = Mathf.Round(proTime * 100f) / 100f;
            proLevel += 1;
            productionTime.text = proTime.ToString() + "/s";
            productionLevel.text ="Level"+ proLevel.ToString();

            coinSayisi -= x;
            x += 1;
            productionCost.text = x.ToString();
            coinText.text = coinSayisi.ToString();
        }
        else if (up == 1 && y <= coinSayisi)
        {
            casHealth += 1;
            casLevel += 1;
            castleHealth.text = casHealth.ToString();
            castleLevel.text ="Level"+ casLevel.ToString();

            coinSayisi -= y;
            y += 1;
            castleCost.text = y.ToString();
            coinText.text = coinSayisi.ToString();
        }
        else if (up == 2 && tower2 <= coinSayisi) 
        {
            tower2Button.SetActive(false); 
            tower2Attack.SetActive(true);
            tower2CoolDown.SetActive(true);
            tower2Attack.transform.parent.GetComponent<Image>().color = new Color32(0x55, 0xA0, 0xFD, 0xFF);

            coinSayisi -= tower2;
            coinText.text = coinSayisi.ToString();
        }
        else if (up == 3 && tower3 <= coinSayisi)
        {
            tower3Button.SetActive(false);
            tower3Attack.SetActive(true);
            tower3CoolDown.SetActive(true);
            tower3Attack.transform.parent.GetComponent<Image>().color = new Color32(0x55, 0xA0, 0xFD, 0xFF);

            coinSayisi -= tower3;
            coinText.text = coinSayisi.ToString();
        }
        else if (up == 4 && tower4 <= coinSayisi)
        {
            tower4Button.SetActive(false);
            tower4Attack.SetActive(true);
            tower4CoolDown.SetActive(true);
            tower4Attack.transform.parent.GetComponent<Image>().color = new Color32(0x55, 0xA0, 0xFD, 0xFF);

            coinSayisi -= tower4;
            coinText.text = coinSayisi.ToString();
        }
        else if (up == 5)
        {
            tower2Attack.SetActive(false);
            tower2CoolDown.SetActive(false);
            tower3Attack.SetActive(false);
            tower3CoolDown.SetActive(false);
            tower4Attack.SetActive(false);
            tower4CoolDown.SetActive(false);
            tower2Button.SetActive(false);
            tower2Button.SetActive(true);
            tower3Button.SetActive(true);
            tower4Button.SetActive(true);
            tower2Attack.transform.parent.GetComponent<Image>().color = new Color32(0x8C, 0x97, 0xAA, 0xFF);
            tower3Attack.transform.parent.GetComponent<Image>().color = new Color32(0x8C, 0x97, 0xAA, 0xFF);
            tower4Attack.transform.parent.GetComponent<Image>().color = new Color32(0x8C, 0x97, 0xAA, 0xFF);
            tower2 *= 4;
            tower3 *= 4;
            tower4 *= 4;
            tower2Button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = tower2/1000+"k";
            tower3Button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = tower3/1000+"k";
            tower4Button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = tower4/1000+"k";

            tower1dmg *= 2;
            tower2dmg *= 2;
            tower3dmg *= 2;
            tower4dmg *= 2;
            tower1Attack.GetComponent<TextMeshProUGUI>().text = (tower1dmg).ToString();
            tower2Attack.GetComponent<TextMeshProUGUI>().text = (tower2dmg).ToString();
            tower3Attack.GetComponent<TextMeshProUGUI>().text = (tower3dmg).ToString();
            tower4Attack.GetComponent<TextMeshProUGUI>().text = (tower4dmg).ToString();

            ChangeLevel(evolveCount);
        }
    }
    public void ChangeLevel(int level)
    {
        if (level == 2)
        {
            tower1Lv2.SetActive(true);
            tower2Lv2.SetActive(true);
            tower3Lv2.SetActive(true);
            tower4Lv2.SetActive(true);
            evolveCount++;
        }
         else if (level == 3)
        {
            tower1Lv3.SetActive(true);
            tower2Lv3.SetActive(true);
            tower3Lv3.SetActive(true);
            tower4Lv3.SetActive(true);
            evolveCount++;
        }
    }

//Kart Men�s�
    public void RandomCard()
    {
        int c = Random.Range(0, deckPrefabs.Count);
        GameObject selectedCard = deckPrefabs[c]; // Rastgele kart se�
        if (c <= 5)
        {
            totalDmg += 0.1f;
            totalDmg = Mathf.Round(totalDmg * 10f) / 10f;
            totalDmgText.text="x"+totalDmg;
        }
        else
        {
            totalHealth += 0.1f;
            totalHealth = Mathf.Round(totalHealth * 10f) / 10f;
            totalHealthText.text = "x" + totalHealth;
        }

        // ��eride ayn� kart var m� kontrol et
        foreach (Transform card in contentTransform)
        {
            if (card.name.StartsWith(selectedCard.name)) // Kart�n ismi i�eridekiyle ayn� m�?
            {
                // Kart�n i�indeki TextMeshPro bile�enini bul
                TextMeshProUGUI cardText = card.GetComponentInChildren<TextMeshProUGUI>();

                if (cardText != null)
                {
                    // �u anki Level'� al, integer'a �evir ve 1 art�r
                    int level = int.Parse(cardText.text.Replace("Level ", "")) + 1;
                    cardText.text = "Level " + level.ToString(); // Yeni de�eri yaz
                }

                Debug.Log("Kart zaten var, level art�r�ld�: " + selectedCard.name);
                return; // Yeni kart eklemeye gerek yok, fonksiyondan ��k
            }
        }

        // E�er kart yoksa yeni kart� ekle
        GameObject newCard = Instantiate(selectedCard, contentTransform);

        // Yeni eklenen kart�n i�indeki TextMeshPro'yu bul ve "Level 1" olarak ba�lat
        TextMeshProUGUI newCardText = newCard.GetComponentInChildren<TextMeshProUGUI>();
        if (newCardText != null)
        {
            newCardText.text = "Level 1";
        }
    }


//Settings Paneli
    public void SetMusicVolume(float volume)
    {
        mixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
    }
    public void SetSFXVolume(float volume)
    {
        mixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
    }
}
