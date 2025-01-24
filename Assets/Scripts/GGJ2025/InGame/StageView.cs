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
        
    }
}