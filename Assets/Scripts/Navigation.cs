using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Navigation : MonoBehaviour
{
    public GameObject ClickUpgradesNotSelected;
    public GameObject ProductionUpgradesNotSelected;

    public TMP_Text ClickUpgradesTitleText;
    public TMP_Text ProductionUpgradesTitleText;

    public void SwitchUpgrades(string location)
    {
        UpgradesManager.instance.clickUpgradesScroll.gameObject.SetActive(false);
        UpgradesManager.instance.productionUpgradesScroll.gameObject.SetActive(false);

        ClickUpgradesNotSelected.SetActive(true);
        ProductionUpgradesNotSelected.SetActive(true);

        ClickUpgradesTitleText.color = Color.grey;
        ProductionUpgradesTitleText.color = Color.grey;

        switch (location)
        {
            case "Click":
                UpgradesManager.instance.clickUpgradesScroll.gameObject.SetActive(true);
                ClickUpgradesNotSelected.SetActive(false);
                ClickUpgradesTitleText.color = Color.white;
                break;
            case "Production":
                UpgradesManager.instance.productionUpgradesScroll.gameObject.SetActive(true);
                ProductionUpgradesNotSelected.SetActive(false);
                ProductionUpgradesTitleText.color = Color.white;
                break;
        }
    }
}
