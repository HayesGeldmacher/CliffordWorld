using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [Header("Dialogue Variables")]
    [SerializeField] protected bool _talk = false;
    public bool _talking = false;
    [SerializeField] protected bool _canWalkAway;
    [SerializeField] protected bool _timed = false;
    [SerializeField] protected float _dialogueTimer;
    [SerializeField] protected float _currentDialogueTime = 0;
    [SerializeField] protected float _dialogueDistance;
    private float _currentDistance;
    public DialogueManager _manager;
    public Dialogue _dialogue;
    public Transform _player;

    public bool _startedTalking = false;

    [Header("Appear Variablels")]
    public bool _appearObjects;
    public GameObject[] _appearList;
    public bool _disappearObjects;
    public GameObject[] _disappearList;

    [Header("Sound Variables")]
    [SerializeField] protected bool _playSound = false;
    [SerializeField] protected AudioClip _soundList;
    [SerializeField] protected AudioSource _soundSource;
    [SerializeField] protected int _currentSound = 0;

    public virtual void Start()
    {
        
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (_startedTalking)
        {

            if (_canWalkAway)
            {
                _currentDistance = Vector3.Distance(_player.position, transform.position);
                if (_currentDistance >= _dialogueDistance)
                {
                    EndDialogue();
                }

            }

            if (_timed)
            {
                _currentDialogueTime -= Time.deltaTime;
                if(_currentDialogueTime <= 0)
                {
                    EndDialogue();
                }
            }
        }
    }

    public virtual void Interact()
    {
       //set variables here to save assigned variables to stuff player doesnt touch
        
        if(_player == null && _canWalkAway)
        {
            _player = GameObject.Find("Player").transform;
        }

       if(_manager == null)
        {
            _manager = GameManager.instance.GetComponent<DialogueManager>();
        }


        if (_talk)
        {
            if (_timed)
            {
                _currentDialogueTime = _dialogueTimer;
            }
            TriggerDialogue();

        }
    }

    public virtual void TriggerDialogue()
    {

        Debug.Log("interacteed!");
        
        if (!_startedTalking)
        {
            Debug.Log("Started new dialogue interaction");
            _manager.StartDialogue(_dialogue, this);
            _startedTalking = true;
        }
        else
        {
            _manager.DisplayNextSentence();
        }
    }

    public virtual void EndDialogue()
    {
        _startedTalking = false;
        _manager.EndDialogue();
    }

    public virtual void CallEndDialogue()
    {
        _manager.CallTimerEnd(_dialogueTimer);
    }

    public virtual void PickupItem()
    {
        if (_talk)
        {
            _manager.CallTimerEnd(_dialogueTimer);
        }
        Destroy(gameObject);
    }
}
