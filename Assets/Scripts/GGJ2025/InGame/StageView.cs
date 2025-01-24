using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace GGJ2025.InGame
{
    public class StageView: MonoBehaviour
    {
        
        private StageUsecase _usecase;
        private StageState _state;
        
        private void Awake()
        {
            _state = new StageState();
            _usecase = new StageUsecase(_state);
        }

        public void StartGame(PlayerView playerView)
        {
            // ステージ進行購読
            _usecase.SubscribeStageProgress(stageNum =>
            {
                Debug.Log($"Stage {stageNum}");
                _usecase.NextStage();
            });
            
            this.UpdateAsObservable().Subscribe(_ =>
            {
                _usecase.TimeProgress(Time.deltaTime);
            });
            
            playerView.GetState().SizeRP.Subscribe(_usecase.UpdatePlayerSize);
            playerView.GetState().VerticalRateRP.Subscribe(_usecase.UpdatePlayerVerticalRate);
        }
        
        public StageState GetState()
        {
            return _state;
        }
    }
}