using UnityEngine;

namespace GGJ2025.InGame
{
    public class ObstacleState
    {
        public bool IsActive { get; private set; }
        /** 位置 */
        public readonly RectTransform Transform;
        /** 速度 */
        public float Speed { get; private set; }

        public ObstacleState(Transform transform)
        {
            IsActive = true;
            Transform = transform as RectTransform;
        }
        
        public void SetActive(bool isActive)
        {
            IsActive = isActive;
        }
        
        /** 速度変更 */
        public void ChangeSpeed(float speed)
        {
            Speed = speed;
        }
        
        /** 移動 */
        public void Move(Vector2 nextPosition)
        {
            Transform.localPosition = nextPosition;
        }
        
        /** 回転 */
        public void Rotate(float angle)
        {
            Transform.localRotation = Quaternion.Euler(0, 0, angle);
        }
        
        /** ゲームオーバー */
        public void OnGameOver()
        {
            IsActive = false;
        }
        
    }
}