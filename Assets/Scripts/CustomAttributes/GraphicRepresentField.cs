using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field)]
public class GraphicRepresentFieldAttribute : PropertyAttribute
{
	public float maxCoordValue;

	public GraphicRepresentFieldAttribute(float maxCoordValue)
	{
		this.maxCoordValue = maxCoordValue;
	}

	public override bool Match(object obj)
	{
		return (obj as Vector2?) != null;
	}
}
[CustomPropertyDrawer(typeof(GraphicRepresentFieldAttribute))]
public class GraphicRepresentInspectorDrawer : PropertyDrawer
{
	int tSize = 75;
	Color32 outBgColor = new Color(0.22f, 0.22f, 0.22f);
	Color32 bgColor = Color.gray;
	Color32 pointColor = Color.green;
	public override float GetPropertyHeight(
		SerializedProperty property, GUIContent label)
	{
		return tSize;
	}
	public override void OnGUI(
		Rect position, SerializedProperty property, GUIContent label)
	{
		if(property.propertyType == SerializedPropertyType.Vector2)
		{
			Texture2D texture = 
				new Texture2D(tSize, tSize, TextureFormat.RGB24, false);
			//texture = new Texture2D(tSize, tSize);
			Color32[] clr = new Color32[tSize * tSize];
			for(int i = 0; i < tSize; i++)
			{
				int yC = 
					(int)Mathf.Sqrt(tSize*tSize/4 - (i - tSize/2)*(i - tSize/2));
				int j = i * tSize;
				for (; j < tSize / 2 - yC + i * tSize; j++)
					clr[j] = outBgColor;
				for (; j < tSize / 2 + yC + i * tSize; j++)
					clr[j] = Color.gray;
				for (; j < (i + 1) * tSize; j++)
					clr[j] = outBgColor;
			}
			texture.SetPixels32(clr);
			Vector2 vector = property.vector2Value;
			GraphicRepresentFieldAttribute attr = 
				this.attribute as GraphicRepresentFieldAttribute;
			int y = (int)((vector.y / attr.maxCoordValue + 1) * tSize / 2);
			int x = (int)((vector.x / attr.maxCoordValue + 1) * tSize / 2);
			texture.SetPixel(y, x, pointColor);
			texture.SetPixel(y, x + 1, pointColor);
			texture.SetPixel(y, x - 1, pointColor);
			texture.SetPixel(y + 1, x, pointColor);
			texture.SetPixel(y - 1, x, pointColor);
			texture.Apply();
			EditorGUI.BeginProperty(position, label, property);
			position.height = tSize;
			position = 
				EditorGUI.PrefixLabel(
					position, GUIUtility.GetControlID(FocusType.Passive), label);
			position.width = tSize;
			EditorGUI.DrawPreviewTexture(position, texture);
			EditorGUI.EndProperty();
		}
	}
}
