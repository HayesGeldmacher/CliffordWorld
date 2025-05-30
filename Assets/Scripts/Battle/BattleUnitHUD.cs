using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BattleUnitHUD : MonoBehaviour
{
    public TMP_Text _nameText;
    public TMP_Text _currentAPText;
    public TMP_Text _currentHPText;
    public TMP_Text _enemyNameText;

    public void SetHUD(BattleUnit unit)
    {
        _nameText.text = unit._unitName;
        string currentHP = unit._currentHP.ToString();
        string maxHP = unit._maxHP.ToString();
        _currentHPText.text = currentHP + " / " + maxHP;
       // _currentAPText.text = unit._currentAP + " / " + unit._maxAP;
    }

    public void SetHP(BattleUnit unit)
    {
        string currentHP = unit._currentHP.ToString();
        string maxHP = unit._maxHP.ToString();
        _currentHPText.text = currentHP + " / " + maxHP;
    }

    public void SetEnemyHUD(BattleUnit unit)
    {
        _enemyNameText.text = unit._unitName;
    }
}
