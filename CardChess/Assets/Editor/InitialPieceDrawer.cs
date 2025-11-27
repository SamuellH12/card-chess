using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(Board.InitialPiece))]
public class InitialPieceDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // children of the serializable class
        var prefabProp   = property.FindPropertyRelative("prefab");
        var xProp        = property.FindPropertyRelative("x");
        var yProp        = property.FindPropertyRelative("y");
        var playerProp   = property.FindPropertyRelative("player");

        string prefabName = prefabProp.objectReferenceValue != null
            ? prefabProp.objectReferenceValue.name
            : "None";

        string niceLabel = $"{prefabName} p{playerProp.intValue} ({xProp.intValue}, {yProp.intValue})";

        EditorGUI.BeginProperty(position, label, property);
        // draw the property with our custom label; true => draw children in a foldout
        EditorGUI.PropertyField(position, property, new GUIContent(niceLabel), true);
        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        // ensure correct height whether folded or not
        return EditorGUI.GetPropertyHeight(property, true);
    }
}
