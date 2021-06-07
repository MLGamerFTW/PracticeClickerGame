using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WorldTransfer : MonoBehaviour
{
    public GameObject ClickGameCanvas;
    public GameObject AutoBattleCanvas;

    public void GoToAutoBattle()
    {
        AutoBattleCanvas.SetActive(true);
        ClickGameCanvas.SetActive(false);
    }

    public void GoToClickGame()
    {
        ClickGameCanvas.SetActive(true);
        AutoBattleCanvas.SetActive(false);
    }
}
