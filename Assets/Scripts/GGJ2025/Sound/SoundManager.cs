using Common;
using UnityEngine;

namespace GGJ2025.Sounds
{
    public class SoundManager : SingletonMonoBehaviour<SoundManager>
    {
        /** 初期化フラグ */
        private static bool _isInitialized;
        /** BGM管理 */
        private BgmManager _bgmManager;
        /** SE管理 */
        private SeManager _seManager;

        public static BgmManager BGM => Entity._bgmManager;
        public static SeManager SE => Entity._seManager;

        /** 初期化 */
        public static void Initialize()
        {
            if (_isInitialized)
            {
                return;
            }
            Entity = new GameObject("SoundManager").AddComponent<SoundManager>();
            DontDestroyOnLoad(Entity.gameObject);
            
            Entity._bgmManager = Entity.gameObject.AddComponent<BgmManager>();
            Entity._bgmManager.Initialize();
            
            Entity._seManager = Entity.gameObject.AddComponent<SeManager>();
            Entity._seManager.Initialize();
            
            _isInitialized = true;
        }
    }

}