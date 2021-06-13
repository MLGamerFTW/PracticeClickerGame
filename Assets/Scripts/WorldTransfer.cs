using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WorldTransfer : MonoBehaviour
{
    public GameObject ClickGameCanvas;
    public GameObject TeamSelectCanvas;
    public GameObject AutoBattleCanvas;
    
    public void GoToTeamSelect()
    {
        TeamSelectCanvas.SetActive(true);
        ClickGameCanvas.SetActive(false);
        AutoBattleCanvas.SetActive(false);
    }

    public void GoToClickGame()
    {
        ClickGameCanvas.SetActive(true);
        AutoBattleCanvas.SetActive(false);
        TeamSelectCanvas.SetActive(false);
        TeamSelectManager.instance.ResetSelectedTeam();
    }

    public void StartBattle()
    {
        if (TeamSelectManager.instance.ChosenMonsters[2] != -1)
        {
            AutoBattleCanvas.SetActive(true);
            ClickGameCanvas.SetActive(false);
            TeamSelectCanvas.SetActive(false);
            BattleSystem.instance.StartBattle();
        }
    }
}
