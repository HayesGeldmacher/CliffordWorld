using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapStateData : MonoBehaviour
{
    #region Singleton

    public static MapStateData instance;

    [Header("Persistent Fields")]
    public Vector3 _playerPosition;

    [Header("Battle Fields")]
    public Enemy _currentEnemy;
    public int _playerPartyCount;



    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of playercontroller present!! NOT GOOD!");
            return;
        }

        instance = this;

        DontDestroyOnLoad(gameObject);
    }

    #endregion


    public void CreateEnemy(Enemy enemy)
    {
        GameObject newEnemy = enemy.gameObject;
        DontDestroyOnLoad(newEnemy);
        newEnemy.transform.parent = this.transform;
        _currentEnemy = newEnemy.GetComponent<Enemy>(); 
        
    }
}
