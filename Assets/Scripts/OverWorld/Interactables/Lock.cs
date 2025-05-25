using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : Interactable
{

    public string _lockValue;
    public bool _locked = true;

    [Header("Unlock Variables")]
    public Animator _lockAnim;
    public Dialogue _unLockDialogue;
    public GameObject[] _unlockDisappeared;

    private Dialogue _lockedDialogue;
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
        if (_locked)
        {
            TryUnlock();
        }
    }

    private void TryUnlock()
    {
        //check player inventory for a matching key, and if so, use
        KeyItem keyItem = Inventory.instance.CheckForKeys(_lockValue);
        if (keyItem != null)
        {
            keyItem.Use();
            Unlock();
            return;
        }

        //giving a message about being locked if no key is found
        base._dialogue = _lockedDialogue;
        base.Interact();
    }

    public void Unlock()
    {
        base._dialogue = _unLockDialogue;
        base.Interact();
        _locked = false;

    }
}
