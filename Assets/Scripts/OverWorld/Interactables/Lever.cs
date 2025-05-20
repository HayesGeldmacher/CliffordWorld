using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : Interactable
{
    //if this is true, lock the lever after interacting with once
    [SerializeField] private bool _interactOnce;

    //Animator controller for lever
    [SerializeField] private Animator _leverAnim;

    //Slots for additional anims (doors opening, et cetera)
    [SerializeField] private Animator[] _effectAnims;

    //Animator triggers for the above slots
    [SerializeField] private string[] _effectTriggersOn;
    [SerializeField] private string[] _effectTriggersOff;


    private bool _interacted = false;
    private bool _flipOn = true;

    [SerializeField] private AudioSource _leverSound;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

    public override void Interact()
    {
        base.Interact();
        if (!_interacted || !_interactOnce)
        {
            _interacted = true;
            LeverInteract();
        }

    }

    private void LeverInteract()
    {
        Debug.Log("leverFlipped!");
        _leverAnim.SetTrigger("flip");
        if(_effectAnims.Length > 0)
        {
            string[] triggerList;
            if (_flipOn)
            {
                triggerList = _effectTriggersOn;
            }
            else
            {
                triggerList = _effectTriggersOff;
            }

            int num = 0;
            foreach(Animator anim in _effectAnims)
            {
                anim.SetTrigger(triggerList[num]);
                num++;
            }
        }

        _flipOn = !_flipOn;

        if(_leverSound != null)
        {
            _leverSound.Play();
        }
    }
}
