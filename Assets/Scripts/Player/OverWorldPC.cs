using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class OverWorldPC : MonoBehaviour
{
    private CharacterController _controller;
    private CapsuleCollider _collider;

    [Header("Movement Variables")]
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _runSpeed;
    [SerializeField] private float _turnSpeed;
    [SerializeField] private float _accelRate;
    private bool _walking;
    private bool _running;

    [Header("Grounded Variables")]
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _groundRange;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private float _fallSpeed;
    private bool _grounded = true;
    
    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<CapsuleCollider>();
        _controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        GroundUpdate();
        WalkUpdate();
    }

    private void GroundUpdate()
    {

    }

    private void WalkUpdate()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("vertical");

        
    }
}
