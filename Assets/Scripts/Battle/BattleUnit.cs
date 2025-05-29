using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUnit : MonoBehaviour
{
    [Header("Stat Fields")]
    public string _unitName;
    public int _unitLevel;
    public int _damage;
    public int _maxHP;
    public int _currentHP;
    public int _currentAP;
    public int _maxAP;

    [Header("Skills")]
    public TargetSkill _baseAttack;
    public TargetSkill[] _targetSkills;
    public NoTargetSkill[] _noTargetSkills;
    
    // Start is called beforde the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public bool TakeDamage(float damage)
    {
        _currentHP -= _damage;
        if(_currentHP <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void AttackSkill(BattleUnit target)
    {
        _baseAttack.Use(this, target);
    }
}
