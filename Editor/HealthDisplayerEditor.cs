using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(HealthDisplayer), true)]
public class HealthDisplayerEditor : Editor
{
    HealthDisplayer healthDisplayer;
    
    SerializedProperty textObject;
    SerializedProperty displayTypeProp;
    SerializedProperty maxHealthTextObject;
    SerializedProperty currentHealthTextObject;
    SerializedProperty slider;
    SerializedProperty backgroundColor;
    SerializedProperty fillColor;
    SerializedProperty textStyleProp;
    private void OnEnable()
    {
        healthDisplayer = (HealthDisplayer)target;
        // Get references
        AssingSerializedProperty();
    }
    private void AssingSerializedProperty()
    {
        displayTypeProp = serializedObject.FindProperty("displayType");
        textObject = serializedObject.FindProperty("TextObject");
        slider = serializedObject.FindProperty("Slider");
        backgroundColor = serializedObject.FindProperty("BackgroundColor");
        maxHealthTextObject = serializedObject.FindProperty(nameof(HealthDisplayer.MaxHealthTextObject));
        currentHealthTextObject = serializedObject.FindProperty(nameof(HealthDisplayer.CurrentHealthTextObject));
        fillColor = serializedObject.FindProperty("FillColor");
        textStyleProp = serializedObject.FindProperty("textStyle");
    }
    
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(displayTypeProp);
    
        // Show slider settings if displayType is Slider
        if (displayTypeProp.enumValueIndex == 0)
        {
            EditorGUILayout.LabelField("Slider Settings", EditorStyles.boldLabel);
            
            EditorGUILayout.PropertyField(backgroundColor);
            
            EditorGUILayout.PropertyField(fillColor);

            if (slider.objectReferenceValue == null)
            {
                EditorGUILayout.HelpBox("A Slider component is missing.", MessageType.Warning);
            }
            EditorGUILayout.PropertyField(slider);
        }

        // Show text settings if displayType is Text
        if (displayTypeProp.enumValueIndex == 1)
        {
            EditorGUILayout.LabelField("Text Settings", EditorStyles.boldLabel);
            

            EditorGUILayout.PropertyField(textStyleProp);
            // Show custom text fields if textStyle is Custom
            if (textStyleProp.enumValueIndex == 3) // Custom
            {
                EditorGUILayout.PropertyField(maxHealthTextObject);
                EditorGUILayout.PropertyField(currentHealthTextObject);
            }
            else
            {
                if (textObject.objectReferenceValue == null)
                {
                    EditorGUILayout.HelpBox("A TextMeshProUGUI component is missing", MessageType.Warning);
                }
                EditorGUILayout.PropertyField(textObject);
            }
        }
        serializedObject.ApplyModifiedProperties();
        if (!Application.isPlaying)
        {
            healthDisplayer.UpdateHealthDisplayEditor();
        }
    }
}
