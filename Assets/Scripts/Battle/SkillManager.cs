using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{
    public Button[] _targetSkillButttons;
    public BattleManager _battleManager;

    public void SetSkillButtons(BattleUnit unit)
    {
        Button button = _targetSkillButttons[0];

        int skillNum = 0;
        foreach (TargetSkill skill in unit._targetSkills)
        {
            //unit can only have 6 skills equipped at a time
            if (skillNum >= 5) return;

            Button currentButton = _targetSkillButttons[skillNum];
            ChangeCallback(currentButton, unit._targetSkills[skillNum]);
        }

        skillNum += 1; 
    }

    void ChangeCallback(Button button, TargetSkill skill)
    {
        Debug.Log("Called on " + button.name);
        button.onClick.RemoveAllListeners();
        //button.onClick.AddListener(_battleManager.OnSkillButton(skill));
        button.onClick.AddListener(() => _battleManager.OnSkillButton(skill));
    }

    public void ListenerCalled()
    {
        Debug.Log("Listener was fucking called!");
    }
}
