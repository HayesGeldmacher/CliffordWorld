using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Skills/Attack")]
public class AttackSkill : TargetSkill
{
    [Header("Damage Fields")]
    public int _damage;
    
    
    public override void Use(BattleUnit caster, BattleUnit target)
    {
        base.Use(caster, target);
    }

    public override void Hit(BattleUnit caster, BattleUnit target)
    {
        base.Hit(caster, target);
    }

    public override void Miss(BattleUnit caster)
    {
        base.Miss(caster);
    }
    
}
