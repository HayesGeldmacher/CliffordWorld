using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleManager : MonoBehaviour
{

    public enum BattleState {START, PLAYERTURN, ENEMYTURN, WON, LOST }
    public BattleState _state;

    [Header("Spawning")]
    public GameObject _playerPref; //just one for prototype - replace with array later for multiple enemies!
    public GameObject _enemyPref;
    public Transform _playerSpawnPoint;
    public Transform _enemySpawnPoint;

    private BattleUnit _playerUnit;
    private BattleUnit _enemyUnit;

    public TMP_Text _enemyNameText;
    public TMP_Text _playerNameText;

    public TMP_Text _dialogueBox;

    public BattleUnitHUD _playerHUD;
    public BattleUnitHUD _enemyHUD;

    // Start is called before the first frame update
    void Start()
    {
        _state = BattleState.START;
        InitializeBattle();
    }

    private void InitializeBattle()
    {
        GameObject newPlayer = Instantiate(_playerPref, _playerSpawnPoint);
        _playerUnit = newPlayer.GetComponent<BattleUnit>();
        _playerNameText.text = _playerUnit._unitName;

        GameObject newEnemy = Instantiate(_enemyPref, _enemySpawnPoint);
        _enemyUnit = newEnemy.GetComponent<BattleUnit>();
        _enemyNameText.text = _enemyUnit._unitName;

        _playerHUD.SetHUD(_playerUnit);
        _playerHUD.SetHUDLimited(_enemyUnit);
    }
}
