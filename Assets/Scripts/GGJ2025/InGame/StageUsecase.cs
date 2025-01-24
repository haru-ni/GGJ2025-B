using System;
using UniRx;

namespace GGJ2025.InGame
{
    public class StageUsecase
    {
        
        private readonly StageState _state;
        
        public StageUsecase(StageState state)
        {
            _state = state;
        }
        
        /** 時間経過 */
        public void TimeProgress(float progress)
        {
            _state.UpdateTimer(progress);
            _state.UpdateHeight(progress);
        }
        
        /** プレイヤーサイズ更新 */
        public void UpdatePlayerSize(int size)
        {
            _state.UpdatePlayerSize(size);
        }
        
        /** プレイヤーY座標更新 */
        public void UpdatePlayerVerticalRate(float verticalRate)
        {
            _state.UpdatePlayerVerticalRate(verticalRate);
        }
        
        /** 次ステージへ */
        public void NextStage()
        {
            _state.NextStage();
        }
        
        /** ステージ経過購読 */
        public IDisposable SubscribeStageProgress(Action<int> stageProgressAction)
        {
            return _state.HeightRP.Subscribe(height =>
            {
                var stageLength = _state.StageLength[_state.StageNum - 1];
                if (height >= stageLength)
                {
                    stageProgressAction(_state.StageNum);
                }
            });
        }
        
    }
}