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
        
        public float BaseSpeed => baseSpeed;
        public float DampingRate => speedDecayRate;

        # if UNITY_EDITOR
        [CustomEditor(typeof(PlayerMaster))]
        public class PlayerMasterEditor: Editor
        {
            public override void OnInspectorGUI()
            {
                var master = (PlayerMaster) target;
                
                MyEditorUtils.DrawMasterHeader("プレイヤー", master, serializedObject);
                MyEditorUtils.DrawFloatField("基本速度", ref master.baseSpeed);
                MyEditorUtils.DrawFloatField("速度減衰率", ref master.speedDecayRate);
            }
        }
        
        #endif
    }
}