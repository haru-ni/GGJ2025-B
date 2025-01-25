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
            _state.UpdateSize(0);
        }
        
        /** ポイント追加 */
        public void AddPoint(int point)
        {
            _state.AddPoint(point);
            var sizeRate = Mathf.Min(1, (float)_state.PointRP.Value / _state.Master.MaxPoint);
            _state.UpdateSize(sizeRate);
        }
        
        /** 左右入力 */
        public void MoveHorizontal(float x)
        {
            var speedX = x * _state.Master.MoveSpeed;
            _state.SetHorizontalSpeed(speedX);
        }
        
        /** 上下入力 */
        public void MoveVertical(float y)
        {
            var speedY = y * _state.Master.MoveSpeed;
            _state.SetVerticalSpeed(speedY);
        }

        /** 更新移動 */
        public void UpdateMove(float deltaTime)
        {
            // 速度減衰
            var nextSpeedX = Mathf.Abs(_state.Speed.x) < PlayerState.MinSpeed
                ? 0
                : Mathf.Lerp(_state.Speed.x, 0, _state.Master.DampingRate * deltaTime);
            var nextSpeedY = Mathf.Abs(_state.Speed.y) < PlayerState.MinSpeed
                ? 0
                : Mathf.Lerp(_state.Speed.y, 0, _state.Master.DampingRate * deltaTime);
            _state.SetHorizontalSpeed(nextSpeedX);
            _state.SetVerticalSpeed(nextSpeedY);
            
            // 移動
            var currentPos = _state.Transform.localPosition;
            var nextPosX = Mathf.Clamp(currentPos.x + _state.Speed.x * deltaTime, -360F + _state.Size / 2, 360F - _state.Size / 2);
            var nextPosY = Mathf.Clamp(currentPos.y + _state.Speed.y * deltaTime, -360F + _state.Size / 2, 360F - _state.Size / 2);
            var nextPos = new Vector2(nextPosX, nextPosY);
            
            _state.Move(nextPos);
            
        }
        
        /** ゲームオーバー */
        public void GameOver()
        {
            _state.Dispose();
        }

    }
}