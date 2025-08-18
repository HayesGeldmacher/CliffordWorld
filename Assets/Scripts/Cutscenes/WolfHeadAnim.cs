using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfHeadAnim : MonoBehaviour
{
    [SerializeField] public CallAnim _callAnim;
    [SerializeField] private Animator _anim;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _anim = transform.GetComponent<Animator>();
        StartCoroutine(BeginScene());
    }

    private IEnumerator BeginScene()
    {
        yield return new WaitForSeconds(1f);
        _anim.SetTrigger("bite");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CallAnimation()
    {
        _callAnim.CallToAnim();
    }
}
