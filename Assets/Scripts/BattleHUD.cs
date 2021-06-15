using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleHUD : MonoBehaviour
{
    public TMP_Text battleText;
    public TMP_Text currentEnemyText;
    public TMP_Text rewardText;
    public GameObject outcomePanel;

    public void SetBattleHUD()
    {
        battleText.text = "";
        currentEnemyText.text = "";
        rewardText.text = "";
    }

    public void TurnOutcomePanelOff()
    {
        outcomePanel.SetActive(false);
    }
}
