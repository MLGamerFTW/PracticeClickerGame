using UnityEngine;
using TMPro;

public class Controller : MonoBehaviour
{
    public Data data;

    public TMP_Text clicksText;

    private void Start()
    {
        data = new Data();
    }

    private void Update()
    {
        clicksText.text = data.clicks + " Clicks";
    }

    public void GenerateClick()
    {
        data.clicks += 1;
    }
}
