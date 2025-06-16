using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerOverWorld : MonoBehaviour
{
    public bool paused = false;
    [SerializeField] private GameObject _puaseMenu;
    [SerializeField] private CamControllerFirst _camControlFirst;


    //Creating unique global reference to OverWorldPC
    #region Singleton

    public static GameManagerOverWorld instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of playercontroller present!! NOT GOOD!");
            return;
        }

        instance = this;
    }

    #endregion


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
            _camControlFirst.frozen = true;
        }
        else
        {
            Time.timeScale = 1f;
            _camControlFirst.frozen = false;
        }

        _puaseMenu.SetActive(freeze);

    }

    public void EnterCombatScene(Enemy enemy)
    {
        Debug.Log("entering overworld with " + enemy.name);
    }
}
