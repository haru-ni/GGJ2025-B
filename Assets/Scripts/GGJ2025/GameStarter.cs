using Common;
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
        }
    }
}