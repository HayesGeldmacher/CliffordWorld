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
        int skillNum = 0;
        foreach (TargetSkill skill in unit._targetSkills)
        {
            //unit can only have 6 skills equipped at a time
            if (skillNum >= 5) return;

            Button currentButton = _targetSkillButttons[skillNum];
            ChangeCallback(currentButton, unit._targetSkills[skillNum]);
            CheckSkillRequirements(skill, unit, currentButton);
        }

        skillNum += 1; 
    }

    void ChangeCallback(Button button, TargetSkill skill)
    {
        Debug.Log("Called on " + button.name);
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => _battleManager.OnSkillButton(skill));
    }

    public void ListenerCalled()
    {
        Debug.Log("Listener was fucking called!");
    }

    public void CheckSkillRequirements(TargetSkill skill, BattleUnit unit, Button button)
    {
        //checking requirements of a skill before it can be cast!
        bool canCast = true;

        int APCost = skill._APCost;
        int currentAP = unit._currentAP;
        if(APCost > 0)
        {
            if(APCost > currentAP)
            {
                canCast = false;
            }
        }

        int HPCost = skill._HPCost;
        int currentHP = unit._currentHP;
        if(HPCost > 0)
        {
            if(HPCost > currentHP)
            {
                canCast = false;
            }
        }

        if (!canCast)
        {
            button.interactable = false;
        }

    }
}
