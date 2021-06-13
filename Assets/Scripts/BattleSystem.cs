using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;
using Random = UnityEngine.Random;
using TMPro;


public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{
    public static BattleSystem instance;
    private void Awake() => instance = this;

    public List<MonsterStats> friendlyMonsterStatsList;
    public List<MonsterStats> enemyMonsterStatsList;

    public GameObject friendlyPrefab;
    public GameObject enemyPrefab;

    public Slider[] friendlyHPSliders;
    public Slider[] enemyHPSliders;

    public Transform[] playerBattleStations;
    public Transform enemyBattleStation;

    public Animator[] friendlyAnimators;
    public Animator[] enemyAnimators;

    public bool[] friendlyAlive;
    public bool[] enemyAlive;

    public int currentFriendly;
    public int currentEnemy;

    public BattleHUD battleHUD;

    public BattleState state;

    public void StartBattle()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject friendlyGO = Instantiate(friendlyPrefab, playerBattleStations[i]);
            MonsterStats friendlyUnit = friendlyGO.GetComponent<MonsterStats>();

            friendlyUnit.MonsterID = TeamSelectManager.instance.ChosenMonsters[i];
            friendlyUnit.MonsterMaxHP = (int)(Math.Pow(Controller.instance.data.productionUpgradeLevel[friendlyUnit.MonsterID], 1.1) * 5);
            friendlyUnit.MonsterCurrentHP = (int)(Math.Pow(Controller.instance.data.productionUpgradeLevel[friendlyUnit.MonsterID], 1.1) * 5);
            friendlyUnit.MonsterAttackPower = (int)(Math.Pow(Controller.instance.data.productionUpgradeLevel[friendlyUnit.MonsterID], 1.1) * 1.3 + 1);
            friendlyUnit.MonsterAnimator.runtimeAnimatorController = friendlyAnimators[friendlyUnit.MonsterID].runtimeAnimatorController;

            friendlyUnit.hpSlider.maxValue = friendlyUnit.MonsterMaxHP;
            friendlyUnit.hpSlider.value = friendlyUnit.MonsterCurrentHP;

            friendlyMonsterStatsList.Add(friendlyUnit);
        }

        for (int i = 0; i < 3; i++)
        {
            int enemyType = Random.Range(0, 3);
            var petLevel = Controller.instance.data.productionUpgradeLevel;
            int enemyLevel = ((petLevel[friendlyMonsterStatsList[0].MonsterID] + petLevel[friendlyMonsterStatsList[1].MonsterID] + petLevel[friendlyMonsterStatsList[2].MonsterID]) * 1) / 2 + (i - 1);
            GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
            MonsterStats enemyUnit = enemyGO.GetComponent<MonsterStats>();

            enemyUnit.MonsterMaxHP = (int)(Math.Pow(enemyLevel, 1.1) * 5) + 1;
            enemyUnit.MonsterCurrentHP = (int)(Math.Pow(enemyLevel, 1.1) * 5) + 1;
            enemyUnit.MonsterAttackPower = (int)(Math.Pow(enemyLevel, 1.1) * 1 + 0);
            enemyUnit.MonsterAnimator.runtimeAnimatorController = enemyAnimators[enemyType].runtimeAnimatorController;

            enemyUnit.hpSlider.maxValue = enemyUnit.MonsterMaxHP;
            enemyUnit.hpSlider.value = enemyUnit.MonsterCurrentHP;
            enemyUnit.Monster.SetActive(false);
            enemyMonsterStatsList.Add(enemyUnit);
        }
        enemyMonsterStatsList[0].Monster.SetActive(true);
        currentEnemy = 0;
        currentFriendly = 0;

        friendlyAlive = new[] { true, true, true };
        enemyAlive = new[] { true, true, true };

        battleHUD.SetBattleHUD();

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        StartCoroutine(PlayerTurn());
    }

    IEnumerator PlayerTurn()
    {
        int numOfAttackfriendlyAlive = 0;
        for (int i = 0; i < 3; i++)
        {
            if (friendlyAlive[i])
                numOfAttackfriendlyAlive++;
        }

        currentFriendly = 0;
        for (int i = 0; i < numOfAttackfriendlyAlive; i++)
        {
            if (!friendlyAlive[currentFriendly])
            {
                if (currentFriendly != 2 && friendlyAlive[currentFriendly + 1])
                    currentFriendly += 1;
                else if (currentFriendly != 1 && friendlyAlive[currentFriendly + 2])
                    currentFriendly += 2;
            }
            

            if (friendlyAlive[currentFriendly])
            {
                friendlyMonsterStatsList[currentFriendly].MonsterAnimator.SetBool("IsAttacking", true);

                yield return new WaitForSeconds(1f);

                enemyAlive[currentEnemy] = enemyMonsterStatsList[currentEnemy].TakeDamage(friendlyMonsterStatsList[currentFriendly].MonsterAttackPower);

                enemyMonsterStatsList[currentEnemy].SetHP();

                battleHUD.battleText.text = $"{UpgradesManager.instance.petNames[friendlyMonsterStatsList[currentFriendly].MonsterID]} attacked the monster for {friendlyMonsterStatsList[currentFriendly].MonsterAttackPower} HP";

                friendlyMonsterStatsList[currentFriendly].MonsterAnimator.SetBool("IsAttacking", false);

                yield return new WaitForSeconds(3f);

                if (!enemyAlive[currentEnemy])
                {

                    enemyMonsterStatsList[currentEnemy].Monster.SetActive(false);
                    currentEnemy++;
                    battleHUD.battleText.text = $"{UpgradesManager.instance.petNames[friendlyMonsterStatsList[currentFriendly].MonsterID]} killed the monster";
                    yield return new WaitForSeconds(3f);
                    if (currentEnemy < 3)
                    {
                        enemyMonsterStatsList[currentEnemy].Monster.SetActive(true);
                        yield return new WaitForSeconds(2f);
                    }

                    if (currentEnemy >= 3)
                    {
                        state = BattleState.WON;
                        EndBattle();
                        break;
                    }

                    if (numOfAttackfriendlyAlive-1 == i)
                    {
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                }
                else
                {
                    if (numOfAttackfriendlyAlive-1 == i)
                    {
                        state = BattleState.ENEMYTURN;
                        StartCoroutine(EnemyTurn());
                    }
                }
            }
            currentFriendly++;
        }

    }

    IEnumerator EnemyTurn()
    {
        for (int i = 0; i < 2; i++)
        {
            int enemyTarget = Random.Range(0, 3);
            while(!friendlyAlive[enemyTarget])
            {
                enemyTarget = Random.Range(0, 3);
            }

            enemyMonsterStatsList[currentEnemy].MonsterAnimator.SetBool("IsAttacking", true);

            yield return new WaitForSeconds(1f);

            friendlyAlive[enemyTarget] = friendlyMonsterStatsList[enemyTarget].TakeDamage(enemyMonsterStatsList[currentEnemy].MonsterAttackPower);

            battleHUD.battleText.text = $"The monster attacked {UpgradesManager.instance.petNames[friendlyMonsterStatsList[enemyTarget].MonsterID]} for {enemyMonsterStatsList[currentEnemy].MonsterAttackPower} HP";

            friendlyMonsterStatsList[enemyTarget].SetHP();

            enemyMonsterStatsList[currentEnemy].MonsterAnimator.SetBool("IsAttacking", false);

            yield return new WaitForSeconds(3f);

            if (!friendlyAlive[enemyTarget])
            {
                friendlyMonsterStatsList[enemyTarget].Monster.SetActive(false);
                battleHUD.battleText.text = $"The monster killed {UpgradesManager.instance.petNames[friendlyMonsterStatsList[enemyTarget].MonsterID]}";
                yield return new WaitForSeconds(3f);

                if (!friendlyAlive[0] && !friendlyAlive[1] && !friendlyAlive[2])
                {
                    state = BattleState.LOST;
                    EndBattle();
                    break;
                }
                else
                {
                    if (i == 1)
                    {
                        state = BattleState.PLAYERTURN;
                        StartCoroutine(PlayerTurn());
                    }
                }
            }
            else
            {
                if (i == 1)
                {
                    state = BattleState.PLAYERTURN;
                    StartCoroutine(PlayerTurn());
                }
            }
        }
    }

    void EndBattle()
    {
        if(state == BattleState.WON)
            battleHUD.battleText.text = "You won!";
        if (state == BattleState.LOST)
            battleHUD.battleText.text = "You lost!";

        battleHUD.outcomePanel.SetActive(true);
    }

    /*
    IEnumerator EnemyTurn()
    {

    }

    IEnumerator PlayerTurn()
    {
        int numOfAttackfriendlyAlive = 0;
        for(int i = 0; i < 3; i++)
        {
            if(friendlyAlive[i])
                numOfAttackfriendlyAlive++;
        }

        for (int i = 0; i < numOfAttackfriendlyAlive; i++)
        { 
        StartCoroutine(FriendlyAttack(i));
        yield return new WaitForSeconds(7f);
        }
    }
    */
}
