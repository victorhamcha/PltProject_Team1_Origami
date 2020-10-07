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
                if (dialogue.name == dialogues[i].name)
                {
                    create = false;
                    break;
                }
            }
            if (create)
            {
                if (!Directory.Exists("Assets/Dialogues/" + dialogue.name + "/" + dialogue.name))
                {
                    //if it doesn't, create it
                    Directory.CreateDirectory("Assets/Dialogues/" + dialogue.name + "/" + dialogue.name);
                }
                Dialogue asset = ScriptableObject.CreateInstance<Dialogue>();

#if UNITY_EDITOR

                AssetDatabase.CreateAsset(asset, "Assets/Dialogues/" + dialogue.name + "/" + dialogue.name + "/" + dialogue.name + ".asset");
                AssetDatabase.SaveAssets();

                EditorUtility.FocusProjectWindow();

                Selection.activeObject = asset;
#endif
                //cards.Add(asset);
                //if(card.iD!=cards.Count-1)
                //{
                //dialogues.Insert(dialogue.iD, asset);
                //}
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
                if (dialogue.name == dialogues[i].name)
                {
                    update = true;
                    change = dialogues[i];
                    break;
                }
            }
            if (update)
            {
//                change._title = dialogue.titreCarte;
//#if UNITY_EDITOR
//                change._image = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/AssetsGraphiques/Cards_Game/" + dialogue.sprite + ".png", typeof(Sprite));
//#endif
//                change._description = dialogue.description;
//                change._placeEnum = (EnumPlaceGame._enumPlace)Enum.Parse(typeof(EnumPlaceGame._enumPlace), dialogue.place);
//                change._cardID = dialogue.iD;
                
               




                   

                   
                    
#if UNITY_EDITOR
                    //if (dialogue.hasVFX)
                    //{

                    //    change._specialVFX = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/AssetsGraphiques/" + dialogue.sprite + ".png", typeof(GameObject));

                    //}
                    
                  
#endif
                }
#if UNITY_EDITOR
                EditorUtility.SetDirty(change);
#endif
            }
        }
    


    

    public void VerifyThings()
    {
        //for(int i=0;i<dialogues.Count;i++)
        //{
        //    if(dialogues[i]._image==null)
        //    {
        //        Debug.Log("No sprite on : " + dialogues[i].name);
        //    }
        //    else if(dialogues[i]._image.name== "Card_Background")
        //    {
        //        Debug.Log("Sprite is background for : " + dialogues[i].name);
        //    }
        //}
    }

    //asset._image = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/AssetsGraphiques/" + card.imageName+".png", typeof(Sprite)); 
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

       
        if (GUILayout.Button("Verify cards"))
        {
           
        }
    }
}
#endif