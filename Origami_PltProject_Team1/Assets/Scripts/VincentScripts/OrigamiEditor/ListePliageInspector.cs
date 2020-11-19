using UnityEngine;
using UnityEditor;
using UnityEditorInternal;


[CustomEditor(typeof(ListePliage))]
public class ListePliageEditor : Editor
{
    private ReorderableList _reorderableList = null;

    private ListePliage listePliage
    {
        get
        {
            return target as ListePliage;
        }
    }

    private void OnEnable()
    {
        _reorderableList = new ReorderableList(listePliage.GetListPliage(), typeof(Pliage), true, true, true, true);

        _reorderableList.drawHeaderCallback += DrawHeader;
        _reorderableList.drawElementCallback += DrawElement;

        _reorderableList.onAddCallback += AddItem;
        _reorderableList.onRemoveCallback += RemoveItem;
    }

    private void OnDisable()
    {
        _reorderableList.drawHeaderCallback -= DrawHeader;
        _reorderableList.drawElementCallback -= DrawElement;

        _reorderableList.onAddCallback -= AddItem;
        _reorderableList.onRemoveCallback -= RemoveItem;
    }

    private void DrawHeader(Rect rect)
    {
        GUI.Label(rect, "Our fancy reorderable list");
    }

    private void DrawElement(Rect rect, int index, bool active, bool focused)
    {
        Debug.Log(active);
        Pliage item = listePliage.GetListPliage()[index];
        EditorGUI.BeginChangeCheck();
        EditorGUI.LabelField(rect, "Pliage" + index);

        rect.x += 50;
        rect.y += 10;
        item.isConfirmationPliage = EditorGUI.Toggle(new Rect(rect.x, rect.y, 18, rect.height), item.isConfirmationPliage);
        //item.stringvalue = EditorGUI.TextField(new Rect(rect.x + 18, rect.y, rect.width - 18, rect.height), item.stringvalue);
        if (EditorGUI.EndChangeCheck())
        {
            EditorUtility.SetDirty(target);
        }
    }

    private void AddItem(ReorderableList list)
    {
        listePliage.GetListPliage().Add(new Pliage());

        EditorUtility.SetDirty(target);
    }

    private void RemoveItem(ReorderableList list)
    {
        listePliage.GetListPliage().RemoveAt(list.index);

        EditorUtility.SetDirty(target);
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        /*            serializedObject.Update();

                    // Actually draw the list in the inspector
                    _reorderableList.DoLayoutList();

                    serializedObject.ApplyModifiedProperties();*/
    }

}


