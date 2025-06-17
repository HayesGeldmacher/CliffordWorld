using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [Header("Battle Fields")]
    public GameObject _arena;

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameManagerOverWorld.instance.EnterCombatScene(this);
        }
    }
}
