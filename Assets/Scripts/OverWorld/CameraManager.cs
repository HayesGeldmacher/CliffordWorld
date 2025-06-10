using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CameraStateHolder;

public class CameraManager : MonoBehaviour
{
    //Creating unique global reference to CameraManager
    #region Singleton

    public static CameraManager instance;

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

    public CameraState state;

    //Cam 0 = First
    //Cam 1 = Fixed
    //Cam 2 = Third
    [SerializeField] private Camera[] _playerCams;
    

    public void TriggerCamChange(CameraState newState)
    {
        switch (newState)
        {
            case CameraState.FIRST:
                state = CameraState.FIRST;
                EnterFirst();
                Debug.Log("ENTERED FIRST PERSON CAM");
                break;

            case CameraState.FIXED:
                state = CameraState.FIXED;
                EnterFixed();
                Debug.Log("ENTERED FIXED CAM");
                break;

            case CameraState.THIRD:
                state = CameraState.FIXED;
                EnterFixed();
                Debug.Log("ENTERED FIXED CAM");
                break;
        }

        OverWorldPC.instance.TriggerCamChange(state);

    }
    public void EnterFirst()
    {
        
        _playerCams[0].enabled = true;

        foreach(Camera cam in _playerCams)
        {
            if(cam != _playerCams[0])
            {
                cam.enabled = false;
            }
        }
    }
    
    public void EnterFixed()
    {
        _playerCams[1].enabled = true;

        foreach (Camera cam in _playerCams)
        {
            if (cam != _playerCams[1])
            {
                cam.enabled = false;
            }
        }
    }

    public void EnterThird()
    {
        _playerCams[2].enabled = true;

        foreach (Camera cam in _playerCams)
        {
            if (cam != _playerCams[2])
            {
                cam.enabled = false;
            }
        }
    }

}
