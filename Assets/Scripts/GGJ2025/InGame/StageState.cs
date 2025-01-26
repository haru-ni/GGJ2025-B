using System.Collections.Generic;
using GGJ2025.Master;
using UniRx;
using UnityEngine;

namespace GGJ2025.InGame
{
    public class StageState
    {
        /** マスター */
        public readonly StageMaster Master;
        /** プレハブ */
        public readonly List<GameObject> ObstaclePrefabs;
        /** アイテム */
        public readonly GameObject ItemPrefab;
        /** 障害物親 */
        public readonly Transform ObstacleParent;
        /** プレイヤー */
        public readonly PlayerView PlayerView;
        /** クリアアイコン */
        public readonly GameObject ClearIcon;
        /** ゲームオーバーアイコン */
        public readonly GameObject GameOverIcon;

        /** 速度 */
        private readonly FloatReactiveProperty _climbSpeed = new();
        /** 時間 */
        private readonly FloatReactiveProperty _timer = new();
        /** 時間ボーナス */
        private readonly FloatReactiveProperty _timeBonus = new();
        /** 座標ボーナス */
        private readonly FloatReactiveProperty _playerPosBonus = new();
        /** 高さ */
        private readonly FloatReactiveProperty _height = new();
        
        public IReadOnlyReactiveProperty<float> ClimbSpeedRP => _climbSpeed;
        public IReadOnlyReactiveProperty<float> TimerRP => _timer;
        public IReadOnlyReactiveProperty<float> TimeBonusRP => _timeBonus;
        public IReadOnlyReactiveProperty<float> PlayerPosBonusRP => _playerPosBonus;
        public IReadOnlyReactiveProperty<float> HeightRP => _height;
        
        public StageState(StageMaster master, List<GameObject> obstaclePrefabs, GameObject itemPrefab, Transform parent, PlayerView playerView, GameObject clearIcon, GameObject gameOverIcon)
        {
            Master = master;
            ObstaclePrefabs = obstaclePrefabs;
            ItemPrefab = itemPrefab;
            ObstacleParent = parent;
            PlayerView = playerView;
            _timer.Value = 0;
            _timeBonus.Value = 1;
            _height.Value = 0;
            ClearIcon = clearIcon;
            clearIcon.SetActive(false);
            GameOverIcon = gameOverIcon;
            gameOverIcon.SetActive(false);
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
        
        /** 時間ボーナス更新 */
        public void UpdateTimeBonus(float bonus)
        {
            _timeBonus.Value = bonus;
        }
        
        /** プレイヤーサイズボーナス更新 */
        public void UpdatePlayerSizeSpeed(float speed)
        {
            _climbSpeed.Value = speed;
        }
        
        /** プレイヤーY座標ボーナス更新 */
        public void UpdatePlayerHeightSpeedBonus(float bonus)
        {
            _playerPosBonus.Value = bonus;
        }
        
        /** ゲームオーバー */
        public void GameOver()
        {
            _climbSpeed.Dispose();
            _timer.Dispose();
            _height.Dispose();
        }
        
    }
}