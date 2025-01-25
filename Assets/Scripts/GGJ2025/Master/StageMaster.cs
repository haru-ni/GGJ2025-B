using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace GGJ2025.Master
{
    [CreateAssetMenu(menuName = "GGJ2025/StageMaster")]
    public class StageMaster : ScriptableObject
    {
        /** 最大レベル */
        [SerializeField] private int maxLevel;
        /** 最大速度ポイント */
        [SerializeField] private float maxPointSpeed;
        /** 最小速度 */
        [SerializeField] private float minSpeed;
        /** 最大速度 */
        [SerializeField] private float maxSpeed;
        
        /** ステージの長さ */
        [SerializeField] private int stageLengthCount;
        [SerializeField] private List<int> stageLength;
        
        /** プレイヤー高さボーナス数 */
        [SerializeField] private int playerHeightBonusCount;
        /** プレイヤー座標リスト */
        [SerializeField] private List<float> playerHeightList;
        /** プレイヤー座標倍率リスト */
        [SerializeField] private List<float> playerHeightSpeedRateList;
        
        public int MaxLevel => maxLevel;
        public float MaxPointSpeed => maxPointSpeed;
        public float MinSpeed => minSpeed;
        public float MaxSpeed => maxSpeed;
        public List<int> StageLength => stageLength;
        public List<float> PlayerHeightList => playerHeightList;
        public List<float> PlayerHeightSpeedBonusList => playerHeightSpeedRateList;
        

        
        # if UNITY_EDITOR
        
        [CustomEditor(typeof(StageMaster))]
        public class StageMasterEditor: Editor
        {
            
            private void OnEnable()
            {
                var master = (StageMaster) target;
                master.playerHeightList ??= new List<float>();
                master.playerHeightSpeedRateList ??= new List<float>();
            }
            
            public override void OnInspectorGUI()
            {
                var master = (StageMaster) target;
                
                MyEditorUtils.DrawMasterHeader("ステージ", master, serializedObject);
                MyEditorUtils.DrawIntField("最大レベル", ref master.maxLevel);
                MyEditorUtils.DrawFloatField("最大速度ポイント", ref master.maxPointSpeed);
                MyEditorUtils.DrawFloatField("最小速度", ref master.minSpeed);
                MyEditorUtils.DrawFloatField("最大速度", ref master.maxSpeed);
                
                #region ステージの長さ
                MyEditorUtils.DrawIntField("ステージの長さ数", ref master.stageLengthCount);
                    
                EditorGUILayout.LabelField("");
                EditorGUILayout.LabelField("ステージの長さリスト");
                if (master.stageLength.Count > master.stageLengthCount)
                {
                    master.stageLength = master.stageLength.Take(master.stageLengthCount).ToList();
                }
                for (var i = 0; i < master.stageLengthCount; i++)
                {
                    if (master.stageLength.Count <= i)
                    {
                        master.stageLength.Add(0);
                    }
                    master.stageLength[i] = EditorGUILayout.IntField("", master.stageLength[i]);    
                }
                #endregion

                #region 高さボーナス
                MyEditorUtils.DrawIntField("プレイヤー高さボーナス数", ref master.playerHeightBonusCount);
                
                EditorGUILayout.LabelField("");
                EditorGUILayout.LabelField("プレイヤー座標リスト");
                if (master.playerHeightList.Count > master.playerHeightBonusCount)
                {
                    master.playerHeightList = master.playerHeightList.Take(master.playerHeightBonusCount).ToList();
                }
                for (var i = 0; i < master.playerHeightBonusCount; i++)
                {
                    if (master.playerHeightList.Count <= i)
                    {
                        master.playerHeightList.Add(0);
                    }
                    master.playerHeightList[i] = EditorGUILayout.FloatField("", master.playerHeightList[i]);    
                }
                
                EditorGUILayout.LabelField("");
                EditorGUILayout.LabelField("プレイヤー座標倍率リスト");
                if (master.playerHeightSpeedRateList.Count > master.playerHeightBonusCount)
                {
                    master.playerHeightSpeedRateList = master.playerHeightSpeedRateList.Take(master.playerHeightBonusCount).ToList();
                }
                for (var i = 0; i < master.playerHeightBonusCount; i++)
                {
                    if (master.playerHeightSpeedRateList.Count <= i)
                    {
                        master.playerHeightSpeedRateList.Add(0);
                    }
                    master.playerHeightSpeedRateList[i] = EditorGUILayout.FloatField("", master.playerHeightSpeedRateList[i]);    
                }
                #endregion

            }
        }
        
        # endif
    }
}