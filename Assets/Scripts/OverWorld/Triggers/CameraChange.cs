using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CameraStateHolder;

public class CameraChange : MonoBehaviour
{
    public CameraState state;
    private bool _hasEntered = false;
    [SerializeField] private Transform _camPos;

    [Header("Teleport Fields")]
    [SerializeField] private bool _teleport;
    [SerializeField] private Transform _teleportPos;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!_hasEntered)
            {
                _hasEntered = true;

                if (state == CameraState.FIXED)
                {
                    if (_camPos != null)
                    {
                        CameraManager.instance.MoveFixed(_camPos);
                    }
                }

                if (_teleport)
                {
                    if(_teleportPos != null)
                    {
                        CameraManager.instance.TeleportPlayer(_teleportPos);
                    }
                }

                CameraManager.instance.TriggerCamChange(state);
             
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
        if(other.tag == "Player")
        {
            _hasEntered = false;
        }
    }
}
