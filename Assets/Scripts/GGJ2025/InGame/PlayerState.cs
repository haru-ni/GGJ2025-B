using UnityEngine;

namespace GGJ2025.InGame
{
    public class PlayerState
    {
        /** 基本速度 */
        public readonly float BaseSpeed = 300f;
        /** 速度減衰率 */
        public readonly float DampingRate = 0.02f;
        /** 速度下限 */
        public readonly float MinSpeed = 0.1f;
        
        public readonly Transform Transform;
        public Vector2 Speed { get; private set; }

        public PlayerState(Transform transform)
        {
            Transform = transform;
        }
        
        /** 左右スピード */
        public void SetHorizontalSpeed(float x)
        {
            Speed = new Vector2(x, Speed.y);
        }
        
        /** 上下スピード */
        public void SetVerticalSpeed(float y)
        {
            Speed = new Vector2(Speed.x, y);
        }
        
        /** 移動 */
        public void Move(Vector2 nextPos)
        {
            Transform.localPosition = nextPos;
        }
        
    }
}