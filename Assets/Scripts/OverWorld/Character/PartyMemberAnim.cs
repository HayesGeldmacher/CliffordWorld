using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyMemberAnim : MonoBehaviour
{

    private Animator _anim;
    public bool _running = false;
    [SerializeField] private OverWorldPC _overWorldPC;
    
    // Start is called before the first frame update
    void Start()
    {
        _anim = transform.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        _running = _overWorldPC._walking;
        _anim.SetBool("running", _running);
    }
}
