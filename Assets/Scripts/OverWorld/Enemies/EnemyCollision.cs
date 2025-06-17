using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{

    public Enemy _enemy;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameManagerOverWorld.instance.EnterCombatScene(_enemy);
        }
    }
}
