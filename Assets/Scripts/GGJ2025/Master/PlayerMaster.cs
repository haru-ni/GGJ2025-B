using UnityEditor;
using UnityEngine;

namespace GGJ2025.Master
{
    [CreateAssetMenu(menuName = "GGJ2025/PlayerMaster")]
    public class PlayerMaster : ScriptableObject
    {
        /** 移動速度 */
        [SerializeField] private float moveSpeed;
        /** 速度減衰率 */
        [SerializeField] private float speedDecayRate;
        /** 最大ポイント */
        [SerializeField] private int maxPoint;
        /** 最小サイズ */
        [SerializeField] private float minSize;
        /** 最大サイズ */
        [SerializeField] private float maxSize;
        
        public float MoveSpeed => moveSpeed;
        public float DampingRate => speedDecayRate;
        public int MaxPoint => maxPoint;
        public float MinSize => minSize;
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
                MyEditorUtils.DrawFloatField("移動速度", ref master.moveSpeed);
                MyEditorUtils.DrawFloatField("速度減衰率", ref master.speedDecayRate);
                MyEditorUtils.DrawIntField("最大ポイント", ref master.maxPoint);
                MyEditorUtils.DrawFloatField("最小サイズ", ref master.minSize);
                MyEditorUtils.DrawFloatField("最大サイズ", ref master.maxSize);
            }
        }
        
        #endif
    }
}