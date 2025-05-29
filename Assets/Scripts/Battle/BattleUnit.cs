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
    public AttackSkill _baseAttack;
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
    
    public void TakeDamage(float damage)
    {
        _currentHP -= _damage;
        
    }

    public bool AttackSkill(BattleUnit target)
    {
       bool hit = _baseAttack.Use(this, target);
        return hit;
    }

    public bool CheckAlive()
    {
        if (_currentHP <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
