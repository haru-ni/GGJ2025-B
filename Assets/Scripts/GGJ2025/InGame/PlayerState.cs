using GGJ2025.Master;
using UniRx;
using UnityEngine;

namespace GGJ2025.InGame
{
    public class PlayerState
    {
        /** サイズ */
        private readonly IntReactiveProperty _size = new();
        /** Y軸 */
        private readonly FloatReactiveProperty _verticalRate = new();
        
        /** サイズ */
        public Vector2 Size { get; private set; }
        /** 速度 */
        public Vector2 Speed { get; private set; }
        /** 位置 */
        public readonly Transform Transform;
        /** マスター */
        public PlayerMaster Master { get; private set; }

        public IReadOnlyReactiveProperty<int> SizeRP => _size;
        public IReadOnlyReactiveProperty<float> VerticalRateRP => _verticalRate;
        
        /** 速度下限 */
        public readonly float MinSpeed = 10f;

        public PlayerState(PlayerMaster master, Transform transform)
        {
            Master = master;
            Transform = transform;
            Size = new Vector2(80, 80);
        }
        
        /** サイズ変更 */
        public void ChangeSize(int size)
        {
            _size.Value = size;
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
            _verticalRate.Value = nextPos.y;
        }
        
    }
}