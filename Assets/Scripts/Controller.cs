using UnityEngine;
using TMPro;
using BreakInfinity;

public class Controller : MonoBehaviour
{
    public static Controller instance;
    private void Awake() => instance = this;
  

    public Data data;

    [SerializeField] private TMP_Text clicksText;
    [SerializeField] private TMP_Text autoClicksPerSecondText;
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

    public BigDouble AutoClicksPerSecond()
    {
        BigDouble total = 0;
        for (int i = 0; i < data.productionUpgradeLevel.Count; i++)
        {
            total += UpgradesManager.instance.productionUpgradesBasePower[i] * data.productionUpgradeLevel[i];
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
        clicksText.text = $"{data.clicks:F2} Clicks";
        autoClicksPerSecondText.text = $"{AutoClicksPerSecond():F2}/s";
        clickClickPowerText.text = $"+ {ClickPower()} Clicks";

        data.clicks += AutoClicksPerSecond() * Time.deltaTime;
    }

    public void GenerateClick()
    {
        data.clicks += ClickPower();
    }
}
