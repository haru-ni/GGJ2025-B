using UnityEngine;

namespace GGJ2025.InGame
{
    public class ItemState
    {
        public bool IsActive { get; private set; }
        /** 位置 */
        public readonly RectTransform Transform;
        /** 速度 */
        public float Speed { get; private set; }

        public ItemState(Transform transform)
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
        
        /** ゲームオーバー */
        public void OnGameOver()
        {
            IsActive = false;
        }
        
    }
}