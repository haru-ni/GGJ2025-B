using UnityEngine;

namespace GGJ2025.InGame
{
    public class ObstacleUsecase
    {
        
        private readonly ObstacleState _state;
        
        public ObstacleUsecase(ObstacleState state)
        {
             _state = state;
        }
        
        /** 位置設定 */
        public void Position(bool isLeft)
        {
            // 左詰め、右詰めの位置設定
            var rect = _state.Transform;
            var stageHalfWidth = GameConst.StageWidth / 2;
            var posX = isLeft ? -stageHalfWidth + rect.sizeDelta.x / 2 : stageHalfWidth - rect.sizeDelta.x / 2;
            var posY = GameConst.StageHeight / 2 + 100;
            _state.Move(new Vector2(posX, posY));
            
            if (!isLeft)
            {
                _state.Rotate(180);
            }
        }
        
        /** 速度変更 */
        public void ChangeSpeed(float speed)
        {
            _state.ChangeSpeed(speed);
        }
        
        /** 落下 */
        public void Fall(float deltaTime)
        {
            var currentPosition = _state.Transform.localPosition;
            var nextPosY = currentPosition.y - _state.Speed * deltaTime;
            _state.Move(new Vector2(currentPosition.x, nextPosY));
            
            if (currentPosition.y < -460F)
            {
                Object.Destroy(_state.Transform.gameObject);
            }
        }
        
        /** 衝突判定 */
        public void Hit(PlayerView playerView)
        {
            bool IsCircleCollidingWithRectangle(Vector2 circleCenter, float circleRadius, Vector2 rectCenter, Vector2 rectSize)
            {
                // 矩形の範囲内で円の中心をクランプ
                float closestX = Mathf.Clamp(circleCenter.x, rectCenter.x - rectSize.x / 2, rectCenter.x + rectSize.x / 2);
                float closestY = Mathf.Clamp(circleCenter.y, rectCenter.y - rectSize.y / 2, rectCenter.y + rectSize.y / 2);

                // クランプされた点（矩形の最も近い点）
                Vector2 closestPoint = new Vector2(closestX, closestY);

                // 円の中心とクランプされた点の距離を計算
                float distance = Vector2.Distance(circleCenter, closestPoint);

                // 距離が半径以下であれば衝突している
                return distance <= circleRadius;
            }
            
            // playerのサイズ(円)と障害物のRectTransform(矩形)で判定
            var playerSize = playerView.GetState().Size;
            var playerPos = playerView.GetState().Transform.localPosition;
            var obstaclePos = _state.Transform.localPosition;
            var obstacleSize = _state.Transform.sizeDelta;

            if (IsCircleCollidingWithRectangle(playerPos, playerSize, obstaclePos, obstacleSize))
            {
                playerView.OnHit();
            }
        }

        /** プレイヤー死亡 */
        public void OnGameOver()
        {
            _state.OnGameOver();
        }

    }
}