using System.Collections;
using System.Collections.Generic;
using BreakInfinity;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;

public class TeamSelectManager : MonoBehaviour
{
    public static TeamSelectManager instance;
    private void Awake() => instance = this;

    public List<TeamSelect> teamSelectList;
    public TeamSelect teamSelectPrefab;

    public ScrollRect teamSelectScroll;
    public RectTransform teamSelectPanel;

    public Animator[] selectedMonsters;
    public int[] ChosenMonsters;
    
    public Image[] selectedMonsterPreview;
    public Sprite petPreview;

    public int currentSelectedMonster;

    public Animator[] choosableMonsterImages;

    public void StartTeamSelectManager()
    {
        ChosenMonsters = new int[] { -1, -1, -1 };

        for (int i = 0; i < Controller.instance.data.productionUpgradeLevel.Count; i++)
        {
            TeamSelect teamSelect = Instantiate(teamSelectPrefab, teamSelectPanel);
            teamSelect.MonsterID = i;
            teamSelect.gameObject.SetActive(false);
            teamSelectList.Add(teamSelect);                       
        }

        teamSelectScroll.normalizedPosition = new Vector2(0, 0);
        teamSelectPanel.offsetMin = teamSelectPanel.offsetMax = Vector2.zero;
        UpdateSelectTeamUI();
    }

    public void UpdateSelectTeamUI()
    {
        for (int i = 0; i < teamSelectList.Count; i++)
        {
            teamSelectList[i].MonsterPreview.runtimeAnimatorController = choosableMonsterImages[i].runtimeAnimatorController;
            teamSelectList[i].NameText.text = UpgradesManager.instance.petNames[i];
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
            case -1:
                break;
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
            ChosenMonsters[0] = ID;
            currentSelectedMonster = 1;
            teamSelectList[ID].gameObject.SetActive(false);
        }

        void SelectTeamMember2(int ID)
        {
            selectedMonsters[1].runtimeAnimatorController = teamSelectList[ID].MonsterPreview.runtimeAnimatorController;
            ChosenMonsters[1] = ID;
            currentSelectedMonster = 2;
            teamSelectList[ID].gameObject.SetActive(false);
        }

        void SelectTeamMember3(int ID)
        {
            selectedMonsters[2].runtimeAnimatorController = teamSelectList[ID].MonsterPreview.runtimeAnimatorController;
            ChosenMonsters[2] = ID;
            currentSelectedMonster = -1;
            teamSelectList[ID].gameObject.SetActive(false);
        }
    }

    public void ResetSelectedTeam()
    {
        for (int i = 0; i < teamSelectList.Count; i++)
        {
            if (!teamSelectList[i].gameObject.activeSelf)
            {
                teamSelectList[i].gameObject.SetActive(Controller.instance.data.productionUpgradeLevel[i] > 0);
            }
        }

        currentSelectedMonster = 0;
        ChosenMonsters =new int[] { -1, -1, -1};

        for (int i = 0; i < selectedMonsters.Length; i++)
        {
            selectedMonsters[i].runtimeAnimatorController = null;
            selectedMonsterPreview[i].sprite = petPreview;
        }
    }
}
