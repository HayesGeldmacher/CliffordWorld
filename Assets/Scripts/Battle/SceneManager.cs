using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneManagement : MonoBehaviour
{

    [Header("Animation Fields")]
    [SerializeField] private Animator _blackAnim;
    private bool _enteringScene = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadGameOver()
    {
        if (_enteringScene) return;
    }


    public void LoadMainMenu()
    {
        if (_enteringScene) return;
    }

    public void CallLoadScene(string sceneName)
    {
        if (_enteringScene) return;
        StartCoroutine(FadeLoadScene(sceneName));
    }

    private IEnumerator FadeLoadScene(string sceneName)
    {
        _enteringScene = true;
        _blackAnim.SetTrigger("in");
        yield return new WaitForSeconds(2f);
        LoadScene(sceneName);
    }
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
