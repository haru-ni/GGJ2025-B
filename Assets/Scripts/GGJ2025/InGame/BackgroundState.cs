using UnityEngine;

namespace GGJ2025.InGame
{
    public class BackgroundState
    {
        
        private readonly SpriteRenderer _renderer;
        private readonly Material _material;
        private float _speed;
        
        public BackgroundState (SpriteRenderer renderer)
        {
            _renderer = renderer;
            _material = renderer.material;
        }

        /** 更新 */
        public void OnUpdate(float progress)
        {
            var offset = _material.mainTextureOffset;
            offset.y += progress * _speed / 800f;
            _material.mainTextureOffset = offset;
        }
        
        /** 速度変更 */
        public void ChangeSpeed(float speed)
        {
            _speed = speed;
        }
        
        /** 背景変更 */
        public void ChangeBackground(Sprite sprite)
        {
            _renderer.sprite = sprite;
        }
        
        public void Dispose()
        {
            Object.Destroy(_material);
        }

    }
}