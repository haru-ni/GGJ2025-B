using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace GGJ2025.Master
{
    [CreateAssetMenu(menuName = "GGJ2025/ResultMaster")]
    public class ResultMaster : ScriptableObject
    {
        /** セリフ数 */
        [SerializeField] int sentenceCount;
        /**  セリフ条件値 */
        [SerializeField] List<int> sentenceConditions;
        /** セリフ */
        [SerializeField] List<string> sentences;
        
        public List<int> SentenceConditions => sentenceConditions;
        public List<string> Sentences => sentences;
        
        # if UNITY_EDITOR

        [CustomEditor(typeof(ResultMaster))]
        public class ResultMasterEditor : Editor
        {
            
            private void OnEnable()
            {
                var master = (ResultMaster)target;
                master.sentenceConditions ??= new List<int>();
                master.sentences ??= new List<string>();
            }

            public override void OnInspectorGUI()
            {
                var master = (ResultMaster)target;

                MyEditorUtils.DrawMasterHeader("リザルト", master, serializedObject);

                #region セリフ

                EditorGUILayout.LabelField("");
                EditorGUILayout.LabelField("セリフ");
                MyEditorUtils.DrawIntField("セリフ数", ref master.sentenceCount);
                if (master.sentenceConditions.Count > master.sentenceCount)
                {
                    master.sentenceConditions.RemoveRange(master.sentenceCount,
                        master.sentenceConditions.Count - master.sentenceCount);
                    master.sentences.RemoveRange(master.sentenceCount, master.sentences.Count - master.sentenceCount);
                }

                EditorGUILayout.LabelField("セリフ条件値");
                for (int i = 0; i < master.sentenceCount; i++)
                {
                    if (master.sentenceConditions.Count <= i)
                    {
                        master.sentenceConditions.Add(0);
                    }

                    master.sentenceConditions[i] = EditorGUILayout.IntField("", master.sentenceConditions[i]);
                }
                
                EditorGUILayout.LabelField("セリフ");
                for (int i = 0; i < master.sentenceCount; i++)
                {
                    if (master.sentences.Count <= i)
                    {
                        master.sentences.Add("");
                    }

                    master.sentences[i] = EditorGUILayout.TextField("", master.sentences[i]);
                }
                
                #endregion
                

            }

        }
        
        # endif
        
    }
}