using GGJ2025.Master;
using UniRx;
using UnityEngine;

namespace GGJ2025.InGame
{
    public class PlayerState
    {
        /** ポイント */
        private readonly IntReactiveProperty _point = new();
        /** Y軸 */
        private readonly FloatReactiveProperty _verticalRate = new();
        /** ゲームオーバー */
        private readonly BoolReactiveProperty _isGameOver = new();
        /** ゲームクリア */
        private readonly BoolReactiveProperty _isGameClear = new();
        /** Spriteサイズ */
        public readonly RectTransform SpriteTransform;
        
        /** サイズ */
        public float Size { get; private set; }
        public float SizeRate { get; private set; }
        /** 速度 */
        public Vector2 Speed { get; private set; }
        /** 位置 */
        public readonly RectTransform Transform;
        /** マスター */
        public PlayerMaster Master { get; }

        public IReadOnlyReactiveProperty<int> PointRP => _point;
        public IReadOnlyReactiveProperty<float> VerticalRateRP => _verticalRate;
        public IReadOnlyReactiveProperty<bool> IsGameOverRP => _isGameOver;
        public IReadOnlyReactiveProperty<bool> IsGameClearRP => _isGameClear;

        /** 速度下限 */
        public const float MinSpeed = 10f;

        public PlayerState(PlayerMaster master, RectTransform transform, RectTransform spriteTransform)
        {
            Master = master;
            Transform = transform;
            SpriteTransform = spriteTransform;
            
            transform.sizeDelta = new Vector2(master.MaxSize, master.MaxSize);
        }
        
        /** ポイント追加 */
        public void AddPoint(int point)
        {
            _point.Value += point;
        }
        
        /** サイズ変更 */
        public void UpdateSize(float sizeRate)
        {
            Size = Mathf.Lerp(Master.MinSize / 2, Master.MaxSize / 2, sizeRate);
            var scale = Master.MinSize / Master.MaxSize + sizeRate * (1 - Master.MinSize / Master.MaxSize);
            // Debug.Log($"Size : {Size} / Scale : {scale}");
            SpriteTransform.localScale = new Vector2(scale, scale);
            SizeRate = scale;
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
        
        /** 入力停止 */
        public void StopInput()
        {
            _point.Dispose();
            _verticalRate.Dispose();
            _isGameClear.Dispose();
        }
        
        /** ゲームクリア */
        public void GameClear()
        {
            _isGameClear.Value = true;
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
            _point.Dispose();
            _verticalRate.Dispose();
            _isGameClear.Dispose();
            Object.Destroy(Transform.gameObject);
        }
        
    }
}