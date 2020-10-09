using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue")]
public class Dialogue : ScriptableObject
{
    [Header("VISUEL")]
    [TextArea(1, 1)] public string chrName;
    
    
    [Space(10)]
    [TextArea(5, 1)] public string sentence;

    [Space(10)]
    public Color chrColor;


  
}

#if UNITY_EDITOR
[CustomEditor(typeof(Dialogue))]
public class CardScriptableObject_Editor : Editor
{
  

    int _lineSize;

    public override void OnInspectorGUI()
    {
        GUIStyle myStyleBold = new GUIStyle();
        myStyleBold.fontStyle = FontStyle.Bold;

        DrawDefaultInspector();

        Dialogue script = (Dialogue)target;

        

        if (GUILayout.Button("Update Valeurs"))
        {
#if UNITY_EDITOR
            EditorUtility.SetDirty(script);
#endif
        }


    }
}
#endif
