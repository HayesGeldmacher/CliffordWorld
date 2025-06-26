using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyMemberAnim : MonoBehaviour
{

    private Animator _anim;
    public bool _running = false;
    public bool _walking = false;
    private bool _walkingLastFrame = false;
    [SerializeField] private OverWorldPC _overWorldPC;

    [Header("Idle Fields")]
    [SerializeField] private float _idleCoolDown;
    [SerializeField] private float _currentIdleTime;
    
    // Start is called before the first frame update
    void Start()
    {
        _anim = transform.GetComponent<Animator>();

        _currentIdleTime = _idleCoolDown;
    }

    // Update is called once per frame
    void Update()
    {
        _walking = _overWorldPC._walking;
        _running = _overWorldPC._running;
        _anim.SetBool("walking", _walking);
        _anim.SetBool("running", _running);


        if (!_walking)
        {
            _currentIdleTime -= Time.deltaTime;
            if(_currentIdleTime <= 0)
            {
                _anim.SetTrigger("idle");
                _currentIdleTime = _idleCoolDown;
            }
        }
        else
        {
            if (!_walkingLastFrame)
            {
                _currentIdleTime = _idleCoolDown;
            }
        }

            _walkingLastFrame = _overWorldPC._walking;
    }
}
