using GGJ2025.Master;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace GGJ2025.InGame
{
    [RequireComponent(typeof(PlayerInputProvider))]
    public class PlayerView: MonoBehaviour
    {
        [SerializeField] private PlayerMaster master;
        [SerializeField] private RectTransform spriteTransform;
        
        private PlayerUsecase _usecase;
        private PlayerState _state;
        private PlayerInputProvider _inputProvider;
        
        private void Awake()
        {
            var rectTransform = GetComponent<RectTransform>();
            _state = new PlayerState(master, rectTransform, spriteTransform);
            _usecase = new PlayerUsecase(_state);
            _inputProvider = GetComponent<PlayerInputProvider>();
        }
        
        public void OnStart()
        {
            _usecase.Init();
            
            _inputProvider.HorizontalButtonHold
                .Where(x => x != 0)
                .Subscribe(_usecase.MoveHorizontal).AddTo(this);
            _inputProvider.VerticalButtonHold
                .Where(x => x != 0)
                .Subscribe(_usecase.MoveVertical).AddTo(this);
            _inputProvider.SpaceButton
                .Where(x => x)
                .Subscribe(_ => OnGameClear()).AddTo(this);
            
            
            this.UpdateAsObservable()
                .Subscribe(_ => _usecase.UpdateMove(Time.deltaTime)).AddTo(this);
            StartInput();
        }
        
        /** 入力開始 */
        public void StartInput()
        {
            _inputProvider.StartInput();
        }
        
        /** 入力停止 */
        public void StopInput()
        {
            _inputProvider.StopInput();
            _usecase.StopInput();
        }
        
        /** アイテム取得 */
        public void OnGetItem()
        {
            _usecase.AddPoint(1);
        }
        
        /** ゲームクリア */
        public void OnGameClear()
        {
            _usecase.GameClear();
            StopInput();
        }
        
        /** ヒット */
        public void OnHit()
        {
            _usecase.GameOver();
            StopInput();
        }
        
        public PlayerState GetState()
        {
            return _state;
        }

        private void OnDestroy()
        {
            _state.OnDestroy();
        }

    }
}