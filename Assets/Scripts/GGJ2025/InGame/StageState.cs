using System.Collections.Generic;
using UniRx;

namespace GGJ2025.InGame
{
    public class StageState
    {

        /** ステージの長さ */
        public readonly List<int> StageLength = new() {10, 20, 30, 40, 9999};
        
        /** 現在のステージ */
        public int StageNum { get; private set; }

        /** プレイヤーサイズ */
        private int _playerSize = 1;
        /** プレイヤーY座標 */
        private float _playerVerticalRate = 0;
        
        /** 速度 */
        private readonly FloatReactiveProperty _climbSpeed = new();
        /** 時間 */
        private readonly FloatReactiveProperty _timer = new();
        /** 高さ */
        private readonly FloatReactiveProperty _height = new();
        
        public IReadOnlyReactiveProperty<float> ClimbSpeedRP => _climbSpeed;
        public IReadOnlyReactiveProperty<float> TimerRP => _timer;
        public IReadOnlyReactiveProperty<float> HeightRP => _height;
        
        public StageState()
        {
            StageNum = 1;
            _timer.Value = 0;
            _height.Value = 0;
        }
        
        /** 速度更新 */
        private void UpdateClimbSpeed()
        {
            _climbSpeed.Value = _playerSize * _playerVerticalRate;
        }
        
        /** 時間更新 */
        public void UpdateTimer(float progress)
        {
            _timer.Value += progress;
        }
        
        /** 高さ更新 */
        public void UpdateHeight(float progress)
        {
            _height.Value += progress * _climbSpeed.Value;
        }
        
        /** ステージ移動 */
        public void NextStage()
        {
            StageNum += 1;
        }
        
        /** プレイヤーサイズ更新 */
        public void UpdatePlayerSize(int size)
        {
            _playerSize = size;
            UpdateClimbSpeed();
        }
        
        /** プレイヤーY座標更新 */
        public void UpdatePlayerVerticalRate(float rate)
        {
            _playerVerticalRate = rate;
            UpdateClimbSpeed();
        }
        
    }
}