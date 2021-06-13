using System.Collections;
using System.Collections.Generic;
using BreakInfinity;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UpgradesManager : MonoBehaviour
{
    public static UpgradesManager instance;
    private void Awake() => instance = this;

    public List<GameObject> unlockedMonsters;

    public Sprite[] petPreviewImages;
    public string[] petNames;

    public List<Upgrades> clickUpgrades;
    public Upgrades clickUpgradePrefab;

    public List<Upgrades> productionUpgrades;
    public Upgrades productionUpgradesPrefab;

    public ScrollRect clickUpgradesScroll;
    public Transform clickUpgradesPanel;

    public ScrollRect productionUpgradesScroll;
    public Transform productionUpgradesPanel;

    public string[] clickUpgradeNames;
    public string[] productionUpgradeNames;

    public BigDouble[] clickUpgradeBaseCost;
    public BigDouble[] clickUpgradeCostMult;
    public BigDouble[] clickUpgradesBasePower;
    public BigDouble[] clickUpgradeUnlock;

    public BigDouble[] productionUpgradeBaseCost;
    public BigDouble[] productionUpgradeCostMult;
    public BigDouble[] productionUpgradesBasePower;
    public BigDouble[] productionUpgradeUnlock;

    public GameObject battleButton;

    public void StartUpgradeManager()
    {
        Methods.UpgradeCheck(Controller.instance.data.clickUpgradeLevel, 4);

        petNames = new[] { "Amogus", "Cone Cat", "Rock Fairy", "Ice Cream" };
        clickUpgradeNames = new[] { "Click Power +1", "Click Power +5", "Click Power +10", "Click Power +25" };
        productionUpgradeNames = new[]
        {
            "+1 AutoClicks/s",
            "+2 AutoClicks/s",
            "+10 AutoClicks/s",
            "+100 AutoClicks/s"
        };

        clickUpgradeBaseCost = new BigDouble[] { 10, 50, 100, 1000 };
        clickUpgradeCostMult = new BigDouble[] { 1.25, 1.35, 1.55, 2 };
        clickUpgradesBasePower = new BigDouble[] { 1, 5, 10, 25 };
        clickUpgradeUnlock = new BigDouble[] {0, 25, 50, 500 };

        productionUpgradeBaseCost = new BigDouble[] { 25, 100, 1000, 10000 };
        productionUpgradeCostMult = new BigDouble[] { 1.5, 1.75, 2, 3 };
        productionUpgradesBasePower = new BigDouble[] { 1, 2, 10, 100 };
        productionUpgradeUnlock = new BigDouble[] {0, 50, 500, 5000};

        battleButton.SetActive(false);

        for (int i = 0; i < Controller.instance.data.clickUpgradeLevel.Count; i++)
        {
            Upgrades upgrade = Instantiate(clickUpgradePrefab, clickUpgradesPanel);
            upgrade.UpgradeID = i;
            upgrade.gameObject.SetActive(false);
            clickUpgrades.Add(upgrade);
        }

        for (int i = 0; i < Controller.instance.data.productionUpgradeLevel.Count; i++)
        {
            Upgrades upgrade = Instantiate(productionUpgradesPrefab, productionUpgradesPanel);
            upgrade.UpgradeID = i;
            upgrade.gameObject.SetActive(false);
            upgrade.petPreviewImage.sprite = petPreviewImages[i];
            upgrade.PetNameText.text = petNames[i];
            productionUpgrades.Add(upgrade);
        }

        clickUpgradesScroll.normalizedPosition = new Vector2(0, 0);
        productionUpgradesScroll.normalizedPosition = new Vector2(0, 0);

        UpdateUpgradeUI("click");
        UpdateUpgradeUI("production");
        CheckMonsterUnlocked();
    }

    public void Update()
    {
        for (int i = 0; i < clickUpgrades.Count; i++)
        {
            if (!clickUpgrades[i].gameObject.activeSelf)
                clickUpgrades[i].gameObject.SetActive(Controller.instance.data.clicks >= clickUpgradeUnlock[i]);
            if(Controller.instance.data.clickUpgradeLevel[i] > 0)
                clickUpgrades[i].gameObject.SetActive(true);
        }

        for (int i = 0; i < clickUpgrades.Count; i++)
        {
            if (!productionUpgrades[i].gameObject.activeSelf)
                productionUpgrades[i].gameObject.SetActive(Controller.instance.data.clicks >= productionUpgradeUnlock[i]);
            if (Controller.instance.data.productionUpgradeLevel[i] > 0)
                productionUpgrades[i].gameObject.SetActive(true);
        }

        if (Controller.instance.data.productionUpgradeLevel[2] > 0)
            battleButton.SetActive(true);
    }

    public void UpdateUpgradeUI(string type,int UpgradeID = -1)
    {
        var data = Controller.instance.data;

        switch(type)
        {
            case "click":
                if (UpgradeID == -1)
                    for (int i = 0; i < clickUpgrades.Count; i++) UpdateUI(clickUpgrades, data.clickUpgradeLevel, clickUpgradeNames, i);
                else UpdateUI(clickUpgrades, data.clickUpgradeLevel, clickUpgradeNames, UpgradeID);
                break;
            case "production":
                if (UpgradeID == -1)
                    for (int i = 0; i < productionUpgrades.Count; i++) UpdateUI(productionUpgrades, data.productionUpgradeLevel, productionUpgradeNames, i);
                else
                {
                    UpdateUI(productionUpgrades, data.productionUpgradeLevel, productionUpgradeNames, UpgradeID);
                }
                break;
        }

        void UpdateUI(List<Upgrades> upgrades, List<int> upgradeLevels, string[] upgradeNames, int ID)
        {
            upgrades[ID].LevelText.text = $"Lv. {upgradeLevels[ID].ToString()}";
            upgrades[ID].CostText.text = $"Cost: {UpgradeCost(type, ID):F2} Clicks";
            upgrades[ID].NameText.text = upgradeNames[ID];
        }
    }

    public BigDouble UpgradeCost(string type,int UpgradeID)
    {
        var data = Controller.instance.data;
        switch(type)
        {
            case "click":
                return clickUpgradeBaseCost[UpgradeID] * BigDouble.Pow(clickUpgradeCostMult[UpgradeID], (BigDouble)data.clickUpgradeLevel[UpgradeID]);
            case "production":
                return productionUpgradeBaseCost[UpgradeID] * BigDouble.Pow(productionUpgradeCostMult[UpgradeID], (BigDouble)data.productionUpgradeLevel[UpgradeID]);
        }

        return 0;
    }

    public void BuyUpgrade(string type, int UpgradeID)
    {
        var data = Controller.instance.data;

        switch (type)
        {
            case "click":
                Buy(data.clickUpgradeLevel);
                break;
            case "production":
                Buy(data.productionUpgradeLevel);
                if(data.productionUpgradeLevel[UpgradeID] > 0)
                    data.monstersUnlocked[UpgradeID] = true;
                CheckMonsterUnlocked(UpgradeID);
                TeamSelectManager.instance.UpdateSelectTeamUI();
                break;
        }

        void Buy(List<int> upgradeLevels)
        {
            if (data.clicks >= UpgradeCost(type, UpgradeID))
            {
                data.clicks -= UpgradeCost(type, UpgradeID);
                upgradeLevels[UpgradeID] += 1;
            }

            UpdateUpgradeUI(type, UpgradeID);
        }
    }

    public void CheckMonsterUnlocked(int UpgradeID = -1)
    {
        var data = Controller.instance.data;
        if (UpgradeID == -1)
        {
            for (int i = 0; i < data.monstersUnlocked.Count; i++)
            {
                bool setActive = data.monstersUnlocked[i];
                unlockedMonsters[i].SetActive(setActive);
            }
        }
        else
        {
            bool setActive = data.monstersUnlocked[UpgradeID];
            unlockedMonsters[UpgradeID].SetActive(setActive);
        }
    }
}

   
