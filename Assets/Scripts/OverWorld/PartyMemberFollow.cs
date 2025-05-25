using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyMemberFollow : MonoBehaviour
{

    [SerializeField] private Transform _followPoint;
    [SerializeField] private float _speed;
    [SerializeField] private bool _following = true;
    [SerializeField] private Vector3 _targetPosition;
    [SerializeField] private Vector3 _currentPosition;
    
    // Update is called once per frame
    void Update()
    {
        //Checking if PM is far enough away to start following player
        float distance = Vector3.Distance(transform.position, _followPoint.position);
        if(distance > 0.1f)
        {
            _following = true;
        }
        else
        {
            _following = false;
        }


        //Smoothly lerp PM position to follow player movement
        if (_following)
        {
            _targetPosition = _followPoint.position;
            _currentPosition = transform.position;
            Vector3 newPosition = Vector3.Lerp(_currentPosition, _targetPosition, _speed * Time.deltaTime);

            transform.position = newPosition;
        }
    }
}
