using UnityEditor;
using UnityEngine;

public abstract class InventoryObjectEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        SerializedProperty listProp = serializedObject.FindProperty("slots");    
        DisplayArray(listProp);          

        serializedObject.ApplyModifiedProperties();
    }
    private void DisplayArray(SerializedProperty array, string caption=null)
    {
        EditorGUILayout.LabelField((caption == null) ? array.name : caption);
        int size = array.arraySize;
        array.Next(true);       //generic field (list)
        array.Next(true);       //arrays size field
        {
            GUI.enabled = false;
            EditorGUILayout.PropertyField(array);
        }
        GUI.enabled = true;
        for (int i = 0; i < size; i++)
        {
            array.Next(false);       //first array element            
            EditorGUILayout.PropertyField(array);
        }
    }
}