using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CameraStateHolder;

public class CameraChange : MonoBehaviour
{
    public CameraState state;
    private bool _hasEntered = false;

   

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!_hasEntered)
            {
                _hasEntered = true;
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
