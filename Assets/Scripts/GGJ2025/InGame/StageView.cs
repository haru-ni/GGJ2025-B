using System;
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
        [SerializeField] private GameObject itemPrefab;
        
        private StageUsecase _usecase;
        private StageState _state;

        private FloatWrapper _itemFloatWrapper;
        
        private void Awake()
        {
            _state = new StageState(master, obstaclePrefabs, itemPrefab, obstacleParent, playerView);
            _usecase = new StageUsecase(_state);
        }
        
        private void StartItemTimer(float waitTime)
        {
            if (_itemFloatWrapper == null)
            {
                _itemFloatWrapper = new FloatWrapper(waitTime);
                // アイテム生成開始
                _usecase.StartItemTimer(this, _itemFloatWrapper);
                return;
            }
            _itemFloatWrapper.Value = waitTime;
        }

        public void StartGame()
        {
            this.UpdateAsObservable().Subscribe(_ =>
            {
                _usecase.TimeProgress(Time.deltaTime);
            });
            
            this.UpdateAsObservable()
                .Select(_ => Input.GetKeyDown(KeyCode.Space))
                .Where(x => x)
                .FirstOrDefault()
                .Subscribe(_ => _usecase.GameClear(playerView)).AddTo(this);
            
            playerView.GetState().PointRP.Subscribe(_usecase.UpdatePlayerPoint);
            playerView.GetState().VerticalRateRP.Subscribe(_usecase.UpdatePlayerVerticalRate);
            playerView.GetState().IsGameOverRP
                .Where(x => x)
                .Subscribe(_usecase.GameOver).AddTo(this);
            
            backgroundView.OnStart(this);
            
            // 障害物生成開始
            _usecase.StartObstacleTimer(this);
            
            _state.PlayerPosBonusRP.Subscribe(StartItemTimer);
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