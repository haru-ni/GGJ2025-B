using Common;
using GGJ2025.Sounds;
using UnityEngine;

namespace GGJ2025
{
    public class GameStarter : SingletonMonoBehaviour<GameStarter>
    {
        // 初期化
        public static bool IsInitialized { get; private set; }
    
        // ゲーム開始時(Awake以前)の生成処理
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Generate()
        {
            Entity = new GameObject("GameStarter").AddComponent<GameStarter>();
        }
    
        private void Awake()
        {
            InitializeGame();
            Debug.Log("Game Awake");
            // 初期化がすんだら破棄する
            Destroy(gameObject);
            IsInitialized = true;
        }
    
        private static void InitializeGame()
        {
            Screen.SetResolution(GameConst.ScreenWidth, GameConst.ScreenHeight, FullScreenMode.Windowed);
            
            // サウンド初期化
            SoundManager.Initialize();
            
            // サウンド読み込み
            SoundManager.BGM.SetAudioClip((int)BGMs.Main, null, Resources.Load<AudioClip>("Sound/BgmMain"));
            SoundManager.SE.Set((int)SEs.GameClear, Resources.Load<AudioClip>("Sound/SeGameClear"));
            SoundManager.SE.Set((int)SEs.GameOver, Resources.Load<AudioClip>("Sound/SeGameOver"));
            
        }
    }
}