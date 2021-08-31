using UnityEngine;
using UnityEditor;
using Game.Scripts.ScriptableObjects;
using UnityEditor.SceneManagement;
using System.Collections.Generic;
using System;
using BrackeysGJ.Assets.Game.Scriptables.Items.Weapons.Editor;

[CustomEditor(typeof(WeaponSO))]
public class WeaponEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Resistances");

        var damagesType = serializedObject.FindProperty("DamagesType");
        var damagesValue = serializedObject.FindProperty("DamagesValue");

        if (damagesType == null || damagesValue == null)
            throw new InvalidOperationException("Property names changed and we don't know how to do change automatically :(");

        var weapon = target as WeaponSO;
        CustomEditorLayoutUtils.SerializeTwoListsThatLooksLikeADictionary(damagesType, damagesValue, weapon.AddDamage, weapon.RemoveDamage);

        serializedObject.ApplyModifiedProperties();
    }
}
