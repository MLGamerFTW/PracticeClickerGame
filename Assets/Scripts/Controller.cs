using UnityEngine;
using TMPro;
using BreakInfinity;

public class Controller : MonoBehaviour
{
    public UpgradesManager upgradesManager;
    public Data data;

    [SerializeField] private TMP_Text clicksText;
    [SerializeField] private TMP_Text clickClickPowerText;

    public BigDouble ClickPower() => 1 + data.clickUpgradeLevel;
    
    private void Start()
    {
        data = new Data();
        upgradesManager.StartUpgradeManager();
    }

    private void Update()
    {
        clicksText.text = data.clicks + " Clicks";
        clickClickPowerText.text = "+" + ClickPower() + " Clicks";
    }

    public void GenerateClick()
    {
        data.clicks += ClickPower();
    }
}
