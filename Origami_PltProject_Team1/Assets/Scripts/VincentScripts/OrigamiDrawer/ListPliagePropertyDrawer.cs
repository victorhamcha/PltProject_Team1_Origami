using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(Pliage))]
public class ListPliagePropertyDrawer : PropertyDrawer
{
    private const float SIZE_EXPENDED = 560;
    private const float SIZE_JUMP_PROPERTY = 1.3f;
    private const float SIZE_JUMP_HEADER = 2f;

    private GUIStyle _headerStyle = new GUIStyle();

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        position.height = EditorGUIUtility.singleLineHeight;

        property.isExpanded = EditorGUI.Foldout(position, property.isExpanded, label.text.Replace("Element", "Folds"));

        _headerStyle.fontSize = 13;
        _headerStyle.fontStyle = FontStyle.Bold;
        _headerStyle.normal.textColor = Color.white;

        if (property.isExpanded)
        {
            Rect posObject = position;

            posObject.y += EditorGUIUtility.singleLineHeight * SIZE_JUMP_HEADER;
            EditorGUI.LabelField(posObject, "Custom Folds", _headerStyle);

            posObject.y += EditorGUIUtility.singleLineHeight * SIZE_JUMP_PROPERTY;

            EditorGUI.PropertyField(posObject, property.FindPropertyRelative("goodPointSelection"));

            posObject.y += EditorGUIUtility.singleLineHeight * SIZE_JUMP_PROPERTY;
            EditorGUI.PropertyField(posObject, property.FindPropertyRelative("endPointSelection"));

            posObject.y += EditorGUIUtility.singleLineHeight * SIZE_JUMP_PROPERTY;
            EditorGUI.PropertyField(posObject, property.FindPropertyRelative("animToPlay"));

            posObject.y += EditorGUIUtility.singleLineHeight * SIZE_JUMP_PROPERTY;
            SerializedProperty spIsConfirmationPliage = property.FindPropertyRelative("isConfirmationPliage");
            EditorGUI.PropertyField(posObject, spIsConfirmationPliage);

            SerializedProperty spListBoundary = property.FindPropertyRelative("listBoundaryParticle");
            float offsetList = 0;

            if (spIsConfirmationPliage.boolValue)
            {
                posObject.y += EditorGUIUtility.singleLineHeight * SIZE_JUMP_PROPERTY;
                EditorGUI.PropertyField(posObject, property.FindPropertyRelative("handAnim"));

                posObject.y += EditorGUIUtility.singleLineHeight * SIZE_JUMP_PROPERTY;
                EditorGUI.PropertyField(posObject, property.FindPropertyRelative("maxSizeSpriteMask"));
            }
            else
            {
                posObject.y += EditorGUIUtility.singleLineHeight * SIZE_JUMP_PROPERTY;
                EditorGUI.PropertyField(posObject, spListBoundary, true);
                offsetList = EditorGUI.GetPropertyHeight(spListBoundary, true);
            }

            posObject.y += EditorGUIUtility.singleLineHeight * SIZE_JUMP_PROPERTY + offsetList;
            EditorGUI.PropertyField(posObject, property.FindPropertyRelative("drawPointSelection"));

            posObject.y += EditorGUIUtility.singleLineHeight * SIZE_JUMP_PROPERTY;
            EditorGUI.PropertyField(posObject, property.FindPropertyRelative("playBounce"));

            posObject.y += EditorGUIUtility.singleLineHeight * SIZE_JUMP_PROPERTY;
            EditorGUI.PropertyField(posObject, property.FindPropertyRelative("offsetPlacementPliage"));

            posObject.y += EditorGUIUtility.singleLineHeight * SIZE_JUMP_HEADER;
            EditorGUI.LabelField(posObject, "Custom Boundary", _headerStyle);

            posObject.y += EditorGUIUtility.singleLineHeight * SIZE_JUMP_PROPERTY;
            EditorGUI.PropertyField(posObject, property.FindPropertyRelative("boundaryAnim"));

            posObject.y += EditorGUIUtility.singleLineHeight * SIZE_JUMP_PROPERTY;
            EditorGUI.PropertyField(posObject, property.FindPropertyRelative("boundarySprite"));

            posObject.y += EditorGUIUtility.singleLineHeight * SIZE_JUMP_PROPERTY;
            EditorGUI.PropertyField(posObject, property.FindPropertyRelative("colorValidationPliage"));

            posObject.y += EditorGUIUtility.singleLineHeight * SIZE_JUMP_PROPERTY;
            EditorGUI.PropertyField(posObject, property.FindPropertyRelative("colorBoundary"));

            posObject.y += EditorGUIUtility.singleLineHeight * SIZE_JUMP_HEADER;
            EditorGUI.LabelField(posObject, "Auto Complete", _headerStyle);

            posObject.y += EditorGUIUtility.singleLineHeight * SIZE_JUMP_PROPERTY;
            EditorGUI.PropertyField(posObject, property.FindPropertyRelative("prctMinValueToCompleteFold"));

            posObject.y += EditorGUIUtility.singleLineHeight * SIZE_JUMP_PROPERTY;
            EditorGUI.PropertyField(posObject, property.FindPropertyRelative("speedAnimAutoComplete"));

            posObject.y += EditorGUIUtility.singleLineHeight * SIZE_JUMP_HEADER;
            EditorGUI.LabelField(posObject, "Rotation", _headerStyle);

            posObject.y += EditorGUIUtility.singleLineHeight * SIZE_JUMP_PROPERTY;
            SerializedProperty spMakeRotation = property.FindPropertyRelative("makeRotation");
            EditorGUI.PropertyField(posObject, spMakeRotation);

            if (spMakeRotation.boolValue)
            {
                posObject.y += EditorGUIUtility.singleLineHeight * SIZE_JUMP_PROPERTY;
                EditorGUI.PropertyField(posObject, property.FindPropertyRelative("yValueWanted"));
            }

        }

    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        if (property.isExpanded)
        {
            float sizeReturn = SIZE_EXPENDED;
            SerializedProperty spListBoundary = property.FindPropertyRelative("listBoundaryParticle"); 
            bool isConfirmationPliage = property.FindPropertyRelative("isConfirmationPliage").boolValue;
            bool makeRotation = property.FindPropertyRelative("makeRotation").boolValue;

            if (spListBoundary.isExpanded && !isConfirmationPliage)
            {
                sizeReturn += EditorGUI.GetPropertyHeight(spListBoundary, true) - EditorGUIUtility.singleLineHeight;
            }
            if (!makeRotation)
            {
                sizeReturn -= EditorGUIUtility.singleLineHeight;
            }
            return sizeReturn;
        }
        return EditorGUIUtility.singleLineHeight;
    }
}
