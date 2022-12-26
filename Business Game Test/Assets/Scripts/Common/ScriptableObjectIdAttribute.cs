using System;
using UnityEngine;
using UnityEditor;

namespace Common
{
    public class ScriptableObjectIdAttribute : PropertyAttribute
    {
        
    }

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(ScriptableObjectIdAttribute))]
    public class ScriptableObjectIdDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (string.IsNullOrEmpty(property.stringValue))
            {
                property.stringValue = Guid.NewGuid().ToString();
            }
            if (GUI.Button(position, property.stringValue))
            {
                property.stringValue = Guid.NewGuid().ToString();
            }
        }
    }
#endif
}