using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

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
	Color32 outBgColor = new Color(0.2f, 0.2f, 0.2f);
	Color32 bgColor = Color.gray;
	Color32 pointColor = Color.green;
	Color32[] bgBitmap;
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
			if (bgBitmap == null) CreatebgBitmap();
			Texture2D texture = new Texture2D(tSize, tSize, TextureFormat.RGB24, false);
			texture.SetPixels32(bgBitmap);
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

	private void CreatebgBitmap()
	{
		bgBitmap = new Color32[tSize * tSize];
		for (int i = 0; i < tSize; i++)
		{
			int yC =
				(int)Mathf.Sqrt(tSize * tSize / 4 - (i - tSize / 2) * (i - tSize / 2));
			int j = i * tSize;
			for (; j < tSize / 2 - yC + i * tSize; j++)
				bgBitmap[j] = outBgColor;
			for (; j < tSize / 2 + yC + i * tSize; j++)
				bgBitmap[j] = Color.gray;
			for (; j < (i + 1) * tSize; j++)
				bgBitmap[j] = outBgColor;
		}
	}
}
