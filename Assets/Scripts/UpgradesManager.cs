using System.Collections;
using System.Collections.Generic;
using BreakInfinity;
using UnityEngine;
using UnityEngine.UI;

public class UpgradesManager : MonoBehaviour
{
    public static UpgradesManager instance;
    private void Awake() => instance = this;

    public List<Upgrades> clickUpgrades;
    public Upgrades clickUpgradePrefab;

    public ScrollRect clickUpgradesScroll;
    public Transform clickUpgradesPanel;

    public string[] clickUpgradeNames;

    public BigDouble[] clickUpgradeBaseCost;
    public BigDouble[] clickUpgradeCostMult;
    public BigDouble[] clickUpgradesBasePower;

    public void StartUpgradeManager()
    {
        clickUpgradeNames = new[] { "Click Power +1", "Click Power +5", "Click Power +10" };
        clickUpgradeBaseCost = new BigDouble[] { 10, 50, 100 };
        clickUpgradeCostMult = new BigDouble[] { 1.25, 1.35, 1.55 };
        clickUpgradesBasePower = new BigDouble[] { 1, 5, 10 };

        for (int i = 0; i < Controller.instance.data.clickUpgradeLevel.Count; i++)
        {
            Upgrades upgrade = Instantiate(clickUpgradePrefab, clickUpgradesPanel);
            upgrade.UpgradeID = i;
            clickUpgrades.Add(upgrade);
        }
        clickUpgradesScroll.normalizedPosition = new Vector2(0, 0);

        UpdateClickUpgradeUI();
    }

    public void UpdateClickUpgradeUI(int UpgradeID = -1)
    {
        var data = Controller.instance.data;
        if (UpgradeID == -1)
            for (int i = 0; i < clickUpgrades.Count; i++) UpdateUI(i);
        else UpdateUI(UpgradeID);

        void UpdateUI(int ID)
        {
            clickUpgrades[ID].LevelText.text = data.clickUpgradeLevel[ID].ToString();
            clickUpgrades[ID].CostText.text = $"Cost:  {ClickUpgradeCost(ID):F2} +  Clicks";
            clickUpgrades[ID].NameText.text = "+1 " + clickUpgradeNames[ID];
        }
    }

    public BigDouble ClickUpgradeCost(int UpgradeID) => clickUpgradeBaseCost[UpgradeID] * BigDouble.Pow(clickUpgradeCostMult[UpgradeID], Controller.instance.data.clickUpgradeLevel[UpgradeID]);

    public void BuyUpgrade(int UpgradeID)
    {
        var data = Controller.instance.data;
        if (data.clicks >= ClickUpgradeCost(UpgradeID))
        {
            data.clicks -= ClickUpgradeCost(UpgradeID);
            data.clickUpgradeLevel[UpgradeID] += 1;
        }

        UpdateClickUpgradeUI(UpgradeID);
    }
}
