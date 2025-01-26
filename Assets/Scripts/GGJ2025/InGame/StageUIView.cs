using GGJ2025.UI;
using UnityEngine;
using UniRx;

namespace GGJ2025.InGame
{
    public class StageUIView : MonoBehaviour
    {
        [SerializeField] private TextWrapper timerText;
        [SerializeField] private TextWrapper timeBonusText;
        [SerializeField] private TextWrapper heightText;
        [SerializeField] private TextWrapper speedText;
        
        /** 時間表示更新 */
        private void UpdateTimer(float timer)
        {
            // mm:ss形式に変換
            var minutes = Mathf.FloorToInt(timer / 60);
            var seconds = Mathf.FloorToInt(timer % 60);
            timerText.SetText($"{minutes:00}:{seconds:00}");
        }
        
        /** 時間ボーナス表示更新 */
        private void UpdateTimeBonus(float timeBonus)
        {
            // 0.x形式に変換
            timeBonusText.SetText($"倍率 {timeBonus:0.0}");
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
            UpdateTimer(0);
            // UpdateHeight(0);
            // UpdateSpeed(0);
            
            var stageState = stageView.GetState();
            // 購読
            stageState.TimerRP.Subscribe(UpdateTimer);
            stageState.TimeBonusRP.Subscribe(UpdateTimeBonus);
            // stageState.HeightRP.Subscribe(UpdateHeight);
            // stageState.ClimbSpeedRP.Subscribe(UpdateSpeed);
            
        }
    }
}