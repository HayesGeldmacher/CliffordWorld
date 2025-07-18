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

    [SerializeField] private TMP_Text _dialogueText;

    [SerializeField] private Animator _blackAnim;
    public Transform[] _skillButtons;

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
       // _playerActionButtons.SetBool("appear", activate);
    }

    public void SetPartyMemberSkills(BattleUnit unit)
    {
       //clearing each button for a fresh start
        foreach(Transform button in _skillButtons)
        {
            button.gameObject.SetActive(false);
        }
        

        //activating and personalizing each button depending on number of unit skills
        int skillNum = 0;
        foreach (TargetSkill skill in unit._targetSkills)
        {
            //unit can only have 6 skills equipped at a time
            if (skillNum >= 5) return;

            _skillButtons[skillNum].gameObject.SetActive(true);
            TMP_Text skillName = _skillButtons[skillNum].GetChild(0).GetComponent<TMP_Text>();
            skillName.text = skill._name;

        }

        skillNum += 1;
    }

    public void SetDialogueBoxSkill()
    {

    }
}
