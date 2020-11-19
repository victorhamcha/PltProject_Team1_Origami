using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(Pliage))]
public class ListPliagePropertyDrawer : PropertyDrawer
{
    private float size = 500f;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        //float initHeight = position.height;
        position.height = EditorGUIUtility.singleLineHeight;
        EditorGUI.LabelField(position,new GUIContent("Pliage N"));

        Rect posObject = position;

        posObject.y += EditorGUIUtility.singleLineHeight * 1.3f;

        EditorGUI.PropertyField(posObject, property.FindPropertyRelative("goodPointSelection"));

        posObject.y += EditorGUIUtility.singleLineHeight * 1.3f;
        EditorGUI.PropertyField(posObject, property.FindPropertyRelative("endPointSelection"));

        posObject.y += EditorGUIUtility.singleLineHeight * 1.3f;
        EditorGUI.PropertyField(posObject, property.FindPropertyRelative("animToPlay"));

        posObject.y += EditorGUIUtility.singleLineHeight * 1.3f;
        EditorGUI.PropertyField(posObject, property.FindPropertyRelative("isConfirmationPliage"));

        posObject.y += EditorGUIUtility.singleLineHeight * 1.3f;
        EditorGUI.PropertyField(posObject, property.FindPropertyRelative("drawPointSelection"));

        posObject.y += EditorGUIUtility.singleLineHeight * 1.3f;
        EditorGUI.PropertyField(posObject, property.FindPropertyRelative("playBounce"));

        posObject.y += EditorGUIUtility.singleLineHeight * 1.3f;
        EditorGUI.PropertyField(posObject, property.FindPropertyRelative("offsetPlacementPliage"));

        posObject.y += EditorGUIUtility.singleLineHeight * 1.3f;
        EditorGUI.PropertyField(posObject, property.FindPropertyRelative("handAnim"));

        posObject.y += EditorGUIUtility.singleLineHeight * 1.3f;
        EditorGUI.PropertyField(posObject, property.FindPropertyRelative("boundaryAnim"));

        posObject.y += EditorGUIUtility.singleLineHeight * 1.3f;
        EditorGUI.PropertyField(posObject, property.FindPropertyRelative("boundarySprite"));

        posObject.y += EditorGUIUtility.singleLineHeight * 1.3f;
        EditorGUI.PropertyField(posObject, property.FindPropertyRelative("maxSizeSpriteMask"));

        posObject.y += EditorGUIUtility.singleLineHeight * 1.3f;
        EditorGUI.PropertyField(posObject, property.FindPropertyRelative("colorValidationPliage"));

        posObject.y += EditorGUIUtility.singleLineHeight * 1.3f;
        EditorGUI.PropertyField(posObject, property.FindPropertyRelative("colorBoundary"));

        posObject.y += EditorGUIUtility.singleLineHeight * 1.3f;
        EditorGUI.PropertyField(posObject, property.FindPropertyRelative("listBoundaryParticle"));

        posObject.y += EditorGUIUtility.singleLineHeight * 1.3f;
        EditorGUI.PropertyField(posObject, property.FindPropertyRelative("playedParticleOnce"));

        posObject.y += EditorGUIUtility.singleLineHeight * 1.3f;
        EditorGUI.PropertyField(posObject, property.FindPropertyRelative("prctMinValueToCompleteFold"));

        posObject.y += EditorGUIUtility.singleLineHeight * 1.3f;
        EditorGUI.PropertyField(posObject, property.FindPropertyRelative("speedAnimAutoComplete"));

        posObject.y += EditorGUIUtility.singleLineHeight * 1.3f;
        EditorGUI.PropertyField(posObject, property.FindPropertyRelative("makeRotation"));

        posObject.y += EditorGUIUtility.singleLineHeight * 1.3f;
        EditorGUI.PropertyField(posObject, property.FindPropertyRelative("yValueWanted"));

        //base.OnGUI(position, property, label);

    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return size;
    }
}
