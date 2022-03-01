using System;
using Game.Scripts.ScriptableObjects;
using Game.Scripts.ScriptableObjects.Environment;
using UnityEditor;
using UnityEngine;

namespace Game.Scriptables.Items.Weapons.Editor
{
    [CustomEditor(typeof(TreeSO))]
    public class DamageableEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Damages Taken");

            var damagesType = serializedObject.FindProperty("DamagesTakenType");
            var damagesValue = serializedObject.FindProperty("DamagesTakenMultiplier");

            if (damagesType == null || damagesValue == null)
                throw new InvalidOperationException("Property names changed and we don't know how to do change automatically :(");

            GUILayout.BeginHorizontal();
            GUILayout.BeginVertical();

            for (int i = 0; i < damagesType.arraySize; i++)
            {
                EditorGUILayout.PropertyField(damagesType.GetArrayElementAtIndex(i), GUIContent.none);
            }

            GUILayout.EndVertical();
            GUILayout.BeginVertical();

            for (int i = 0; i < damagesValue.arraySize; i++)
            {
                EditorGUILayout.PropertyField(damagesValue.GetArrayElementAtIndex(i), GUIContent.none);
            }

            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            var damageable = target as DamageableSO;
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("+", GUILayout.Width(25), GUILayout.Height(25)))
                damageable.AddResistance();
            else if (GUILayout.Button("-", GUILayout.Width(25), GUILayout.Height(25)))
                damageable.RemoveResistance();
            EditorGUILayout.EndHorizontal();

            serializedObject.ApplyModifiedProperties();
        }
    }
}
