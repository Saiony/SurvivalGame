using UnityEngine;
using UnityEditor;
using Game.Scripts.ScriptableObjects;
using UnityEditor.SceneManagement;
using System.Collections.Generic;

[CustomEditor(typeof(WeaponSO))]
public class WeaponEditor : Editor
{
    SerializedObject serializedWeapon;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Damage");

        var attackTypes = serializedObject.FindProperty("AttackTypes");
        var attackDamages = serializedObject.FindProperty("AttackDamages");

        GUILayout.BeginHorizontal();
        GUILayout.BeginVertical();

        for (int i = 0; i < attackTypes.arraySize; i++)
        {
            EditorGUILayout.PropertyField(attackTypes.GetArrayElementAtIndex(i), GUIContent.none);
        }

        GUILayout.EndVertical();
        GUILayout.BeginVertical();

        for (int i = 0; i < attackDamages.arraySize; i++)
        {
            EditorGUILayout.PropertyField(attackDamages.GetArrayElementAtIndex(i), GUIContent.none);
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
