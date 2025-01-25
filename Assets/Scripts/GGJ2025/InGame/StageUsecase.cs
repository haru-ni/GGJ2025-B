using System;
using UniRx;
using UnityEngine;

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
        public void UpdatePlayerPoint(int point)
        {
            _state.UpdatePlayerSizeSpeed(1 + _state.Master.PointSpeed * point);
            _state.UpdateClimbSpeed();
        }
        
        /** プレイヤーY座標更新 */
        public void UpdatePlayerVerticalRate(float verticalRate)
        {
            var speedBonusIndex = _state.Master.PlayerHeightList.FindRangeIndexBinarySearch(verticalRate);
            if (speedBonusIndex != -1)
            {
                _state.UpdatePlayerHeightSpeedBonus(_state.Master.PlayerHeightSpeedBonusList[speedBonusIndex]);
                _state.UpdateClimbSpeed();
            }
        }
        
        /** 次ステージへ */
        public void NextStage()
        {
            _state.NextStage();
        }
        
        /** 障害物生成開始 */
        public void StartObstacleTimer(StageView stageView)
        {
            const float waitTime = 3.0f;
            Observable.CreateWithState<float, float>(waitTime, (fireTime, observer) =>
            {
                return _state.TimerRP
                    .Where(time => time > fireTime)
                    .Skip(1)
                    .Subscribe(time =>
                    {
                        observer.OnNext(time);
                        fireTime = time + waitTime;
                    });
            }).Subscribe(_ =>
            {
                CreateObstacle(stageView, true);
                CreateObstacle(stageView, false);
            });
        }
        
        /** 障害物生成 */
        private void CreateObstacle(StageView stageView, bool isLeft)
        {
            // 障害物生成
            // 0 ~ 2の乱数生成
            // var randomIndex = (_state.StageNumRP.Value - 1) * 2 + UnityEngine.Random.Range(0, 3);
            var randomIndex = 0;
            var obstacle = UnityEngine.Object.Instantiate(_state.ObstaclePrefabs[randomIndex], _state.ObstacleParent);
            var obstacleView = obstacle.GetComponent<ObstacleView>();
            obstacleView.OnStart(_state.PlayerView, stageView, isLeft);
        }
        
        /** ステージ経過購読 */
        public IDisposable SubscribeStageProgress(Action<int> stageProgressAction)
        {
            return _state.HeightRP.Subscribe(height =>
            {
                var stageLength = _state.Master.StageLength[_state.StageNumRP.Value - 1];
                if (height >= stageLength)
                {
                    stageProgressAction(_state.StageNumRP.Value);
                }
            });
        }
        
        /** ゲームオーバー */
        public void GameOver(bool isGameOver)
        {
            _state.GameOver();
        }
        
    }
}