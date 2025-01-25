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
        public void UpdatePlayerSize(int size)
        {
            if (size < 1 || size > _state.Master.SizeSpeedList.Count)
            {
                return;
            }
            _state.UpdatePlayerSizeSpeed(_state.Master.SizeSpeedList[size - 1]);
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