using System.Collections.Generic;
using GGJ2025.Master;
using UniRx;

namespace GGJ2025.InGame
{
    public class StageState
    {
        /** マスター */
        public readonly StageMaster Master;
        /** ステージの長さ */
        public readonly List<int> StageLength = new() {10, 20, 30, 40, 9999};
        
        /** 現在のステージ */
        public int StageNum { get; private set; }

        /** プレイヤーサイズ速度 */
        private int _playerSizeSpeed;
        /** プレイヤーY座標ボーナス */
        private float _playerHeightSpeedBonus;
        
        /** 速度 */
        private readonly FloatReactiveProperty _climbSpeed = new();
        /** 時間 */
        private readonly FloatReactiveProperty _timer = new();
        /** 高さ */
        private readonly FloatReactiveProperty _height = new();
        
        public IReadOnlyReactiveProperty<float> ClimbSpeedRP => _climbSpeed;
        public IReadOnlyReactiveProperty<float> TimerRP => _timer;
        public IReadOnlyReactiveProperty<float> HeightRP => _height;
        
        public StageState(StageMaster master)
        {
            Master = master;
            StageNum = 1;
            _timer.Value = 0;
            _height.Value = 0;
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
        
        /** プレイヤーサイズボーナス更新 */
        public void UpdatePlayerSizeSpeed(int speed)
        {
            _playerSizeSpeed = speed;
        }
        
        /** プレイヤーY座標ボーナス更新 */
        public void UpdatePlayerHeightSpeedBonus(float bonus)
        {
            _playerHeightSpeedBonus = bonus;
        }
        
        /** 速度更新 */
        public void UpdateClimbSpeed()
        {
            _climbSpeed.Value = _playerSizeSpeed * _playerHeightSpeedBonus;
        }
        
    }
}