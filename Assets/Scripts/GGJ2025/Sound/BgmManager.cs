using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace GGJ2025.Sounds
{
    public class BgmManager : MonoBehaviour
    {
        /** 現在のBGM */
        private int _currentBgm;
        /** アプリケーションポーズ */
        private bool _isApplicationPause;
        /** 音量 */
        private float _bgmVolume;
        /** コンフィグ */
        private float _configBgmVolume;
        /** BGMオーディオソース */
        private AudioSource _bgmIntroAudioSource;
        private AudioSource _bgmLoopAudioSource;
        private Dictionary<int, AudioClip> _bgmIntroDictionary;
        private Dictionary<int, AudioClip> _bgmLoopDictionary;
        /** イントロ -> ループキャンセル */
        private CancellationTokenSource _bgmCancellationTokenSource;
        
        private Tween _bgmFadeTween;
        
        private void OnApplicationPause(bool pauseStatus)
        {
            _isApplicationPause = pauseStatus;
        }
        
        /** イントロ再生 */
        private async UniTaskVoid PlayBGMIntroAudioSource(CancellationToken token)
        { 
            _bgmIntroAudioSource.Play();
            
            await UniTask.WaitUntil(() => _bgmIntroAudioSource.isPlaying, PlayerLoopTiming.Update, token);
            // 再生終了待ち
            await UniTask.WaitWhile(() => _bgmIntroAudioSource.isPlaying || _isApplicationPause, PlayerLoopTiming.Update, token);
            
            // ループ開始再生
            _bgmLoopAudioSource.Play();
        }
        
        /** BGM音量変更 */
        private void OnChangeAudioVolume()
        {
            _bgmIntroAudioSource.volume = _bgmVolume * _configBgmVolume;
            _bgmLoopAudioSource.volume = _bgmVolume * _configBgmVolume;
        }
        
        /** BGM音量一時変更 */
        private void OnChangeAudioVolumeTemp(float volume)
        {
            _bgmIntroAudioSource.volume = volume * _configBgmVolume;
            _bgmLoopAudioSource.volume = volume * _configBgmVolume;
        }

        /** フェードアウト付きBGM停止 */
        private async UniTask StopBgmWithFadeOut(float fadeOutTime, CancellationToken token)
        {
            if (_bgmFadeTween != null)
            {
                _bgmFadeTween.Complete();
                _bgmFadeTween = null;
            }
            
            var currentVolume = _bgmVolume;
            _bgmFadeTween = DOTween.To(() => currentVolume, x => currentVolume = x, 0.0f, fadeOutTime)
                .OnUpdate(() =>
                {
                    OnChangeAudioVolumeTemp(currentVolume);
                })
                .OnComplete(() =>
                {
                    _bgmIntroAudioSource.Stop();
                    _bgmLoopAudioSource.Stop();
                    _bgmIntroAudioSource.clip = null;
                    _bgmLoopAudioSource.clip = null;
                    _bgmFadeTween = null;
                });
            await _bgmFadeTween.ToUniTask(cancellationToken: token);
        }

        /** BGM設定 */
        public void SetAudioClip(int bgmId, AudioClip introClip, AudioClip loopClip)
        {
            if (_bgmIntroDictionary.ContainsKey(bgmId))
            {
                return;
            }
            _bgmIntroDictionary[bgmId] = introClip;
            _bgmLoopDictionary[bgmId] = loopClip;
        }
        
        /** BGM再生 */
        public void Play(BGMs bgm, float fadeOutTime = 0f)
        {
            Play((int)bgm, fadeOutTime).Forget();
        }
        
        public async UniTaskVoid Play(int bgmId, float fadeOutTime = 0f)
        {
            Debug.Log("Play BGM1");
            if (_bgmIntroDictionary.TryGetValue(bgmId, out var introClip))
            {
                if (_bgmIntroAudioSource.clip != null && _bgmIntroAudioSource.clip.Equals(introClip)) return;
            }
            if (_bgmLoopDictionary.TryGetValue(bgmId, out var loopClip))
            {
                if (_bgmLoopAudioSource.clip != null && _bgmLoopAudioSource.clip.Equals(loopClip)) return;
            }
            
            await StopBgmWithFadeOut(fadeOutTime, _bgmCancellationTokenSource.Token);
            
            _currentBgm = bgmId;
            
            _bgmIntroAudioSource.clip = introClip;
            _bgmLoopAudioSource.clip = loopClip;
            
            OnChangeAudioVolume();
            
            if (introClip != null)
            {
                Debug.Log("Play BGM2");
                PlayBGMIntroAudioSource(_bgmCancellationTokenSource.Token).Forget();
            }
            else
            {
                Debug.Log("Play BGM3");
                _bgmLoopAudioSource.Play();
            }
        }
        
        /** BGM停止 */
        public void Stop(float fadeOutTime = 0f)
        {
            StopBgmWithFadeOut(fadeOutTime, _bgmCancellationTokenSource.Token).Forget();
        }
        
        /** BGM一時停止 */
        public void Pause()
        {
            _bgmIntroAudioSource.pitch = 0;
            _bgmLoopAudioSource.pitch = 0;
        }
        
        /** BGM再開 */
        public void Resume()
        {
           _bgmIntroAudioSource.pitch = 1;
           _bgmLoopAudioSource.pitch = 1;
        }
        
        public void Initialize()
        {
            _bgmCancellationTokenSource = new CancellationTokenSource();
            _bgmIntroAudioSource = gameObject.AddComponent<AudioSource>();
            _bgmLoopAudioSource = gameObject.AddComponent<AudioSource>();
            _bgmIntroAudioSource.loop = false;
            _bgmLoopAudioSource.loop = true;
            _bgmIntroAudioSource.playOnAwake = false;
            _bgmLoopAudioSource.playOnAwake = false;
            _bgmIntroDictionary = new Dictionary<int, AudioClip>();
            _bgmLoopDictionary = new Dictionary<int, AudioClip>();
            
            _bgmVolume = 1f;
            _configBgmVolume = 1f;
        }
        
        private void OnDestroy()
        {
            _bgmCancellationTokenSource?.Cancel();
            _bgmCancellationTokenSource?.Dispose();
            _bgmFadeTween.Kill();
        }
    }
}