using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{

    [Header("InteractVariables")]
    [SerializeField] private float _interactRange;
    [SerializeField] private LayerMask _interactMask;
    public bool _canInteract = true;

    [SerializeField] private Transform _interactPoint;
    [SerializeField] private Animator _interactCursorAnim;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_canInteract)
        {
            RaycastHit hit;
            if(Physics.Raycast(_interactPoint.position, transform.forward, out hit, _interactRange, _interactMask))
            {
                if(hit.transform.GetComponent<Interactable>() != null)
                {
                    _interactCursorAnim.SetBool("active", true);
                    
                    if (Input.GetButtonDown("Interact"))
                    {
                        hit.transform.GetComponent<Interactable>().Interact();
                        _interactCursorAnim.SetTrigger("click");
                    }
                }
                else
                {
                    _interactCursorAnim.SetBool("active", false);
                }
            }
            else
            {
                _interactCursorAnim.SetBool("active", false);
            }
        }
        else
        {
            _interactCursorAnim.SetBool("active", false);
        }
    }
}
