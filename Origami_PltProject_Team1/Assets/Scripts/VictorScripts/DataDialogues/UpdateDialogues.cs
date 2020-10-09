using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DataSheetDialogueTypes;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "New DialoguesSheet", menuName = "DIaloguesSheet")]
public class UpdateDialogues : ScriptableObject
{
    public Feuille1 sheettest;
    public List<Dialogue> dialogues;
   // public List<CardScriptableObject> cardsPlace;
    



    public void CreateCards()
    {
        foreach (DataSheetDialogueTypes.Feuille1 dialogue in DataSheetDialogue.feuille1)
        {
            bool create = true;
            for (int i = 0; i < dialogues.Count; i++)
            {
                
                Debug.Log("Excel : " + dialogue.name);
                Debug.Log("Unity : " + dialogues[i].chrName);
                if (dialogue.name+"_" + dialogue.iD == dialogues[i].chrName+"_" + dialogue.iD)
                {
                    create = false;
                    break;
                }
            }
            if (create)
            {
                if (!Directory.Exists("Assets/Prefab/Dialogues/" + dialogue.eventName))
                {
                    //if it doesn't, create it
                    Directory.CreateDirectory("Assets/Prefab/Dialogues/" + dialogue.eventName);
                }
                Dialogue asset = ScriptableObject.CreateInstance<Dialogue>();

#if UNITY_EDITOR

                AssetDatabase.CreateAsset(asset, "Assets/Prefab/Dialogues/" + dialogue.eventName + "/" + dialogue.name +"_"+dialogue.iD+ ".asset");
                AssetDatabase.SaveAssets();

                EditorUtility.FocusProjectWindow();

                Selection.activeObject = asset;
#endif
                dialogues.Add(asset);
                if (dialogue.iD != dialogues.Count - 1)
                {
                    dialogues.Insert(dialogue.iD, asset);
                }
            }



        }
        Update();
#if UNITY_EDITOR
        EditorUtility.SetDirty(this);
#endif
    }

    public void Update()
    {
        //cardsPlace = cards;
        //cards.Clear();

        foreach (DataSheetDialogueTypes.Feuille1 dialogue in DataSheetDialogue.feuille1)
        {

            bool update = false;
            Dialogue change = null;
            
            for (int i = 0; i < dialogues.Count; i++)
            {
                if (dialogue.name + "_" + dialogue.iD == dialogues[i].name)
                {
                    update = true;
                    change = dialogues[i];
                    break;
                }
            }
            if (update)
            {
                

                change.chrName = dialogue.name;
                change.sentence = dialogue.sentence;
                
               







            }
#if UNITY_EDITOR
                EditorUtility.SetDirty(change);
#endif
            }
        }
    


    

  

    
}
#if UNITY_EDITOR
[CustomEditor(typeof(UpdateDialogues))]
public class CardsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        GUIStyle myStyleBold = new GUIStyle();
        myStyleBold.fontStyle = FontStyle.Bold;

        DrawDefaultInspector();

        UpdateDialogues script = (UpdateDialogues)target;

        GUI.backgroundColor = Color.white;

        if (GUILayout.Button("Update Valeurs"))
        {
            script.Update();
        }
        if (GUILayout.Button("Update Cards"))
        {
            script.CreateCards();
        }

       
       
    }
}
#endif