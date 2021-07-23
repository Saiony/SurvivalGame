using UnityEngine;
using UnityEditor;
using Game.Scripts.ScriptableObjects;
using UnityEditor.SceneManagement;
using System.Collections.Generic;
using System;

[CustomEditor(typeof(WeaponSO))]
public class WeaponEditor : Editor
{
    SerializedObject serializedWeapon;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Damage");

        var damagesType = serializedObject.FindProperty("DamagesType");
        var damagesValue = serializedObject.FindProperty("DamagesValue");

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
        var weapon = target as WeaponSO;
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("+", GUILayout.Width(25), GUILayout.Height(25)))
            weapon.AddDamage();
        else if (GUILayout.Button("-", GUILayout.Width(25), GUILayout.Height(25)))
            weapon.RemoveDamage();
        EditorGUILayout.EndHorizontal();

        serializedObject.ApplyModifiedProperties();
    }
}
