using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class BattleHUD : MonoBehaviour
{


    [SerializeField] private Animator _battleDialogueAnim;

    public void SetButtons(BattleUnit unit)
    {
        
    }

    public void ActivateContinue(bool activate)
    {
        Debug.Log("Activated the damn animation!");
        _battleDialogueAnim.SetBool("appear", activate);
    }
    
}
