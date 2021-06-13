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
    public Image petPreviewImage;
    public TMP_Text PetNameText;

    public void BuyClickUpgrade() => UpgradesManager.instance.BuyUpgrade("click", UpgradeID);
    public void BuyProductionUpgrade() => UpgradesManager.instance.BuyUpgrade("production", UpgradeID);
}
