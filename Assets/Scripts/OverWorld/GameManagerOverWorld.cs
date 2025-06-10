using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerOverWorld : MonoBehaviour
{
    public bool paused = false;
    [SerializeField] private GameObject _puaseMenu;
    
    // Start is called before the first frame update
    void Start()
    {
        paused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            paused = !paused;
            Pause(paused);
        }
    }

    private void Pause(bool freeze)
    {
        if (freeze)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1f;
        }

        _puaseMenu.SetActive(freeze);

    }
}
