using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class BattleHUD : MonoBehaviour
{


    [SerializeField] private Animator _dialoguePointerAnim;
    [SerializeField] private Animator _actionPointerAnim;

    public void SetButtons(BattleUnit unit)
    {
        
    }

    public void ActivateDialoguePointer(bool activate)
    {
        _dialoguePointerAnim.SetBool("appear", activate);
    }
    
    public void ActivateActionPointer(bool activate)
    {
        _actionPointerAnim.SetBool("appear", activate);
    }
}
