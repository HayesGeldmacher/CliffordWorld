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
    [SerializeField] private Transform _camera;
    private bool _walking;
    private bool _running;

    [Header("Grounded Variables")]
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _groundRange;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private float _fallSpeed;
    [SerializeField] private bool _grounded = true;

    [SerializeField] private float _turnSmoothTime = 0.1f;
    private float _turnSmoothVelocity;
    private Vector3 _velocity;


    //Creating unique global reference to OverWorldPC
    #region Singleton

    public static OverWorldPC instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of playercontroller present!! NOT GOOD!");
            return;
        }

        instance = this;
    }

    #endregion

    void Start()
    {
        //Assign essential components
        _collider = GetComponent<CapsuleCollider>();
        _controller = GetComponent<CharacterController>();

        //lock and hide that damn mouse cursor!
        //Cursor.lockState = CursorLockMode.Locked;
       // Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        GroundUpdate();
        WalkUpdate();
    }

    private void GroundUpdate()
    {
        //Check if player is standing or falling
        RaycastHit hit;
        if(Physics.Raycast(_groundCheck.position, Vector3.down, out hit, _groundRange, _groundMask))
        {
            _grounded = true;
        }
        else
        {
            _grounded = false;
        }

        if (_grounded)
        {
            _velocity.y = -10;
        }
        else
        {
            if (Mathf.Abs(_velocity.y) < 10)
            {
                _velocity.y += _fallSpeed * Time.deltaTime;
            }
        }

        _controller.Move(_velocity * Time.deltaTime);
    }

    private void WalkUpdate()
    {
        //Get raw keyboard + controller input
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(x, 0, z).normalized;

        //Get target speed 
        float speed;
        if (Input.GetButton("Run"))
        {
            _running = true;
            speed = _runSpeed;
        }
        else
        {
            speed = _walkSpeed;
        }

         //Check if input is detected before moving player
         float moveMag = direction.magnitude;
        if(moveMag > 0.1f)
        {
            //Establish direction player should be facing based on camera direction
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _camera.eulerAngles.y;
            
            //Smoothly rotate player in desired direction
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, _turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            //Get final direction and move player
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            _controller.Move(moveDir * speed * Time.deltaTime);
        }
    }
}
