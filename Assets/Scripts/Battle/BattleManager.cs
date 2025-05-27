using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleManager : MonoBehaviour
{

    public enum BattleState {START, PLAYERTURN, ENEMYTURN, WON, LOST, WAIT }
    public BattleState _state;
    public BattleState _nextState;

    [Header("Spawning")]
    public GameObject _playerPref; //just one for prototype - replace with array later for multiple enemies!
    public GameObject _enemyPref;
    public Transform[] _playerSpawnPoints;
    public Transform _enemySpawnPoint;

    private BattleUnit _playerUnit;
    private BattleUnit _enemyUnit;

    public TMP_Text _enemyNameText;
    public TMP_Text _playerNameText;

    public TMP_Text _dialogueText;

    public BattleUnitHUD _playerHUD;
    public BattleUnitHUD _enemyHUD;

    public GameObject _actionSelections;

    public BattleUnit[] _playerUnits;
    public BattleUnit[] _enemyUnits;

    
    // Start is called before the first frame update
    void Start()
    {
        _state = BattleState.START;
        StartCoroutine(InitializeBattle());

    }

    private IEnumerator InitializeBattle()
    {
         GameObject newPlayer = Instantiate(_playerPref, _playerSpawnPoints[0]);
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

    private void Update()
    {

        if(_state == BattleState.WAIT)
        {
            if (Input.GetButtonDown("Interact"))
            {
                StartCoroutine(EnterState(_nextState));
                Debug.Log("FUCKYEAHBOY!");
            }
        }
    }

    private IEnumerator EnterState(BattleState newState)
    {
        
        switch (newState)
        {
            case BattleState.PLAYERTURN:
                _state = BattleState.PLAYERTURN;
                StartCoroutine(PlayerTurn());
                break;

            case BattleState.ENEMYTURN:
                _state = BattleState.ENEMYTURN;
                StartCoroutine(EnemyTurn());
                break;

            case BattleState.WON:
                _state = BattleState.WON;
                StartCoroutine(BattleEnd());
                break;

            case BattleState.LOST:
                _state = BattleState.LOST;
                StartCoroutine(BattleEnd());
                break;

            case BattleState.WAIT:
                _state = BattleState.WAIT;
                StartCoroutine(Wait());
                break;
            
        }

        yield return new WaitForSeconds(0.1f);
    }

    private IEnumerator Wait()
    {
        Debug.Log("waiting");
        yield return new WaitForSeconds(0.1f);
    }

    private IEnumerator PlayerTurn()
    {
        _actionSelections.SetActive(true);
        //this is where we determine who is currently active!
        yield return new WaitForSeconds(0.1f);
    }

    private IEnumerator BattleEnd()
    {
        yield return new WaitForSeconds(0.1f);

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
        yield return new WaitForSeconds(0.1f);
        _dialogueText.text = _enemyUnit._unitName + " lashes out at Clifford!";
        Debug.Log("attacked the enemy!");

        float attackDamage = _enemyUnit._damage;
        bool isDead = _playerUnit.TakeDamage(attackDamage);
        _dialogueText.text =  _enemyUnit._unitName + " attacked Clifford for " + attackDamage + " damage!";

        yield return new WaitForSeconds(1f);
        if (isDead)
        {
            _state = BattleState.LOST;
            StartCoroutine(BattleEnd());
        }
        else
        {
            _nextState = BattleState.PLAYERTURN;
            _state = BattleState.WAIT;
        }
    }

    public void OnAttackButton()
    {
        if (_state != BattleState.PLAYERTURN) return;
        _actionSelections.SetActive(false);
        StartCoroutine(PlayerAttack());

    }

    private IEnumerator PlayerAttack()
    {
        //damage the enemy
        yield return new WaitForSeconds(0.1f);
        Debug.Log("attacked the enemy!");

        float attackDamage = _playerUnit._damage;
        bool isDead = _enemyUnit.TakeDamage(attackDamage);
        _dialogueText.text = "Clifford attacked " + _enemyUnit._unitName + " for " + attackDamage + " damage!";

        yield return new WaitForSeconds(1f);

;        if (isDead)
        {
            _state = BattleState.WON;
            StartCoroutine(BattleEnd());
        }
        else
        {
            _nextState = BattleState.ENEMYTURN;
            _state = BattleState.WAIT;
        }
    }

    private void RemoveActionsHUD()
    {
        _actionSelections.SetActive(false);
    }
}
