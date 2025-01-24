using GGJ2025.UI;
using UnityEngine;
using UniRx;

namespace GGJ2025.InGame
{
    public class StageUIView : MonoBehaviour
    {
        [SerializeField] private TextWrapper timerText;
        [SerializeField] private TextWrapper heightText;
        [SerializeField] private TextWrapper speedText;

        private void Awake()
        {
            // 初期表示
            UpdateTimer(0);
            UpdateHeight(0);
            UpdateSpeed(0);
        }
        
        /** 時間表示更新 */
        private void UpdateTimer(float timer)
        {
            // mm:ss形式に変換
            var minutes = Mathf.FloorToInt(timer / 60);
            var seconds = Mathf.FloorToInt(timer % 60);
            timerText.SetText($"{minutes:00}:{seconds:00}");
        }
        
        /** 高さ表示更新 */
        private void UpdateHeight(float height)
        {
            heightText.SetText($"{(int) height}m");
        }
        
        /** 速度表示更新 */
        private void UpdateSpeed(float speed)
        {
            speedText.SetText($"{speed}");
        }
        
        /** ゲームスタート */
        public void StartGame(StageView stageView)
        {
            var stageState = stageView.GetState();
            // 購読
            stageState.TimerRP.Subscribe(UpdateTimer);
            stageState.HeightRP.Subscribe(UpdateHeight);
            stageState.ClimbSpeedRP.Subscribe(UpdateSpeed);
            
        }
    }
}