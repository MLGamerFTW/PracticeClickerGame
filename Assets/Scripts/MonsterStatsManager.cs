using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStatsManager : MonoBehaviour
{
    public static MonsterStatsManager instance;
    private void Awake() => instance = this;

    public List<MonsterStats> monsterStatsList;
    public MonsterStats monsterStatsPrefab;

    public GameObject[] monsterObjects;
    public Animator[] monsterAnimators;

    public void StartMonsterStatsManager()
    {
        for (int i = 0; i < 3; i++)
        {
            MonsterStats monsterStats = new MonsterStats();
            monsterStats.MonsterID = TeamSelectManager.instance.ChosenMonsters[i];
            monsterStats.MonsterMaxHP = (int)(Math.Pow((double)Controller.instance.data.productionUpgradeLevel[monsterStats.MonsterID], (double)1.1) * 5);
            monsterStats.MonsterCurrentHP = (int)(Math.Pow((double)Controller.instance.data.productionUpgradeLevel[monsterStats.MonsterID], (double)1.1) * 5);
            monsterStats.MonsterAttackPower = (int)(Math.Pow((double)Controller.instance.data.productionUpgradeLevel[monsterStats.MonsterID], (double)1.1) * 2);
            monsterStats.Monster = monsterObjects[i];
            monsterStats.MonsterAnimator = monsterAnimators[i];
            monsterStats.gameObject.SetActive(false);
            monsterStatsList.Add(monsterStats);
        }
    }


}
