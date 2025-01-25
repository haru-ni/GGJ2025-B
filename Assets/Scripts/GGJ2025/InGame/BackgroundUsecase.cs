using UnityEngine;

namespace GGJ2025.InGame
{
    public class BackgroundUsecase
    {
        
        private readonly BackgroundState _state;
        
        public BackgroundUsecase(BackgroundState state)
        {
            _state = state;
        }
        
        /** 背景更新 */
        public void UpdateBackground(float progress)
        {
            _state.OnUpdate(progress);
        }
        
        /** 速度変更 */
        public void ChangeSpeed(float speed)
        {
            _state.ChangeSpeed(speed);
        }
        
        /** 背景変更 */
        public void ChangeBackground(Sprite sprite)
        {
            _state.ChangeBackground(sprite);
        }
        
        public void Dispose()
        {
            _state.Dispose();
        }
        
    }
}