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

    public string dataFileName = "PlayerData_Tutorial";
    private void Start()
    {
        data = SaveSystem.SaveExists(dataFileName)
            ? SaveSystem.LoadData<Data>(dataFileName)
            : new Data();

        UpgradesManager.instance.StartUpgradeManager();
        TeamSelectManager.instance.StartTeamSelectManager();
    }

    public float SaveTime;

    private void Update()
    {
        clicksText.text = $"{data.clicks.Notate()} Clicks";
        autoClicksPerSecondText.text = $"{AutoClicksPerSecond().Notate()}/s";
        clickClickPowerText.text = $"+ {ClickPower()} Clicks";

        data.clicks += AutoClicksPerSecond() * Time.deltaTime;

        SaveTime += Time.deltaTime;
        if(SaveTime >= 5)
        {
            SaveSystem.SaveData(data, dataFileName);
            SaveTime = 0;
        }
    }

    public void GenerateClick()
    {
        data.clicks += ClickPower();
    }

}
