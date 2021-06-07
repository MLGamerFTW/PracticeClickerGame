using System.Collections;
using System.Collections.Generic;
using BreakInfinity;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor.Animations;

public class TeamSelectManager : MonoBehaviour
{
    public static TeamSelectManager instance;
    private void Awake() => instance = this;

    public List<TeamSelect> teamSelectList;
    public TeamSelect teamSelectPrefab;

    public ScrollRect teamSelectScroll;
    public Transform teamSelectPanel;

    public Animator[] selectedMonsters;

    public int currentSelectedMonster;

    public Animator[] choosableMonsterImages;

    public string[] monsterNames;

    public void StartTeamSelectManager()
    {
        monsterNames = new[] { "Monster0", "Monster1", "Monster2", "Monster3" };

        for (int i = 0; i < Controller.instance.data.productionUpgradeLevel.Count; i++)
        {
            TeamSelect teamSelect = Instantiate(teamSelectPrefab, teamSelectPanel);
            teamSelect.MonsterID = i;
            teamSelect.gameObject.SetActive(false);
            teamSelectList.Add(teamSelect);                       
        }

        teamSelectScroll.normalizedPosition = new Vector2(0, 0);

        UpdateSelectTeamUI();
    }
    
    public void UpdateSelectTeamUI()
    {
        for (int i = 0; i < teamSelectList.Count; i++)
        {
            teamSelectList[i].MonsterPreview = choosableMonsterImages[i];
            teamSelectList[i].NameText.text = monsterNames[i];
            teamSelectList[i].LevelText.text = $"Level: {Controller.instance.data.productionUpgradeLevel[i]}";
            
            if (!teamSelectList[i].gameObject.activeSelf)
            {
                teamSelectList[i].gameObject.SetActive(Controller.instance.data.productionUpgradeLevel[i] > 0);
            }
        }
    }

    public void SelectTeamMember(int MonsterID, int currentSelection )
    {
        switch(currentSelection)
        {
            case 0:
                SelectTeamMember1(MonsterID);
                break;
            case 1:
                SelectTeamMember2(MonsterID);
                break;
            case 2:
                SelectTeamMember3(MonsterID);
                break;
        }

        void SelectTeamMember1(int ID)
        {
            selectedMonsters[0].runtimeAnimatorController = teamSelectList[ID].MonsterPreview.runtimeAnimatorController;
            currentSelectedMonster = 1;
        }

        void SelectTeamMember2(int ID)
        {
            selectedMonsters[1].runtimeAnimatorController = teamSelectList[ID].MonsterPreview.runtimeAnimatorController;
            currentSelectedMonster = 2;
        }

        void SelectTeamMember3(int ID)
        {
            selectedMonsters[2].runtimeAnimatorController = teamSelectList[ID].MonsterPreview.runtimeAnimatorController;
            currentSelectedMonster = 0;
        }
    }
}
