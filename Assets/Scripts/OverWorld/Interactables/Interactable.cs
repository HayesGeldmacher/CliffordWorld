using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public InteractableAppear _appearFields;


    [Header("Dialogue Variables")]
    [HideInInspector] public bool _talk = true;
    [HideInInspector] public bool _talking = false;
    [HideInInspector] public bool _canWalkAway;
    [HideInInspector] public bool _timed = false;
    [HideInInspector] public float _dialogueTimer;
    [HideInInspector] public float _currentDialogueTime = 0;
    [HideInInspector] public float _dialogueDistance;
    [HideInInspector] public float _currentDistance;
    [HideInInspector] public DialogueManager _manager;
    [HideInInspector] public Dialogue _dialogue;
    [HideInInspector] public Transform _player;
    [HideInInspector] public bool _startedTalking = false;

    

    [Header("Sound Variables")]
    [HideInInspector] protected bool _playSound = false;
    [HideInInspector] protected AudioClip _soundList;
    [HideInInspector] protected AudioSource _soundSource;
    [HideInInspector] protected int _currentSound = 0;

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

[CustomEditor(typeof(Interactable))]
public class Interactable_Editor : Editor
{ 
    public void Start()
    {
       // Interactable interactable = ScriptableObject.CreateInstance<Interactable>();
      //  SerializedObject serializedObject = new UnityEditor.SerializedObject(interactable);
       // SerializedProperty serializedArrayAppear = serializedObject.FindProperty("_appearList");

    }

    public override void OnInspectorGUI()
    {
        
        
        var script = (Interactable)target;
        script._talk = EditorGUILayout.Toggle("Can Talk", script._talk);

        if (script._talk)
        {
            script._talking = EditorGUILayout.Toggle("Is Talking", script._talking);
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
        }

      //  script._appearObjects = EditorGUILayout.Toggle("Appear Objects", script._appearObjects);
     //   GameObject[] newAppearObjects = script._appearList;
      //  if (script._appearObjects)
     //   {
        //    for(int i = 0; i < newAppearObjects.Length; i++)
         //   {
        //        GameObject obj = newAppearObjects[i];
       //         obj = EditorGUILayout.ObjectField(newAppearObjects[i], typeof(GameObject)) as GameObject;
       //     }
     //   }

        
       
    }




}

