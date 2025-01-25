using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace GGJ2025.InGame
{
    public class ObstacleView : MonoBehaviour
    {
        
        private ObstacleState _state;
        private ObstacleUsecase _usecase;
        
        private void Awake()
        {
            _state = new ObstacleState(transform);
            _usecase = new ObstacleUsecase(_state);
        }
        
        public void OnStart(PlayerView playerView, StageView stageView, bool iSLeft)
        {
            _usecase.Position(iSLeft);
            
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