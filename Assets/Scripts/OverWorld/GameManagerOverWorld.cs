using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class GameManagerOverWorld : MonoBehaviour
{
    public bool paused = false;
    [SerializeField] private GameObject _pauseMenu;
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

        //lock and hide that damn mouse cursor!
        ActivateCursor(false);
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

        _pauseMenu.SetActive(freeze);

    }

    public void ActivateCursor(bool activate)
    {
        if (activate)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }


    public void EnterCombatScene(Enemy enemy)
    {
        ActivateCursor(true);

        Debug.Log("entering overworld with " + enemy.name);
        MapStateData.instance.CreateEnemy(enemy);
        SceneManager.LoadScene("BattleTestScene1");
    }
}
