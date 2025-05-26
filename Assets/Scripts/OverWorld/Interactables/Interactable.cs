using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [Header("Dialogue Variables")]
    [SerializeField] public bool _talk = true;
    [SerializeField] public bool _talking = false;
    [SerializeField] public bool _canWalkAway;
    [SerializeField] public bool _timed = false;
    [SerializeField] public float _dialogueTimer;
    [SerializeField] public float _currentDialogueTime = 0;
    [SerializeField] public float _dialogueDistance;
    [SerializeField] public float _currentDistance;
    [SerializeField] public DialogueManager _manager;
    [SerializeField] public Dialogue _dialogue;
    [SerializeField] public Transform _player;
    [SerializeField] public bool _startedTalking = false;

    [Header("Sound Variables")]
    [SerializeField] public bool _playSound = false;
    [SerializeField] public AudioClip _soundClipDefault;
    [SerializeField] public AudioSource _soundSource;

    public virtual void Start()
    {
        if (_playSound)
        {
            if(TryGetComponent<AudioSource>(out AudioSource source))
            {
                _soundSource = source;
            }
            else
            {
                _playSound = false;
            }
        }

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

        if (_playSound)
        {
            PlaySound(_soundClipDefault);
        }
    }

    public virtual void PlaySound(AudioClip clip)
    {
        _soundSource.clip = clip;
        _soundSource.pitch = Random.Range(0.8f, 1.2f);
        _soundSource.Play();
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

[CustomEditor(typeof(Interactable), true)]
public class Interactable_Editor : Editor
{ 
    public override void OnInspectorGUI()
    {
        var script = (Interactable)target;
        script._talk = EditorGUILayout.Toggle("Can Talk", script._talk);

        if (script._talk)
        {
            EditorGUI.indentLevel++;
            script._canWalkAway = EditorGUILayout.Toggle("Can Walk Away", script._canWalkAway);
            if (script._canWalkAway)
            {
                script._dialogueDistance = EditorGUILayout.FloatField("Max Distance :", script._dialogueDistance);
            }

            script._timed = EditorGUILayout.Toggle("Timed", script._timed);
            if (script._timed)
            {
                script._dialogueTimer = EditorGUILayout.FloatField("Max Time :", script._dialogueTimer);
            }

            EditorGUI.indentLevel--;
        }

        script._playSound = EditorGUILayout.Toggle("Play Sound", script._playSound);
        if (script._playSound)
        {
            EditorGUI.indentLevel++;
            script._soundClipDefault = (AudioClip)EditorGUILayout.ObjectField("Interact Sound: ", script._soundClipDefault, typeof(AudioClip), false);
            EditorGUI.indentLevel--;
        }            
    }
}

