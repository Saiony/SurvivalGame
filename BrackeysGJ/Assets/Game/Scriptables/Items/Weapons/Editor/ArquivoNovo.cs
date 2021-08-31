using System;
using UnityEditor;
using UnityEngine;

namespace BrackeysGJ.Assets.Game.Scriptables.Items.Weapons.Editor
{
    public class CustomEditorLayoutUtils
    {
        public static void SerializeTwoListsThatLooksLikeADictionary(SerializedProperty type, SerializedProperty value, Action add, Action remove)
        {
            GUILayout.BeginHorizontal();
            GUILayout.BeginVertical();

            for (int i = 0; i < type.arraySize; i++)
            {
                EditorGUILayout.PropertyField(type.GetArrayElementAtIndex(i), GUIContent.none);
            }

            GUILayout.EndVertical();
            GUILayout.BeginVertical();

            for (int i = 0; i < value.arraySize; i++)
            {
                EditorGUILayout.PropertyField(value.GetArrayElementAtIndex(i), GUIContent.none);
            }

            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("+", GUILayout.Width(25), GUILayout.Height(25)))
                add();
            else if (GUILayout.Button("-", GUILayout.Width(25), GUILayout.Height(25)))
                remove();
            EditorGUILayout.EndHorizontal();
        }
    }
}
