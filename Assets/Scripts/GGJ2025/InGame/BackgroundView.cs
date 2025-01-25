using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace GGJ2025.InGame
{
    public class BackgroundView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer backgroundRenderer;
        [SerializeField] private List<Sprite> stageBackgrounds;
        
        private BackgroundState _state;
        private BackgroundUsecase _usecase;
        
        private void Awake()
        {
            _state = new BackgroundState(backgroundRenderer);
            _usecase = new BackgroundUsecase(_state);
        }
        
        public void OnStart(StageView stageView)
        {
            this.UpdateAsObservable()
                .Subscribe(_ => _usecase.UpdateBackground(Time.deltaTime))
                .AddTo(this);
            
            // 速度変更
            stageView.GetState().ClimbSpeedRP
                .Subscribe(speed => _usecase.ChangeSpeed(speed))
                .AddTo(this);
        }
        
        private void OnDestroy()
        {
            _usecase.Dispose();
        }
        
    }
}