using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterStats : MonoBehaviour
{
    public int MonsterID;
    public int MonsterMaxHP;
    public int MonsterCurrentHP;
    public int MonsterAttackPower;
    public Slider hpSlider;
    public GameObject Monster;
    public Animator MonsterAnimator;
    
    public bool TakeDamage(int dmg)
    {
        MonsterCurrentHP -= dmg;

        if (MonsterCurrentHP <= 0)
            return false;
        else
            return true;
    }

    public void SetHP()
    {
        hpSlider.value = MonsterCurrentHP;
    }
}
