using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace GGJ2025.Master
{
    [CreateAssetMenu(menuName = "GGJ2025/PlayerMaster")]
    public class PlayerMaster : ScriptableObject
    {
        /** 基本速度 */
        [SerializeField] private float baseSpeed;
        /** 速度減衰率 */
        [SerializeField] private float speedDecayRate;
        /** 最大グレード */
        [SerializeField] private int maxGrade;
        /** サイズリスト */
        [SerializeField] private List<Vector2> sizeList;
        
        public float BaseSpeed => baseSpeed;
        public float DampingRate => speedDecayRate;
        public int MaxGrade => maxGrade;
        public List<Vector2> SizeList => sizeList;

        # if UNITY_EDITOR
        [CustomEditor(typeof(PlayerMaster))]
        public class PlayerMasterEditor: Editor
        {
            
            private void OnEnable()
            {
                var master = (PlayerMaster) target;
                master.sizeList ??= new List<Vector2>();
            }
            
            public override void OnInspectorGUI()
            {
                var master = (PlayerMaster) target;
                
                MyEditorUtils.DrawMasterHeader("プレイヤー", master, serializedObject);
                MyEditorUtils.DrawFloatField("基本速度", ref master.baseSpeed);
                MyEditorUtils.DrawFloatField("速度減衰率", ref master.speedDecayRate);
                MyEditorUtils.DrawIntField("最大グレード", ref master.maxGrade);
                
                EditorGUILayout.LabelField("");
                EditorGUILayout.LabelField("サイズリスト");
                if (master.sizeList.Count > master.MaxGrade)
                {
                    master.sizeList = master.sizeList.Take(master.MaxGrade).ToList();
                }
                for (var i = 0; i < master.MaxGrade; i++)
                {
                    if (master.sizeList.Count <= i)
                    {
                        master.sizeList.Add(Vector2.zero);
                    }
                    master.sizeList[i] = EditorGUILayout.Vector2Field("", master.sizeList[i]);    
                }
                
            }
        }
        
        #endif
    }
}