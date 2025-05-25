using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TestParams : MonoBehaviour
{
    [HideInInspector] public bool canRun;

    [Header("Hidden Run Fields")]
    [HideInInspector] public float runSpeed;
    [HideInInspector] public float turnRadius;
}


[CustomEditor(typeof(TestParams))]
public class Params_Editor : Editor 
{ 
    public override void OnInspectorGUI()
    {
        var script = (TestParams)target;
        script.canRun = EditorGUILayout.Toggle("Can Run", script.canRun);

        if (script.canRun == false) return;
        else
        {
            script.runSpeed = EditorGUILayout.FloatField("Run Speed : ", script.runSpeed);
            script.turnRadius = EditorGUILayout.FloatField("Turn Radius: ", script.turnRadius);
        }
    }

    [Header("Appear Variablels")]
    public bool _appearObjects;
    public GameObject[] _appearList;
    public bool _disappearObjects;
    public GameObject[] _disappearList;
}

