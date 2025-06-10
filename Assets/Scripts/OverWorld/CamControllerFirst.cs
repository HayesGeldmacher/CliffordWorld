using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControllerFirst : MonoBehaviour
{

    [Header("Control Fields")]
    [SerializeField] private float _sensitivityX;
    [SerializeField] private float _sensitivityY;

    [HideInInspector] public bool frozen = false;
    [SerializeField] private bool _canTurnX = true;
    [SerializeField] private bool _canTurnY = true;

    [SerializeField] private Transform _playerBody;

    private float rotationX = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (frozen) return;

        //getting mouse input
        float mouseX = Input.GetAxis("Mouse X") * _sensitivityX;
        float mouseY = Input.GetAxis("Mouse Y") * _sensitivityY;

        if (!_canTurnX)
        {
            mouseX = 0;
        }

        if (!_canTurnY)
        {
            mouseY = 0;
        }

        //transforming mouse to cam input
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        //rotate player
        _playerBody.Rotate(Vector3.up * mouseX);

        //rotate cam
        transform.localRotation = Quaternion.Euler(rotationX, 0, 0);

    }
}
