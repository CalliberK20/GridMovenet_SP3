using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Collectable))]
public class CollectableEditor : Editor
{
    SerializedProperty spriteItems;
    SerializedProperty effectType;

    SerializedProperty heal;

    SerializedProperty damage;

    SerializedProperty slownessTime;
    SerializedProperty slowness;

    SerializedProperty spriteRenderer;

    private void OnEnable()
    {
        spriteItems = serializedObject.FindProperty("spriteItems");
        effectType = serializedObject.FindProperty("effectType");

        heal = serializedObject.FindProperty("heal");

        damage = serializedObject.FindProperty("damage");

        slownessTime = serializedObject.FindProperty("slownessTime");
        slowness = serializedObject.FindProperty("slowness");

        spriteRenderer = serializedObject.FindProperty("spriteRenderer");
    }

    public override void OnInspectorGUI()
    {
        Collectable _collectable = (Collectable)target;

        serializedObject.Update();
            
        EditorGUILayout.PropertyField(spriteItems);
        EditorGUILayout.PropertyField(effectType);

        EditorGUILayout.Space(10);

        if(_collectable.effectType == EffectType.Slow)
        {
            EditorGUILayout.PropertyField(slownessTime);
            EditorGUILayout.PropertyField(slowness);
        }
        else if(_collectable.effectType == EffectType.Damage)
        {
            EditorGUILayout.PropertyField(damage);
        }
        else if (_collectable.effectType == EffectType.Heal)
        {
            EditorGUILayout.PropertyField(heal);
        }

        EditorGUILayout.Space(10);

        EditorGUILayout.PropertyField(spriteRenderer);


        serializedObject.ApplyModifiedProperties();
    }
}
