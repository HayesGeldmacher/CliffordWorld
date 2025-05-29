using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Skills/TargetSkill")]
public class TargetSkill : ScriptableObject
{

    public string _name;

    [TextArea(3, 10)]
    public string _description;

    [Header("Resource Cost")]
    public int _APCost;
    public int _HPCost;
    
    public bool _limited = false;
    public int _uses;
    public int _coolDownTurns;

    [Header("Chance")]
    public bool _random = false;
    public int _successChance;

   public virtual void Use(BattleUnit caster, BattleUnit target)
    {
        if (_random)
        {
            int randomInt = Random.Range(0, 100);
            if(_successChance >= randomInt)
            {
                Hit(caster, target);
            }
            else
            {
                Miss(caster);
            }
        }
        else
        {
            Hit(caster, target);
        }   
    } 

    public virtual void Hit(BattleUnit caster, BattleUnit target)
    {
        Debug.Log("HIT TARGET!");
    }

    public virtual void Miss(BattleUnit caster)
    {
        Debug.Log("MISSED TARGET!");
    }
    
}
