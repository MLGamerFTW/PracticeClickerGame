using UnityEngine.UI;
using TMPro;
using UnityEngine;
using UnityEditor.Animations;

public class TeamSelect : MonoBehaviour
{
    public int MonsterID;
    public Image SelectButton;
    public TMP_Text LevelText;
    public TMP_Text NameText;
    public Animator MonsterPreview;

    public void SelectMonster() => TeamSelectManager.instance.SelectTeamMember(MonsterID, TeamSelectManager.instance.currentSelectedMonster);
}
