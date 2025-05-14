using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PanelControl : MonoBehaviour
{
    public GameObject[] maps;       // Haritalarý sýrayla tutan dizi
    private int currentIndex = 0;   // Þu anki aktif harita index'i

    public static PanelControl Instance;
    public AudioMixer mixer;
    public Slider MusicSlider, SFXSlider;
    public GameObject settingsPanel;
    public GameObject buttonPanel;
    public GameObject[] panels;
    public Button[] panelButtons; // Tüm butonlar
    public GameObject[] pictures;
    private Vector3 defaultScale = new Vector3(1f, 1f, 1f);
    private Vector3 highlightedScale = new Vector3(1.4f, 1.4f, 4f); // Buton büyüklüðü
    public Animator mainFloorAnim;

    public static int coinSayisi=10000000;
    public TextMeshProUGUI coinText;
    public static int diamondSayisi=0;
    public TextMeshProUGUI diamondText;
    public int cardSayisi = 0;
    public TextMeshProUGUI cardText;
    public Button unlockButton; // Butonlarý açan ana buton
    public Button exitButton;
    public Button[] battlePassButtonsL; // Sol 30 butonu içeren dizi
    public Button[] battlePassButtonsR; // sað 30 butonu içeren dizi
    private List<Button> lockedLeft = new List<Button>(); // Kilitli sol butonlar listesi
    private List<Button> lockedRight = new List<Button>(); // Kilitli sað butonlar listesi
    private int unlockCounter = 0; // Týklama sayacý
    private Dictionary<Button, Color> originalColors = new Dictionary<Button, Color>(); // Butonlarýn orijinal renkleri

    public Slider slider;   // Baðlanacak Slider
    public GameObject battleButton;
    public bool battle = false;
    private float timer = 0f;
    public static int productionCount = 0;
    public TextMeshProUGUI productionText;
    public TextMeshProUGUI productionTime;
    public TextMeshProUGUI productionLevel;
    public TextMeshProUGUI productionCost;
    public TextMeshProUGUI castleHealth;
    public TextMeshProUGUI castleLevel;
    public TextMeshProUGUI castleCost;
    private float proTime=5.2f;
    private int proLevel=1;
    public static float casHealth=1000;
    private int casLevel=1;
    private int x = 1;
    private int y = 1;
    private int tower1dmg = 1;
    public Button tower2ButL, tower3ButL, tower4ButL;
    public Button tower2ButR, tower3ButR, tower4ButR;
    public Button tower2ButM, tower3ButM, tower4ButM;
    public GameObject TowerPanelL, TowerPanelR, TowerPanelM;
    private bool grup1Aktif = true;
    private bool grup2Aktif = false;
    private bool grup3Aktif = false;
    public GameObject tower1Attack, tower1CoolDown, tower1Lv1, tower1Lv2, tower1Lv3;
    private int tower2=2000, tower2dmg=5;
    public GameObject tower2Button, tower2Attack, tower2CoolDown, tower2Lv1, tower2Lv2, tower2Lv3;
    private int tower3=5000, tower3dmg=3;
    public GameObject tower3Button, tower3Attack, tower3CoolDown, tower3Lv1, tower3Lv2, tower3Lv3;
    private int tower4=7000, tower4dmg=9;
    public GameObject tower4Button, tower4Attack, tower4CoolDown, tower4Lv1, tower4Lv2, tower4Lv3;
    public static int evolveCount=2;

    public List<GameObject> deckPrefabs;  // 12 kartlýk prefab listesi (Inspector'dan ekle)
    public Transform contentTransform;    // Scroll View içindeki Content objesi
    public static float totalDmg = 1.0f;
    public TextMeshProUGUI totalDmgText;
    public static float totalHealth = 1.0f;
    public TextMeshProUGUI totalHealthText;

    public GameObject gameOverPanel;
    public GameObject congratsPanel;
    public KaleHealth KaleTotalHealth;

    public static int levelCount = 1;
    public GameObject[] levels; // Level1...Level13 objeleri buraya atanacak


    public bool boolCatapult = false, boolTurret = false, boolCannon = false;


    void Awake()
    {
        Instance = this;
    }
    public void CallSpawnOnActiveLevel()
    {
        foreach (GameObject level in levels)
        {
            if (level.activeSelf)
            {
                EnemyManager manager = level.GetComponentInChildren<EnemyManager>();

                if (manager != null)
                {
                    // private fonksiyonu reflection ile çaðýr
                    manager.Invoke("SpawnEnemy", 0f);
                    Debug.Log($"{level.name} içindeki EnemyManager tetiklendi.");
                }
                else
                {
                    Debug.LogWarning($"{level.name} içinde EnemyManager bulunamadý.");
                }

                break; // sadece aktif olan level için çalýþmalý
            }
        }
    }
    private void Start()
    {
        AssignTowerButtonsByTag(); // Butonlarý dinamik ata
        StartScene();
    }
    private void Update()
    {
        if (battle == true) {Production();}
        MusicSlider.onValueChanged.AddListener(SetMusicVolume);
        SFXSlider.onValueChanged.AddListener(SetSFXVolume);
        productionText.text = productionCount.ToString();
        coinText.text = coinSayisi.ToString();
        if (KaleHealth.Instance.health <= 0) // Kale saðlýðý sýfýr veya daha düþükse
        {
            gameOverPanel.SetActive(true);
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
    public void CongratsPanelOC()
    {
        congratsPanel.SetActive(true);
        battle=false;
        mainFloorAnim = null;
    }
    public void NewMap()
    {
        congratsPanel.SetActive(false);
        panels[2].SetActive(true);
        buttonPanel.SetActive(true);
        slider.gameObject.SetActive(false);
        productionCount = 0;
        battleButton.SetActive(true);
        
     
        if (currentIndex < maps.Length)     // Þu anki haritayý kapat
            maps[currentIndex].SetActive(false);
        currentIndex++;      // Bir sonraki haritaya geç
 
        if (currentIndex < maps.Length)      // Diziyi aþmadýysa yeni haritayý aç
        {
            maps[currentIndex].SetActive(true);
        }
        else
        {
            Debug.Log("Tüm haritalar bitti!");      // Ýstersen burada butonu pasifleþtirebilir veya bir final ekraný açabilirsin.
        }
        StartScene();

    }
    public void StartScene()
    {
        AssignTowerButtonsByTag(); // Butonlarý dinamik ata
        CallSpawnOnActiveLevel();

        if (tower2ButL != null) tower2ButL.interactable = false;
        if (tower3ButL != null) tower3ButL.interactable = false;
        if (tower4ButL != null) tower4ButL.interactable = false;
        if (tower2ButR != null) tower2ButR.interactable = false;
        if (tower3ButR != null) tower3ButR.interactable = false;
        if (tower4ButR != null) tower4ButR.interactable = false;
        if (tower2ButM != null) tower2ButM.interactable = false;
        if (tower3ButM != null) tower3ButM.interactable = false;
        if (tower4ButM != null) tower4ButM.interactable = false;

        if (boolCatapult)
        {
            if (tower2ButL != null) tower2ButL.interactable = true;
            if (tower2ButR != null) tower2ButR.interactable = true;
            if (tower2ButM != null) tower2ButM.interactable = true;
        }
        if (boolTurret)
        {
            if (tower3ButL != null) tower3ButL.interactable = true;
            if (tower3ButR != null) tower3ButR.interactable = true;
            if (tower3ButM != null) tower3ButM.interactable = true;
        }
        if (boolCannon)
        {
            if (tower4ButL != null) tower4ButL.interactable = true;
            if (tower4ButR != null) tower4ButR.interactable = true;
            if (tower4ButM != null) tower4ButM.interactable = true;
        }


        if (TowerPanelL != null) TowerPanelL.SetActive(false);
        if (TowerPanelR != null) TowerPanelR.SetActive(false);
        if (TowerPanelM != null) TowerPanelM.SetActive(false);

        exitButton.gameObject.SetActive(false);
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

        // Eðer mainFloorAnim atanmadýysa sahneden bul
        if (mainFloorAnim == null)
        {
            GameObject obj = GameObject.FindGameObjectWithTag("Floor");
            if (obj != null)
            {
                mainFloorAnim = obj.GetComponent<Animator>();
            }
            else
            {
                Debug.LogWarning("Floor tag'lý obje bulunamadý!");
            }
        }

    }

    //Kart Menüsü
    public void RandomCard()
    {
        if (cardSayisi >= 1)
        {
            int c = Random.Range(0, deckPrefabs.Count);
            GameObject selectedCard = deckPrefabs[c]; // Rastgele kart seç
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

            // Ýçeride ayný kart var mý kontrol et
            foreach (Transform card in contentTransform)
            {
                if (card.name.StartsWith(selectedCard.name)) // Kartýn ismi içeridekiyle ayný mý?
                {
                    // Kartýn içindeki TextMeshPro bileþenini bul
                    TextMeshProUGUI cardText = card.GetComponentInChildren<TextMeshProUGUI>();

                    if (cardText != null)
                    {
                        // Þu anki Level'ý al, integer'a çevir ve 1 artýr
                        int level = int.Parse(cardText.text.Replace("Level ", "")) + 1;
                        cardText.text = "Level " + level.ToString(); // Yeni deðeri yaz
                    }

                    Debug.Log("Kart zaten var, level artýrýldý: " + selectedCard.name);
                    return; // Yeni kart eklemeye gerek yok, fonksiyondan çýk
                }
            }

            // Eðer kart yoksa yeni kartý ekle
            GameObject newCard = Instantiate(selectedCard, contentTransform);

            // Yeni eklenen kartýn içindeki TextMeshPro'yu bul ve "Level 1" olarak baþlat
            TextMeshProUGUI newCardText = newCard.GetComponentInChildren<TextMeshProUGUI>();
            if (newCardText != null)
            {
                newCardText.text = "Level 1";
            }
            cardSayisi--;
            cardText.text= cardSayisi.ToString();
        }
        
    }

//Geliþtirme Menüsü
    public void UpgradeMenu(int up)
    {
        if (up == 0 && x <= coinSayisi) //Üretim hýzýný artýrma
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
        else if (up == 1 && y <= coinSayisi) //Kalenin canýný artýrma
        {
            casHealth += 50;
            casLevel += 1;
            castleHealth.text = casHealth.ToString();
            castleLevel.text ="Level"+ casLevel.ToString();

            coinSayisi -= y;
            y += 1;
            castleCost.text = y.ToString();
            coinText.text = coinSayisi.ToString();
        }
        else if (up == 2 && tower2 <= coinSayisi) // mancýnýðý açma
        {
            tower2Button.SetActive(false); 
            tower2Attack.SetActive(true);
            tower2CoolDown.SetActive(true);
            tower2Attack.transform.parent.GetComponent<Image>().color = new Color32(0x55, 0xA0, 0xFD, 0xFF);
            tower2ButL.interactable = true;
            tower2ButR.interactable = true;
            tower2ButM.interactable = true;

            boolCatapult = true;
            coinSayisi -= tower2;
            coinText.text = coinSayisi.ToString();
        }
        else if (up == 3 && tower3 <= coinSayisi) // turretý açma
        {
            tower3Button.SetActive(false);
            tower3Attack.SetActive(true);
            tower3CoolDown.SetActive(true);
            tower3Attack.transform.parent.GetComponent<Image>().color = new Color32(0x55, 0xA0, 0xFD, 0xFF);
            tower3ButL.interactable = true;
            tower3ButR.interactable = true;
            tower3ButM.interactable = true;

            boolTurret = true;
            coinSayisi -= tower3;
            coinText.text = coinSayisi.ToString();
        }
        else if (up == 4 && tower4 <= coinSayisi) //cannonu açma
        {
            tower4Button.SetActive(false);
            tower4Attack.SetActive(true);
            tower4CoolDown.SetActive(true);
            tower4Attack.transform.parent.GetComponent<Image>().color = new Color32(0x55, 0xA0, 0xFD, 0xFF);
            tower4ButL.interactable = true;
            tower4ButR.interactable = true;
            tower4ButM.interactable = true;

            boolCannon = true;  
            coinSayisi -= tower4;
            coinText.text = coinSayisi.ToString();
        }
        else if (up == 5) //Evolve Butonu
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
            tower2ButL.interactable = tower3ButL.interactable = tower4ButL.interactable = false;
            tower2ButR.interactable = tower3ButR.interactable = tower4ButR.interactable = false;
            tower2ButM.interactable = tower3ButM.interactable = tower4ButM.interactable = false;

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
            for (int i = 0; i < TowerArea.kuleler.GetLength(0); i++)
            {
                TowerArea.kuleler[i, 1] *= 2;
            }
            EvolvePlus();
            GameObject clickedObject = EventSystem.current.currentSelectedGameObject;
            Transform imageTransform = clickedObject.transform.Find($"Image{evolveCount - 1}"); 
            imageTransform.GetComponent<Image>().color = new Color(0.281f, 0.541f, 0.898f); // Rengi deðiþtir

            boolCatapult= false;
            boolTurret= false;
            boolCannon= false;
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
        else if (level == 4) evolveCount++;
        else if (level == 5) evolveCount++;

    }
    public void EvolvePlus()
    {
        if (grup1Aktif)
        {
            grup1Aktif = false;
            grup2Aktif = true;
            grup3Aktif = false;
        }
        else if (grup2Aktif)
        {
            grup1Aktif = false;
            grup2Aktif = false;
            grup3Aktif = true;
        }
        else if (grup3Aktif)
        {
            grup1Aktif = true;
            grup2Aktif = false;
            grup3Aktif = false;
        }

        // Dizideki elemanlarý aktif veya pasif yap
        for (int i = 0; i < 35; i++)
        {
            if (grup1Aktif && i % 3 == 0)
            {
                pictures[i].SetActive(true); // Ýlk grup elemanlarý aktif
            }
            else if (grup2Aktif && i % 3 == 1)
            {
                pictures[i].SetActive(true); // Ýkinci grup elemanlarý aktif
            }
            else if (grup3Aktif && i % 3 == 2)
            {
                pictures[i].SetActive(true); // Üçüncü grup elemanlarý aktif
                pictures[35].SetActive(true) ;
            }
            else
            {
                pictures[i].SetActive(false); // Elemanlar pasif
            }
        }
    }
    private void AssignTowerButtonsByTag()
    {
        TowerPanelL = GameObject.FindGameObjectWithTag("TowerPanelL");
        TowerPanelR = GameObject.FindGameObjectWithTag("TowerPanelR");
        TowerPanelM = GameObject.FindGameObjectWithTag("TowerPanelM");
        tower2ButL = GameObject.FindGameObjectWithTag("tower2ButL")?.GetComponent<Button>();
        tower3ButL = GameObject.FindGameObjectWithTag("tower3ButL")?.GetComponent<Button>();
        tower4ButL = GameObject.FindGameObjectWithTag("tower4ButL")?.GetComponent<Button>();

        tower2ButR = GameObject.FindGameObjectWithTag("tower2ButR")?.GetComponent<Button>();
        tower3ButR = GameObject.FindGameObjectWithTag("tower3ButR")?.GetComponent<Button>();
        tower4ButR = GameObject.FindGameObjectWithTag("tower4ButR")?.GetComponent<Button>();

        tower2ButM = GameObject.FindGameObjectWithTag("tower2ButM")?.GetComponent<Button>();
        tower3ButM = GameObject.FindGameObjectWithTag("tower3ButM")?.GetComponent<Button>();
        tower4ButM = GameObject.FindGameObjectWithTag("tower4ButM")?.GetComponent<Button>();
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
        mainFloorAnim.SetTrigger("floorTrig");
        exitButton.gameObject.SetActive(true);
        battle = true;
        battleButton.SetActive(false);
        slider.gameObject.SetActive(true);
        productionCount = 0;
        panels[2].SetActive(false);
        buttonPanel.SetActive(false);
        //enemySpawn.Spawn();
        KaleTotalHealth.KaleStart();
    }

//BattlePass butonlarý
    public void ButtonClicked( int buttonIndex)
    {
        int diamondsGained = 0;
        int cardsGained = 0;
        switch (buttonIndex)
        {
            case 0: diamondsGained = 1; break;
            case 1: diamondsGained = 2; break;
            case 2: diamondsGained = 3; break;
            case 3: diamondsGained = 4; break;
            case 4: diamondsGained = 5; break;
            case 5: diamondsGained = 10; break;
            case 6: diamondsGained = 15; break;
            case 7: diamondsGained = 20; break;
            case 8: diamondsGained = 25; break;
            case 9: cardsGained = 1; break;
            case 10: cardsGained = 2; break;
            case 11: cardsGained = 3; break;
            case 12: cardsGained = 5; break;
            default: diamondsGained = 0; break;
        }
        diamondSayisi += diamondsGained;
        diamondText.text=diamondSayisi.ToString();
        cardSayisi += cardsGained;
        cardText.text=cardSayisi.ToString();
        
    }
    public void DisableCurrentButton()
    {
        GameObject clickedObject = EventSystem.current.currentSelectedGameObject;
        Button clickedButton = clickedObject.GetComponent<Button>();
        clickedButton.interactable = false;
        clickedButton.image.color = new Color(0.333f, 0.627f, 0.992f); // #55A0FD
        clickedButton.transform.Find("TickIcon").gameObject.SetActive(true); // Tik iþaretini görünür yap

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
            buttonToUnlock.image.color = new Color(0.0f, 0.44f, 0.99f); // #0071FD Rengi

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

//Settings Paneli
    public void SetMusicVolume(float volume)
    {
        mixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
    }
    public void SetSFXVolume(float volume)
    {
        mixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
    }
    public void ExitGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }
    
}
