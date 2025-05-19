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
    [SerializeField] protected float _dialogueDistance;
    public DialogueManager _manager;
    public Dialogue _dialogue;
    public Transform _player;

    [SerializeField] private Animator _dialogueBoxAnim;
    public bool _startedTalking = false;

    [Header("Appear Variablels")]
    public bool _appearObjects;
    public GameObject[] _appearList;
    public bool _disappearObjects;
    public GameObject[] _disappearList;

    [Header("Sound Variables")]
    [SerializeField] protected bool _playSound = false;
    [SerializeField] protected AudioClip _soundList;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact()
    {
        Debug.Log("Interacted!");
    }
}
