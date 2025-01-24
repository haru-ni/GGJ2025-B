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
        
        /** 左右入力 */
        public void MoveHorizontal(float x)
        {
            var speedX = x * _state.BaseSpeed;
            _state.SetHorizontalSpeed(speedX);
        }
        
        /** 上下入力 */
        public void MoveVertical(float y)
        {
            var speedY = y * _state.BaseSpeed;
            _state.SetVerticalSpeed(speedY);
        }

        /** 更新移動 */
        public void UpdateMove(float deltaTime)
        {
            // 速度減衰
            var nextSpeedX = Mathf.Abs(_state.Speed.x) < _state.MinSpeed
                ? 0
                : _state.Speed.x * Mathf.Pow(_state.DampingRate, deltaTime);
            var nextSpeedY = Mathf.Abs(_state.Speed.y) < _state.MinSpeed
                ? 0
                : _state.Speed.y * Mathf.Pow(_state.DampingRate, deltaTime);
            _state.SetHorizontalSpeed(nextSpeedX);
            _state.SetVerticalSpeed(nextSpeedY);
            
            // 移動
            var currentPos = _state.Transform.localPosition;
            var nextPos = new Vector2(currentPos.x + _state.Speed.x * deltaTime, currentPos.y + _state.Speed.y * deltaTime);
            _state.Move(nextPos);
            
        }

    }
}