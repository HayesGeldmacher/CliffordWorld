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
        TargetSkill[] skills = unit._targetSkills;

        Button currentButton = _targetSkillButttons[0];
        ChangeCallback(currentButton, skills[0]);
        
    }

    void ChangeCallback(Button button, TargetSkill skill)
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => {
            _battleManager.OnSkillButton(skill);
        });
    }
}
