using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace GGJ2025.InGame
{
    public class PlayerInputProvider: MonoBehaviour
    {
        private CompositeDisposable _buttonDisposables;
        private FloatReactiveProperty _horizontalButtonHold;
        private FloatReactiveProperty _verticalButtonHold;
        private BoolReactiveProperty _pauseButton;

        public IReadOnlyReactiveProperty<float> HorizontalButtonHold => _horizontalButtonHold;
        public IReadOnlyReactiveProperty<float> VerticalButtonHold => _verticalButtonHold;
        public IReadOnlyReactiveProperty<bool> PauseButton => _pauseButton;

        private void Awake()
        {
            _buttonDisposables = new CompositeDisposable();
            _horizontalButtonHold = new FloatReactiveProperty(0).AddTo(this);
            _verticalButtonHold = new FloatReactiveProperty(0).AddTo(this);
            _pauseButton = new BoolReactiveProperty(false).AddTo(this);
        }
        
        public void StartInput()
        {
            // 水平キー購読
            this.UpdateAsObservable()
                .Select(_ => Input.GetAxisRaw("Horizontal"))
                .Subscribe(input => _horizontalButtonHold.SetValueAndForceNotify(input)).AddTo(_buttonDisposables);
            
            // 垂直キー購読
            this.UpdateAsObservable()
                .Select(_ => Input.GetAxisRaw("Vertical"))
                .Subscribe(input => _verticalButtonHold.SetValueAndForceNotify(input)).AddTo(_buttonDisposables);
            
            // ポーズキー購読
            this.UpdateAsObservable().
                Select(_ => Input.GetKeyDown(KeyCode.Escape))
                .SkipWhile(input => input)
                .Subscribe(input => _pauseButton.Value = input).AddTo(_buttonDisposables);
        }
        
        /** 操作中断 */
        public void StopInput()
        {
            _buttonDisposables.Clear();
        }
        
        /** 入力初期化 */
        public void DefaultInput()
        {
            _horizontalButtonHold.Value = 0;
            _verticalButtonHold.Value = 0;
            _pauseButton.Value = false;
        }
    }
}