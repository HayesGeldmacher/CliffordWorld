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

    public TMP_Text _dialogueText;

    public BattleUnitHUD _playerHUD;
    public BattleUnitHUD _enemyHUD;

    // Start is called before the first frame update
    void Start()
    {
        _state = BattleState.START;
        StartCoroutine(InitializeBattle());
    }

    private IEnumerator InitializeBattle()
    {
        GameObject newPlayer = Instantiate(_playerPref, _playerSpawnPoint);
        _playerUnit = newPlayer.GetComponent<BattleUnit>();
        _playerNameText.text = _playerUnit._unitName;

        GameObject newEnemy = Instantiate(_enemyPref, _enemySpawnPoint);
        _enemyUnit = newEnemy.GetComponent<BattleUnit>();
        _enemyNameText.text = _enemyUnit._unitName;

        _dialogueText.text = "A terrible presence emerges from the fog...";

        _playerHUD.SetHUD(_playerUnit);
        _playerHUD.SetHUDLimited(_enemyUnit);

        yield return new WaitForSeconds(2f);
        _state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    private void PlayerTurn()
    {
        _dialogueText.text = "It's the player's turn...";

    }

    private IEnumerator BattleEnd()
    {
        yield return new WaitForSeconds(1f);

        if (_state == BattleState.WON)
        {
            _dialogueText.text = "Clifford and friends were victorious!";
        }
        else if (_state == BattleState.LOST)
        {
            _dialogueText.text = "Clifford and friends were defeated...";
        }
    }

    private IEnumerator EnemyTurn()
    {
        //damage the player
        _dialogueText.text = _enemyUnit._unitName + " lashes out at Clifford!";
        yield return new WaitForSeconds(2f);
        Debug.Log("attacked the enemy!");

        float attackDamage = _enemyUnit._damage;
        bool isDead = _playerUnit.TakeDamage(attackDamage);
        _dialogueText.text = "Clifford attacked " + _enemyUnit._unitName + " for " + attackDamage + " damage!";


        if (isDead)
        {
            _state = BattleState.WON;
            StartCoroutine(BattleEnd());
        }
        else
        {
            _state = BattleState.LOST;
        }
    }

    public void OnAttackButton()
    {
        if (_state != BattleState.PLAYERTURN) return;
        StartCoroutine(PlayerAttack());

    }

    private IEnumerator PlayerAttack()
    {
        //damage the enemy
        yield return new WaitForSeconds(2f);
        Debug.Log("attacked the enemy!");

        float attackDamage = _playerUnit._damage;
        bool isDead = _enemyUnit.TakeDamage(attackDamage);
        _dialogueText.text = "Clifford attacked " + _enemyUnit._unitName + " for " + attackDamage + " damage!"; 


        if (isDead)
        {
            _state = BattleState.WON;
            StartCoroutine(BattleEnd());
        }
        else
        {
            _state = BattleState.LOST;
        }
    }

    
}
