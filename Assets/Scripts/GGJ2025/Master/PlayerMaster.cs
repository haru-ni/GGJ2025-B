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
        /** 最大ポイント */
        [SerializeField] private int maxPoint;
        /** 最大サイズ */
        [SerializeField] private float maxSize;
        
        public float BaseSpeed => baseSpeed;
        public float DampingRate => speedDecayRate;
        public int MaxPoint => maxPoint;
        public float MaxSize => maxSize;

        # if UNITY_EDITOR
        [CustomEditor(typeof(PlayerMaster))]
        public class PlayerMasterEditor: Editor
        {
            
            private void OnEnable()
            {
                var master = (PlayerMaster) target;
            }
            
            public override void OnInspectorGUI()
            {
                var master = (PlayerMaster) target;
                
                MyEditorUtils.DrawMasterHeader("プレイヤー", master, serializedObject);
                MyEditorUtils.DrawFloatField("基本速度", ref master.baseSpeed);
                MyEditorUtils.DrawFloatField("速度減衰率", ref master.speedDecayRate);
                MyEditorUtils.DrawIntField("最大ポイント", ref master.maxPoint);
                MyEditorUtils.DrawFloatField("最大サイズ", ref master.maxSize);
            }
        }
        
        #endif
    }
}