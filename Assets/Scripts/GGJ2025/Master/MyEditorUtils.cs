using System;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace GGJ2025.Master
{
    public static class MyEditorUtils
    {
        /** マスターヘッダー描画 */
        public static void DrawMasterHeader(string masterName, ScriptableObject target, SerializedObject serializedObject)
        {
            // 保存ボタン
            if (GUILayout.Button("*保存*"))
            {
                EditorUtility.SetDirty(target);
                AssetDatabase.SaveAssets();
                serializedObject.ApplyModifiedProperties();
            }
            // マスター名
            EditorGUILayout.LabelField("");
            EditorGUILayout.LabelField(masterName, EditorStyles.boldLabel);
        }
        
        /** intフィールド描画 */
        public static void DrawIntField(string label, ref int value)
        {
            EditorGUILayout.LabelField("");
            value = EditorGUILayout.IntField(label, value);
        }
        
        /** Floatフィールド描画 */
        public static void DrawFloatField(string label, ref float value)
        {
            EditorGUILayout.LabelField("");
            value = EditorGUILayout.FloatField(label, value);
        }
        
        /** stringフィールド描画 */
        public static void DrawStringField(string label, ref string value)
        {
            EditorGUILayout.LabelField("");
            value = EditorGUILayout.TextField(label, value);
        }
        
        /** Enumポップアップ描画 */
        public static void DrawEnumPopup<T>(string label, ref int value) where T : Enum
        {
            EditorGUILayout.LabelField("");
            value = EditorGUILayout.Popup(label, value, System.Enum.GetNames(typeof(T)));
        }
        
        /** boolフィールド描画 */
        public static void DrawBoolField(string label, ref bool value)
        {
            EditorGUILayout.LabelField("");
            value = EditorGUILayout.Toggle(label, value);
        }
    }
}