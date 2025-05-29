using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Skills/NoTarget/NoTargetSkill")]
public class NoTargetSkill : ScriptableObject
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


    public virtual void Use()
    {


    }
}
