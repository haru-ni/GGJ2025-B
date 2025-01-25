using System;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            
            var timeBonus = Mathf.Lerp(_state.Master.MinTimeBonus, _state.Master.MaxTimeBonus, Mathf.Min(1, _state.TimerRP.Value / _state.Master.BonusMaxTime));
            _state.UpdateTimeBonus(1 - timeBonus);
        }
        
        /** プレイヤーサイズ更新 */
        public void UpdatePlayerPoint(int point)
        {
            _state.UpdatePlayerSizeSpeed(Mathf.Lerp(_state.Master.MinSpeed, _state.Master.MaxSpeed, Mathf.Min(1, point / _state.Master.MaxPointSpeed)));
        }
        
        /** プレイヤーY座標更新 */
        public void UpdatePlayerVerticalRate(float verticalRate)
        {
            var speedBonusIndex = _state.Master.PlayerHeightList.FindRangeIndexBinarySearch(verticalRate);
            if (speedBonusIndex != -1)
            {
                var interval = _state.Master.ItemBaseTime / _state.Master.PlayerHeightSpeedBonusList[speedBonusIndex];
                _state.UpdatePlayerHeightSpeedBonus(interval);
            }
        }
        
        /** 障害物生成開始 */
        public void StartObstacleTimer(StageView stageView)
        {
            const float waitTime = 3.0f;
            _state.TimerRP.RepeatTimerObservable(waitTime).Subscribe(_ =>
            {
                var index = CreateObstacle(stageView, true);
                CreateObstacle(stageView, false, index);
            });
        }
        
        /** アイテム生成開始 */
        public IDisposable StartItemTimer(StageView stageView, FloatWrapper waitTime)
        {
            return _state.TimerRP.RepeatTimerObservable(waitTime)
                .Subscribe(_ =>
            {
                CreateItem(stageView);
            });
        }
        
        /** 障害物生成 */
        private int CreateObstacle(StageView stageView, bool isLeft, int preIndex = -1)
        {
            // 障害物生成
            // 0 ~ 2の乱数生成
            var randomIndex = UnityEngine.Random.Range(0, 3);
            if (preIndex == randomIndex)
            {
                randomIndex = (randomIndex + 1) % 3;
            }
            var obstacle = UnityEngine.Object.Instantiate(_state.ObstaclePrefabs[randomIndex], _state.ObstacleParent);
            var obstacleView = obstacle.GetComponent<ObstacleView>();
            obstacleView.OnStart(_state.PlayerView, stageView, isLeft);
            return randomIndex;
        }
        
        /** アイテム生成 */
        private void CreateItem(StageView stageView)
        {
            var item = UnityEngine.Object.Instantiate(_state.ItemPrefab, _state.ObstacleParent);
            var itemView = item.GetComponent<ItemView>();
            itemView.OnStart(_state.PlayerView, stageView);
        }
        
        /** ゲームクリア */
        public void GameClear(PlayerView playerView)
        {
            _state.GameOver();
            ScoreManager.GameClear(playerView.GetState().PointRP.Value, _state.TimeBonusRP.Value);
            playerView.StopInput();
            
            Observable.Timer(TimeSpan.FromSeconds(2)).Subscribe(_ =>
            {
                SceneManager.LoadScene("ResultScene");
            });
        }
        
        /** ゲームオーバー */
        public void GameOver(bool isGameOver)
        {
            _state.GameOver();
            ScoreManager.GameOver();
            
            Observable.Timer(TimeSpan.FromSeconds(2)).Subscribe(_ =>
            {
                SceneManager.LoadScene("ResultScene");
            });
        }
        
    }
}