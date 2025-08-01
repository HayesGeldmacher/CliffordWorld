using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallAnim : MonoBehaviour
{
    [SerializeField] private string _animString;
    [SerializeField] private Animator _anim;

    private void Start()
    {
        _anim = transform.GetComponent<Animator>();
    }

    public void CallToAnim()
    {
        if(_anim != null)
        {
            _anim.SetTrigger(_animString);
        }
    }
}
