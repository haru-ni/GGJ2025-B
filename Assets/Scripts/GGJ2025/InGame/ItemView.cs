using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace GGJ2025.InGame
{
    public class ItemView : MonoBehaviour
    {
        
        private ItemState _state;
        private ItemUsecase _usecase;
        
        private void Awake()
        {
            _state = new ItemState(transform);
            _usecase = new ItemUsecase(_state);
        }
        
        public void OnStart(PlayerView playerView, StageView stageView)
        {
            _usecase.Position();
            
            this.UpdateAsObservable().Subscribe(_ =>
                {
                    _usecase.Fall(Time.deltaTime);
                    
                    if (_state.IsActive)
                    {
                        _usecase.Hit(playerView);
                    }
                }).AddTo(this);

            stageView.GetState().ClimbSpeedRP.Subscribe(_usecase.ChangeSpeed).AddTo(this);
            
            playerView.GetState().IsGameOverRP.
                Where(x => x).
                Subscribe(_ => _usecase.OnGameOver()).AddTo(this); 
        }
    }
}