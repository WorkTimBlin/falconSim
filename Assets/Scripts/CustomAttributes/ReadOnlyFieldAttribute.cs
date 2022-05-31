using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;

public class ReadOnlyFieldAttribute : PropertyAttribute { }

[CustomPropertyDrawer(typeof(ReadOnlyFieldAttribute))]
public class ReadOnlyInspectorDrawer : PropertyDrawer
{
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		bool wasEnabled = GUI.enabled;
		GUI.enabled = false;
		EditorGUI.PropertyField(position, property, label);
		GUI.enabled = wasEnabled;
	}
}