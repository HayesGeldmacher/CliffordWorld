using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Skills/NoTarget/Escape")]
public class EscapeSkill : NoTargetSkill
{
  
    public override void Use()
    {
        Debug.Log("Escaping!...");
    }
}
