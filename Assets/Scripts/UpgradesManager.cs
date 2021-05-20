using System.Collections;
using System.Collections.Generic;
using BreakInfinity;
using UnityEngine;

public class UpgradesManager : MonoBehaviour
{
    public Controller controller;

    public Upgrades clickUpgrade;

    public string clickUpgradeName;

    public BigDouble clickUpgradeBaseCost;
    public BigDouble clickUpgradeCostMult;

    public void StartUpgradeManager()
    {
        clickUpgradeName = "Clicks Per Click";
        clickUpgradeBaseCost = 10;
        clickUpgradeCostMult = 1.5;
        UpdateClickUpgradeUI();
    }

    public void UpdateClickUpgradeUI()
    {
        clickUpgrade.LevelText.text = controller.data.clickUpgradeLevel.ToString();
        clickUpgrade.CostText.text = "Cost: " + Cost() + " Clicks";
        clickUpgrade.NameText.text = "+1 " + clickUpgradeName;
    }

    public BigDouble Cost() => clickUpgradeBaseCost* BigDouble.Pow(clickUpgradeCostMult, controller.data.clickUpgradeLevel);

    public void BuyUpgrade()
    {
        if(controller.data.clicks >= Cost())
        {
            controller.data.clicks -= Cost();
            controller.data.clickUpgradeLevel += 1;
        }

        UpdateClickUpgradeUI();
    }
}
