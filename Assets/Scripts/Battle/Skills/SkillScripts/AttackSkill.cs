using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Skills/Attack")]
public class AttackSkill : TargetSkill
{
    [Header("Damage Fields")]
    public int _damage;
    
    
    public override bool Use(BattleUnit caster, BattleUnit target)
    {
        base.Use(caster, target);

        return true;
    }

    public override void Hit(BattleUnit caster, BattleUnit target)
    {
        base.Hit(caster, target);
        target.TakeDamage(_damage);
    }

    public override void Miss(BattleUnit caster)
    {
        base.Miss(caster);
    }
    
}
