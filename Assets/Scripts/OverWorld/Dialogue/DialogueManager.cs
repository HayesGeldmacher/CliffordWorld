using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    private Queue<string> _sentences;
    public TMP_Text _dialogueText;
    [SerializeField] private Interactable _currentTrigger;
    [SerializeField] private Animator _dialogueBoxAnim;

    public bool _talking = true;

    // Start is called before the first frame update
    void Start()
    {
        _sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue, Interactable trigger)
    {
        if(_currentTrigger != null)
        {
            _currentTrigger._startedTalking = false;
        }

        _talking = true;
        _currentTrigger = trigger;
        _dialogueBoxAnim.SetBool("active", true);
        _sentences.Clear();

        //uses FIFO to queue up each sentence in our public dialogue box
        foreach(string sentence in dialogue.sentences)
        {
            _sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    private void DisplayNextSentence()
    {
        Debug.Log("Continued Dialogue!");

        if(_sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        //Takes next-added sentence out of queue and loads it into text box
        string loadedText = _sentences.Dequeue();
        _dialogueText.text = loadedText;
        Debug.Log(loadedText);
    }

    private void EndDialogue()
    {
        Debug.Log("End of dialogue");
        _dialogueBoxAnim.SetBool("active", true);
        _dialogueText.text = "";

        if (_currentTrigger)
        {
            _currentTrigger._startedTalking = false;
            _currentTrigger = null;
        }

        _talking = false;
    }

    
}
