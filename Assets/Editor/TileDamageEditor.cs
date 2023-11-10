using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TileDamage))]
public class TileDamageEditor : Editor
{
    SerializedProperty zoneType;

    SerializedProperty slowSpeed;

    SerializedProperty dmgSpeed;
    SerializedProperty damage;

    SerializedProperty healSpeed;
    SerializedProperty heal;

    public void OnEnable()
    {
        zoneType = serializedObject.FindProperty("zoneType");

        slowSpeed = serializedObject.FindProperty("slowSpeed");

        dmgSpeed = serializedObject.FindProperty("dmgSpeed");
        damage = serializedObject.FindProperty("damage");

        healSpeed = serializedObject.FindProperty("healSpeed");
        heal = serializedObject.FindProperty("heal");
    }

    public override void OnInspectorGUI()
    {
        TileDamage _tileDamage = (TileDamage)target;

        serializedObject.Update();

        EditorGUILayout.PropertyField(zoneType);

        EditorGUILayout.Space(10);
        if(_tileDamage.zoneType == EffectType.Slow)
        {
            EditorGUILayout.PropertyField(slowSpeed);
        }
        else if(_tileDamage.zoneType == EffectType.Damage)
        {
            EditorGUILayout.PropertyField(dmgSpeed);
            EditorGUILayout.PropertyField(damage);
        }
        else if(_tileDamage.zoneType == EffectType.Heal)
        {
            EditorGUILayout.PropertyField(healSpeed);
            EditorGUILayout.PropertyField(heal);
        }

        serializedObject.ApplyModifiedProperties();
    }
}
