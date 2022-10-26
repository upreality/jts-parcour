using ABTests.domain.model;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ABTestGroup))]
public class ABTestGroupDrawer : PropertyDrawer
{
    private const int Divider = 15;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        var groupNameRect = GetPropertyInlineRect(position, 3, 0);
        var propertyRect = GetPropertyInlineRect(position, 3, 1);
        var weightRect = GetPropertyInlineRect(position, 3, 2);

        var groupNameProperty = property.FindPropertyRelative("groupName");
        EditorGUI.PropertyField(groupNameRect, groupNameProperty, GUIContent.none);
        DrawHint(groupNameProperty, groupNameRect, "Group name");

        var propertyNameProperty = property.FindPropertyRelative("property");
        EditorGUI.PropertyField(propertyRect, propertyNameProperty, GUIContent.none);
        DrawHint(propertyNameProperty, propertyRect, "Property name");

        var weightProperty = property.FindPropertyRelative("weight");
        EditorGUI.PropertyField(weightRect, weightProperty, GUIContent.none);

        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();
    }

    private static void DrawHint(SerializedProperty property, Rect propertyRect, string hint)
    {
        if (property.stringValue.Length > 0)
            return;

        const int hintMargin = 10;
        var hintRect = new Rect(
            x: propertyRect.x + hintMargin,
            y: propertyRect.y,
            width: propertyRect.width - hintMargin * 2,
            height: propertyRect.height
        );
        EditorGUI.LabelField(hintRect, hint, EditorStyles.miniLabel);
    }

    private static Rect GetPropertyInlineRect(Rect position, int propertiesCount, int propertyIndex)
    {
        var propWidth = position.width / propertiesCount - propertiesCount * Divider;
        var propertyPos = position.x + (propWidth + Divider) * propertyIndex;
        return new Rect(propertyPos, position.y, propWidth, position.height);
    }
}