using UnityEditor;
using Origami;

[CustomEditor(typeof(Pliage))]
public class PliageInspector : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        SerializedProperty spGoodPointSelection = serializedObject.FindProperty("goodPointSelection");
        EditorGUILayout.PropertyField(spGoodPointSelection);

        serializedObject.ApplyModifiedProperties();
    }
}
