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
    public int _maxAP;
    public int _maxStress;
    public int _currentHP;
    public int _currentAP;
    public int _currentStress;

    [Header("Skills")]
    public AttackSkill _baseAttack;
    public TargetSkill[] _targetSkills;
    public NoTargetSkill[] _noTargetSkills;

    [Header("Hud")]
    public BattleUnitHUD _hud;

    [Header("Animation")]
    public Animator _anim;
    private bool _animates = false;
    
    // Start is called before the first frame update
    void Start()
    {
        CheckForAnimation();
    }

    private void CheckForAnimation()
    {
        if (transform.childCount > 0)
        {
            Transform child = transform.GetChild(0);
            if(child.TryGetComponent<Animator>(out Animator animate))
            {
                _anim = animate;
                _animates = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void TakeDamage(float damage)
    {
        _currentHP -= _damage;
        bool isDead = CheckAlive();
        if (isDead == true)
        {
            Die();
        }
        else
        {
            TakeDamageAnim();
        } 
    }

    public bool AttackSkill(BattleUnit target)
    {
        if (_animates)
        {
            _anim.SetTrigger("attack");
        }
        
        bool hit = _baseAttack.Use(this, target);
        return hit;
    }

    public bool CastTargetSkill(BattleUnit target, TargetSkill skill)
    {
        if (_animates)
        {
            //change to unique anim at some point for each  skill
            _anim.SetTrigger("attack");
        }

        bool hit = skill.Use(this, target);
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

    public void UpdateStats()
    {
        _hud.SetHUD(this);
    }

    public void UpdateEnemyStats()
    {
        _hud.SetEnemyHUD(this);
    }

    public void Die()
    {
        if (!_animates) return;
        _anim.SetTrigger("die");
    }
    
    public void TakeDamageAnim()
    {
        if (!_animates) return;
        _anim.SetTrigger("hurt");
    }
}
