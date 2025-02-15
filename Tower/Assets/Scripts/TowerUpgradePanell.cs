using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TowerUpgradePanel : MonoBehaviour
{
    public TMP_Text requiredPointsText;
    public Button upgradeButton;
    public ProgressBar progressBar;
    public TowerAttack tower;

    public int initialRequiredPoints = 3; // Baþlangýç seviyesi için gereken puan
    public int[] upgradeCosts = { 3, 5, 7, 10 }; // Seviye baþýna gereken puanlar

    private int currentLevel = 0;

    void Start()
    {
        upgradeButton.onClick.AddListener(UpgradeTower);
        UpdateUI();
    }

    void Update()
    {
        int requiredPoints = GetRequiredPoints();
        upgradeButton.interactable = progressBar.GetCount() >= requiredPoints;
    }

    void UpgradeTower()
    {
        int requiredPoints = GetRequiredPoints();

        if (progressBar.GetCount() >= requiredPoints)
        {
            progressBar.UsePoints(requiredPoints);
            tower.UpgradeTower();
            currentLevel++;
            UpdateUI();
        }
    }

    void UpdateUI()
    {
        requiredPointsText.text = GetRequiredPoints().ToString();
    }

    int GetRequiredPoints()
    {
        if (currentLevel < upgradeCosts.Length)
            return upgradeCosts[currentLevel]; // Listedeki deðeri kullan
        else
            return upgradeCosts[upgradeCosts.Length - 1] + (currentLevel - upgradeCosts.Length + 1) * 5; // Maksimumdan sonra artýþ
    }
}
