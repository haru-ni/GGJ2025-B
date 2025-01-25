using UnityEngine;

namespace GGJ2025.InGame
{
    /** インゲーム管理クラス */
    public class InGameController : MonoBehaviour
    {
        [SerializeField] private StageView stageView;
        [SerializeField] private StageUIView stageUIView;
        
        private void Start()
        {
            // ゲームスタート
            stageView.StartGame();
            stageUIView.StartGame(stageView);
            
            stageView.OnStart();
        }
        
    }
}