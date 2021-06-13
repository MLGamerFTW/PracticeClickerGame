using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BattleHUD : MonoBehaviour
{
    public TMP_Text battleText;

    public void SetBattleHUD()
    {
        battleText.text = "";
    }
}
