using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CameraStateHolder;

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

    private float x;
    private float z;
    private Vector3 direction;
    private float speed;

    [Header("Camera Fields")]
    [SerializeField] private Transform _cameraFirst;
    [SerializeField] private Transform _cameraFixed;
    [SerializeField] private Transform _cameraThird;
    [SerializeField] private CamControllerFirst _camControlFirst;

    [Header("Grounded Variables")]
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _groundRange;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private float _fallSpeed;
    [SerializeField] private bool _grounded = true;

    [SerializeField] private float _turnSmoothTime = 0.1f;
    private float _turnSmoothVelocity;
    private Vector3 _velocity;

    [Header("Camera State")]
    public CameraState _camState;


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
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        _camState = CameraState.THIRD;
    }

    public void TriggerCamChange(CameraState newState)
    {
        switch (newState)
        {
            case CameraState.FIRST:
                _camState = CameraState.FIRST;
                EnterFirst();
                Debug.Log("ENTERED FIRST PERSON CAM");
                break;

            case CameraState.FIXED:
                _camState = CameraState.FIXED;
                EnterFixed();
                Debug.Log("ENTERED FIXED CAM");
                break;

            case CameraState.THIRD:
                _camState = CameraState.THIRD;
                EnterFixed();
                Debug.Log("ENTERED FIXED CAM");
                break;
        }

    }

    private void EnterFirst()
    {
        Debug.Log("Controller entered first state!");
        _camControlFirst.enabled = true;
    }

    private void EnterFixed()
    {
        Debug.Log("Controller entered fixed state!");
        _camControlFirst.enabled = false;
    }

    private void EnterThird()
    {
        Debug.Log("Controller entered Third state!");
        _camControlFirst.enabled = false;
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
        x = Input.GetAxisRaw("Horizontal");
        z = Input.GetAxisRaw("Vertical");

        //Get target speed 
        if (Input.GetButton("Run"))
        {
            _running = true;
            speed = _runSpeed;
        }
        else
        {
            speed = _walkSpeed;
        }

        //Check camera state before we calculate move direction
        if (_camState == CameraState.FIRST)
        {
            WalkUpdateFirst();
        }
        else
        {
            WalkUpdateThird();
        }
    }
    
    
    private void WalkUpdateFirst()
    {
        //Stores that input in a variable to be used later in function
        direction = (transform.right * x + transform.forward * z);
        _controller.Move(direction * speed * Time.deltaTime);

    }

    private void WalkUpdateThird()
    {
        Transform cam;

        if(_camState == CameraState.THIRD)
        {
            cam = _cameraThird;
        }
        else
        {
            cam = _cameraFixed;
        }

        direction = new Vector3(x, 0, z).normalized;

        //Check if input is detected before moving player
        float moveMag = direction.magnitude;
        if(moveMag > 0.1f)
        {
            //Establish direction player should be facing based on camera direction
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            
            //Smoothly rotate player in desired direction
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, _turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            //Get final direction and move player
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            _controller.Move(moveDir * speed * Time.deltaTime);
        }
    }
}
