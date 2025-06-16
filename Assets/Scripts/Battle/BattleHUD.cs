using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class BattleHUD : MonoBehaviour
{


    [SerializeField] private Animator _dialoguePointerAnim;
    [SerializeField] private Animator _actionPointerAnim;
    [SerializeField] private Animator _playerActionButtons;

    public void SetButtons(BattleUnit unit)
    {
        
    }

    public void ActivateDialoguePointer(bool activate)
    {
        _dialoguePointerAnim.SetBool("appear", activate);
    }
    
    public void ActivatePlayerActionButtons(bool activate)
    {
        _playerActionButtons.SetBool("appear", activate);
    }
}
