using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "New Achievement", menuName = "Achievements")]
public class Achievements : ScriptableObject
{
    public Sprite _sprtSucces;
    public string nameSucces;
    public string descriptionSucces;
    public bool _isLock;
}

#if UNITY_EDITOR
[CustomEditor(typeof(Achievements))]
public class Achievements_Editor : Editor
{


    int _lineSize;

    public override void OnInspectorGUI()
    {
        GUIStyle myStyleBold = new GUIStyle();
        myStyleBold.fontStyle = FontStyle.Bold;

        DrawDefaultInspector();

        Achievements script = (Achievements)target;



        if (GUILayout.Button("Update Valeurs"))
        {
#if UNITY_EDITOR
            EditorUtility.SetDirty(script);
#endif
        }


    }
}
#endif
