using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{

    public enum BattleState {START, PLAYERTURN, ENEMYTURN, WON, LOST }
    public BattleState _state;

    [Header("Spawning")]
    public GameObject _playerPref; //just one for prototype - replace with array later for multiple enemies!
    public GameObject _enemyPref;
    public Transform _playerSpawnPoint;
    public Transform _enemySpawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        _state = BattleState.START;
    }

    private void InitializeBattle()
    {
        GameObject newPlayer = Instantiate(_playerPref, _playerSpawnPoint);
        GameObject newnEnemy = Instantiate(_enemyPref, _enemySpawnPoint);
    }
}
