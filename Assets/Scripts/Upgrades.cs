using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class Upgrades : MonoBehaviour
{
    public int UpgradeID;
    public Image UpgradeButton;
    public TMP_Text LevelText;
    public TMP_Text NameText;
    public TMP_Text CostText;

    public void BuyClickUpgrade() => UpgradesManager.instance.BuyUpgrade(UpgradeID);
}
