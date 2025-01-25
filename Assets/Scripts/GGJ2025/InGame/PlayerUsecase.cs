using UnityEngine;

namespace GGJ2025.InGame
{
    public class PlayerUsecase
    {
        
        private readonly PlayerState _state;
        
        public PlayerUsecase(PlayerState state)
        {
            _state = state;
        }
        
        /** 初期化 */
        public void Init()
        {
            var initGrade = 1;
            _state.ChangeGrade(initGrade);
            _state.ChangeSize(_state.Master.SizeList[initGrade - 1]);
        }
        
        /** グレード変更 */
        public void AddGrade(int grade)
        {
            var nextGrade = _state.GradeRP.Value + grade;
            if (nextGrade > _state.Master.MaxGrade)
            {
                return;
            }
            _state.ChangeGrade(nextGrade);
            _state.ChangeSize(_state.Master.SizeList[nextGrade - 1]);
        }
        
        /** 左右入力 */
        public void MoveHorizontal(float x)
        {
            var speedX = x * _state.Master.BaseSpeed;
            _state.SetHorizontalSpeed(speedX);
        }
        
        /** 上下入力 */
        public void MoveVertical(float y)
        {
            var speedY = y * _state.Master.BaseSpeed;
            _state.SetVerticalSpeed(speedY);
        }

        /** 更新移動 */
        public void UpdateMove(float deltaTime)
        {
            // 速度減衰
            var nextSpeedX = Mathf.Abs(_state.Speed.x) < PlayerState.MinSpeed
                ? 0
                : Mathf.Lerp(_state.Speed.x, 0, _state.Master.DampingRate);
            var nextSpeedY = Mathf.Abs(_state.Speed.y) < PlayerState.MinSpeed
                ? 0
                : Mathf.Lerp(_state.Speed.y, 0, _state.Master.DampingRate);
            _state.SetHorizontalSpeed(nextSpeedX);
            _state.SetVerticalSpeed(nextSpeedY);
            
            // 移動
            var currentPos = _state.Transform.localPosition;
            var nextPosX = Mathf.Clamp(currentPos.x + _state.Speed.x * deltaTime, -GameConst.StageWidth / 2 + _state.Size.x / 2, GameConst.StageWidth / 2 - _state.Size.x / 2);
            var nextPosY = Mathf.Clamp(currentPos.y + _state.Speed.y * deltaTime, -GameConst.StageHeight / 2 + _state.Size.y / 2, GameConst.StageHeight / 2 - _state.Size.y / 2);
            var nextPos = new Vector2(nextPosX, nextPosY);
            
            _state.Move(nextPos);
            
        }

    }
}