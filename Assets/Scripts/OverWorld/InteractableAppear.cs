using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableAppear : ScriptableObject
{
    [Header("Appear Variablels")]
    public bool _appearObjects;
    public GameObject[] _appearList;
    public bool _disappearObjects;
    public GameObject[] _disappearList;
}
