using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControllerFirst : MonoBehaviour
{

    [Header("Control Fields")]
    [SerializeField] private float _sensitivityX;
    [SerializeField] private float _sensitivityY;

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
        //getting mouse input
        float mouseX = Input.GetAxis("Mouse X") * _sensitivityX;
        float mouseY = Input.GetAxis("Mouse Y") * _sensitivityY;

        //transforming mouse to cam input
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        //rotate player
        _playerBody.Rotate(Vector3.up * mouseX);

        //rotate cam
        transform.localRotation = Quaternion.Euler(rotationX, 0, 0);

    }
}
