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

    [SerializeField] private Animator _blackAnim;

    private void Awake()
    {
        StartCoroutine(FadeBlackIntro());
    }

    private IEnumerator FadeBlackIntro()
    {
        _blackAnim.SetTrigger("instantBlack");
        yield return new WaitForSeconds(0.5f);
        _blackAnim.SetTrigger("out");
    }


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
