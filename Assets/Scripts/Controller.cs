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

    public BigDouble ClickPower() => 1 + data.clickUpgradeLevel[0];
    
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
