using System.Collections.Generic;
using GGJ2025.Master;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace GGJ2025.InGame
{
    public class StageView: MonoBehaviour
    {
        [SerializeField] private StageMaster master;
        [SerializeField] private PlayerView playerView;
        [SerializeField] private Transform obstacleParent;
        [SerializeField] private BackgroundView backgroundView;
        [SerializeField] private List<GameObject> obstaclePrefabs;
        
        private StageUsecase _usecase;
        private StageState _state;
        
        private void Awake()
        {
            _state = new StageState(master, obstaclePrefabs, obstacleParent, playerView);
            _usecase = new StageUsecase(_state);
        }

        public void StartGame()
        {
            // ステージ進行購読
            _usecase.SubscribeStageProgress(stageNum =>
            {
                _usecase.NextStage();
            }).AddTo(this);
            
            this.UpdateAsObservable().Subscribe(_ =>
            {
                _usecase.TimeProgress(Time.deltaTime);
            });
            
            playerView.GetState().PointRP.Subscribe(_usecase.UpdatePlayerPoint);
            playerView.GetState().VerticalRateRP.Subscribe(_usecase.UpdatePlayerVerticalRate);
            playerView.GetState().IsGameOverRP
                .Where(x => x)
                .Subscribe(_usecase.GameOver).AddTo(this);
            
            backgroundView.OnStart(this);
            
            // 障害物生成開始
            _usecase.StartObstacleTimer(this);
        }
        
        public void OnStart()
        {
            playerView.OnStart();
        }
        
        public StageState GetState()
        {
            return _state;
        }
    }
}