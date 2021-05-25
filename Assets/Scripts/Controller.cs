using UnityEngine;
using TMPro;
using BreakInfinity;

public class Controller : MonoBehaviour
{
    public static Controller instance;
    private void Awake() => instance = this;
  

    public Data data;

    [SerializeField] private TMP_Text clicksText;
    [SerializeField] private TMP_Text clickClickPowerText;

    public BigDouble ClickPower()
    {
        BigDouble total = 1;
        for(int i = 0; i < data.clickUpgradeLevel.Count; i++)
        {
            total += UpgradesManager.instance.clickUpgradesBasePower[i] * data.clickUpgradeLevel[i];
        }

        return total;
    }
    
    private void Start()
    {
        data = new Data();
        UpgradesManager.instance.StartUpgradeManager();
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
