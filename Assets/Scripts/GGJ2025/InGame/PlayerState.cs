using GGJ2025.Master;
using UniRx;
using UnityEngine;

namespace GGJ2025.InGame
{
    public class PlayerState
    {
        /** グレード */
        private readonly IntReactiveProperty _grade = new();
        /** Y軸 */
        private readonly FloatReactiveProperty _verticalRate = new();
        /** ゲームオーバー */
        private readonly BoolReactiveProperty _isGameOver = new();
        
        /** サイズ */
        public Vector2 Size { get; private set; }
        /** 速度 */
        public Vector2 Speed { get; private set; }
        /** 位置 */
        public readonly Transform Transform;
        /** マスター */
        public PlayerMaster Master { get; private set; }

        public IReadOnlyReactiveProperty<int> GradeRP => _grade;
        public IReadOnlyReactiveProperty<float> VerticalRateRP => _verticalRate;
        public IReadOnlyReactiveProperty<bool> IsGameOverRP => _isGameOver;

        /** 速度下限 */
        public const float MinSpeed = 10f;

        public PlayerState(PlayerMaster master, Transform transform)
        {
            Master = master;
            Transform = transform;
        }
        
        /** グレード変更 */
        public void ChangeGrade(int grade)
        {
            _grade.Value = grade;
        }
        
        /** サイズ変更 */
        public void ChangeSize(Vector2 size)
        {
            Size = size;
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
            _verticalRate.Value = (nextPos.y + (float)GameConst.StageHeight / 2) / GameConst.StageHeight;
        }
        
        /** 破棄 */
        public void Dispose()
        {
            _isGameOver.Value = true;
            _isGameOver.Dispose();
            _grade.Dispose();
            _verticalRate.Dispose();
            Object.Destroy(Transform.gameObject);
        }
        
    }
}