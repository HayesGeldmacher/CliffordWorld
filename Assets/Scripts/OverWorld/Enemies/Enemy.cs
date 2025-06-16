using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("HIT ENEMY~!");
            GameManagerOverWorld.instance.EnterCombatScene(this);
        }
    }
}
