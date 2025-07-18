using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleManager : MonoBehaviour
{

    public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST, WAIT }
    public BattleState _state;
    public BattleState _nextState;

    [Header("Spawning Fields")]
    public GameObject[] _partyMembers; //just one for prototype - replace with array later for multiple enemies!
    public GameObject _enemyPref;
    public Transform[] _playerSpawnPoints;
    public Transform[] _enemySpawnPoints;

    [Header("Arena Fields")]
    public GameObject _defaultArena;
    public Transform _arenaSpawnPoint;
    private ArenaDataStore _currentArena;


    public TMP_Text _enemyNameText;
    public TMP_Text _playerNameText;

    public TMP_Text _dialogueText;

    public BattleUnitHUD _playerHUD;
    public BattleUnitHUD _enemyHUD;

    public GameObject _actionSelections;
    public SkillManager _skillManager;


    public List<BattleUnit> _partyUnits = new List<BattleUnit>();

    [Header("Enemy Units")]
    public BattleUnit[] _enemyUnits;
    private BattleUnit _enemyUnit;


    [Header("PartyTurns")]
    public BattleUnit _currentPartyMember;
    public int _partyTracker;

    [Header("HUD Fields")]
    public BattleHUD _battleHUD;
    public BattleUnitHUD _partyHUDs;

    [Header("Timing Fields")]
    public float _maxTurnWait;
    public float _currentWaitTime;


    [Header("Admin Fields")]
    public string _nextScene;
    [SerializeField] private SceneManagement _sceneManagement;

    
    // Start is called before the first frame update
    void Start()
    {
        _state = BattleState.START;
        StartCoroutine(InitializeBattle());

    }

    private IEnumerator InitializeBattle()
    {

        //Initialize Background, theme, music, etc
        GameObject arena;
        if (MapStateData.instance != null)
        {
            Enemy enemy = MapStateData.instance._currentEnemy;
            if(enemy != null)
            {
                _defaultArena.SetActive(false);
                arena = Instantiate(enemy._arena, _arenaSpawnPoint);
            }
            else
            {
                arena = Instantiate(_defaultArena, _arenaSpawnPoint);
            }

        }
        else
        {
          arena = Instantiate(_defaultArena, _arenaSpawnPoint);
        }

        _currentArena = arena.GetComponent<ArenaDataStore>();
        _playerSpawnPoints = _currentArena._playerSpawns;
        _enemySpawnPoints = _currentArena._enemySpawns;

        //Spawning party members:
        int partySize = 0;
        Debug.Log("party size: " + partySize);
        foreach(GameObject member in _partyMembers)
        {
            GameObject newPlayer = Instantiate(_partyMembers[partySize], _playerSpawnPoints[partySize]);
            _partyUnits.Add(newPlayer.GetComponent<BattleUnit>());

            partySize++;

        }

        //Setting first party member to current PM:
        _currentPartyMember = _partyUnits[0];
        _playerNameText.text = _partyUnits[0]._unitName;

        //Spawning enemies
        GameObject newEnemy = Instantiate(_enemyPref, _enemySpawnPoints[0]);
        _enemyUnit = newEnemy.GetComponent<BattleUnit>();
        _enemyNameText.text = _enemyUnit._unitName;

        _dialogueText.text = "A terrible presence emerges from the fog...";

        //Setting up battle states
        _nextState = BattleState.PLAYERTURN;
        StartCoroutine(EnterState(BattleState.WAIT));
        yield return new WaitForSeconds(0.1f);
    }

    private void Update()
    {

        if(_state == BattleState.WAIT)
        {

            _currentWaitTime -= Time.deltaTime;
            if(_currentWaitTime <= 0)
            {
                 StartCoroutine(EnterState(_nextState));
                 _battleHUD.ActivateDialoguePointer(false);
                 Debug.Log("FUCKYEAHBOY!");
            }



            if (Input.GetButtonDown("Interact"))
            {
               // StartCoroutine(EnterState(_nextState));
              //  _battleHUD.ActivateDialoguePointer(false);
              //  Debug.Log("FUCKYEAHBOY!");
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
        _currentWaitTime = _maxTurnWait;
       // _battleHUD.ActivateDialoguePointer(true);
        yield return new WaitForSeconds(0.1f);
    }

    private IEnumerator PlayerTurn()
    {
        _battleHUD.ActivatePlayerActionButtons(true);
        _currentPartyMember = _partyUnits[_partyTracker];
        _playerNameText.text = _currentPartyMember._unitName;
        //this is where we determine who is currently active!
        _skillManager.SetSkillButtons(_currentPartyMember);
        _battleHUD.SetPartyMemberSkills(_currentPartyMember);
        _dialogueText.text = _currentPartyMember._unitName + " is ready to act...";
        yield return new WaitForSeconds(0.1f);
    }

    private IEnumerator BattleEnd()
    {
        yield return new WaitForSeconds(0.1f);

        if (_state == BattleState.WON)
        {
            StartCoroutine(BattleVictory());
        }
        else if (_state == BattleState.LOST)
        {
            _dialogueText.text = "Clifford and friends were defeated...";
        }
    }

   
    private IEnumerator BattleVictory()
    {
        _dialogueText.text = "Clifford and friends were victorious!";
        yield return new WaitForSeconds(5);
        _sceneManagement.CallLoadScene(_nextScene);
    }


    private void RemoveActionsHUD()
    {
        _actionSelections.SetActive(false);
    }

    private IEnumerator PlayerTargetSkill(TargetSkill skill)
    {
        yield return new WaitForSeconds(0.25f);

        //trigger animation and sound here
        bool hit = _currentPartyMember.CastTargetSkill(_enemyUnit, skill);
        if (hit)
        {
            _dialogueText.text = _currentPartyMember._unitName + " cast " + skill._name + " at " + _enemyUnit._unitName + " !";

            bool isDead = _enemyUnit.CheckAlive();

            if (isDead)
            {
                yield return new WaitForSeconds(1f);
                _state = BattleState.WON;
                StartCoroutine(BattleEnd());
            }
            else
            {
                yield return new WaitForSeconds(0.25f);
                EndPlayerTurn();
            }
        }
        else
        {
            _dialogueText.text = _currentPartyMember._unitName + " missed!";
            EndPlayerTurn();
        }

        yield return new WaitForSeconds(0.1f);
    }

    private IEnumerator PlayerAttack()
    {
        yield return new WaitForSeconds(0.25f);
        AttackSkill attack = _currentPartyMember._baseAttack;
        if (attack == null) yield break;
        
        //trigger animation and sound here
        bool hit = _currentPartyMember.AttackSkill(_enemyUnit);
        if (hit)
        {
            _dialogueText.text = _currentPartyMember._unitName + " hit " + _enemyUnit._unitName + " for " + attack._damage + " damage!";

            bool isDead = _enemyUnit.CheckAlive();

            if (isDead)
            {
                yield return new WaitForSeconds(1f);
                _state = BattleState.WON;
                StartCoroutine(BattleEnd());
            }
            else
            {
                yield return new WaitForSeconds(0.25f);
                EndPlayerTurn();
            }
        }
        else
        {
            _dialogueText.text = _currentPartyMember._unitName + " missed!";
            EndPlayerTurn();
        }

        yield return new WaitForSeconds(0.1f);
    }

    private void EndPlayerTurn()
    {

        _partyTracker++;

        if (_partyTracker >= _partyUnits.Count)
        {
            _nextState = BattleState.ENEMYTURN;
            _partyTracker = 0;
        }
        else
        {
            _nextState = BattleState.PLAYERTURN;
        }

        StartCoroutine(EnterState(BattleState.WAIT));
    }

    private IEnumerator EnemyTurn()
    {
        //damage the player
        yield return new WaitForSeconds(0.1f);

        AttackSkill attack = _enemyUnit._baseAttack;
        if (attack == null) yield break;

        //trigger animation and sound here
        BattleUnit target = _partyUnits[Random.Range(0, _partyUnits.Count)];
        bool hit = _enemyUnit.AttackSkill(target);
        if (hit)
        {
            _dialogueText.text = _enemyUnit._unitName + " lashes out at " + target._unitName + " for " + attack._damage + " damage!";

            bool isDead = target.CheckAlive();

            if (isDead)
            {
                yield return new WaitForSeconds(1f);
                _state = BattleState.LOST;
                StartCoroutine(BattleEnd());
            }
            else
            {
                yield return new WaitForSeconds(0.25f);
                _nextState = BattleState.PLAYERTURN;
                StartCoroutine(EnterState(BattleState.WAIT));
            }
        }
        else
        {
            _dialogueText.text = _enemyUnit._unitName + " missed!";
            _nextState = BattleState.PLAYERTURN;
            StartCoroutine(EnterState(BattleState.WAIT));
        }
    }

    //ALL BUTTON LOGIC BELOW:
    //Button presses are handled in battlemanager, but logic happens in BattleUnit
    public void OnAttackButton()
    {
        if (_state != BattleState.PLAYERTURN) return;
        StartCoroutine(PlayerAttack());
        _battleHUD.ActivatePlayerActionButtons(false);

    }

    public void OnSkillButton(TargetSkill skill)
    {
        //check if skill is targeted or non-targeted

        //battleunit.nontargetskill(nontargetskill)
        //battleunit.targettskill(targetskill)

        //open up the skills folder!
        Debug.Log("ACTIVATED SKILLS BUTTON WHOOO!!!");

        if (_state != BattleState.PLAYERTURN) return;


        StartCoroutine(PlayerTargetSkill(skill));
        _battleHUD.ActivatePlayerActionButtons(false);

    }

    private void AssignSkillButtons()
    {
        
    }
}
