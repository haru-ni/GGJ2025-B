using System.Collections.Generic;
using UnityEngine;

namespace GGJ2025.Sounds
{
    public class SeManager : MonoBehaviour
    {
        /** SEの呼び出し情報クラス */
        private class SeCallInfo
        {
            public int Id;
            public AudioClip Clip;  
            /** 再生済みフラグ */
            public bool IsDone;
            /** 再生候補になってからの経過フレーム数 */
            public int FrameCount;
        }
        
        /** アプリケーションポーズ */
        private bool _isApplicationPause;
        
        /** 音量 */
        private float _seVolume;
        /** コンフィグ */
        private float _configSeVolume;
        /** SEオーディオソース */
        private AudioSource _seAudioSource;
        private Dictionary<int, AudioClip> _seDictionary;
        
        /** 再生を遅延させるフレーム数 */
        private const int DelayFrameCount = 3;
        /** 再生を予約できる最大数 */
        private const int MaxQueuedCount  = 1;
        /** 呼び出しテーブル */
        private Dictionary<int, Queue<SeCallInfo>> _callTable;
        
        /** SE音量変更 */
        private void OnChangeAudioVolume()
        {
            _seAudioSource.volume = _seVolume * _configSeVolume;
        }

        private void Update()
        {
            //SE
            foreach (var queue in _callTable.Values)
            {
                if (queue.Count == 0)
                {
                    continue;
                }
                
                while (true)
                {
                    if (queue.Count == 0) break;
                    if (queue.Peek().IsDone)
                    {
                        queue.Dequeue();
                    }
                    else
                    {
                        break;
                    }
                }

                if (queue.Count == 0)
                {
                    continue;
                }

                var info = queue.Peek();
                info.FrameCount += 1;
                if (info.FrameCount > DelayFrameCount)
                {
                    _seAudioSource.PlayOneShot(info.Clip);
                    queue.Dequeue();
                }
            }
        }

        /** 呼び出し中の要素数取得 */
        private int GetCalledCount()
        {
            var count = 0;
            foreach (var queue in _callTable.Values)
            {
                count += queue.Count;
            }
            return count;
        }
        
        /** SE設定 */
        public void Set(int seId, AudioClip clip)
        {
            _seDictionary.TryAdd(seId, clip);
        }
        
        /** SE再生 */
        public void Play(SEs seId)
        {
            Play((int)seId);
        }
        public void Play(int seId)
        {
            if (!_seDictionary.ContainsKey(seId))
            {
                return;
            }
            var seClip = _seDictionary[seId];
            
            var info =  new SeCallInfo
            {
                Id = seId,
                Clip = seClip,
                IsDone = false,
                FrameCount = 0,
            };
            
            if (!_callTable.ContainsKey(seId))
            {
                // 即再生
                _seAudioSource.volume = _seVolume;
                _seAudioSource.PlayOneShot(seClip);
                info.IsDone = true;
                
                var queue = new Queue<SeCallInfo>();
                queue.Enqueue(info);
                _callTable.Add(seId, queue);
            }
            else
            {
                // 再生予約
                var queue = _callTable[seId];
                if (queue.Count < MaxQueuedCount)
                {
                    queue.Enqueue(info);
                }
            }
        }
        
        /** SE */
        public void SetSeVolume(float volume)
        {
            _seVolume = volume;
            OnChangeAudioVolume();
        }
        
        /** 再生ストップ */
        public void Stop()
        {
            _seAudioSource.Stop();
        }

        /** 初期化 */
        public void Initialize()
        {
            _seAudioSource = gameObject.AddComponent<AudioSource>();
            _seDictionary = new Dictionary<int, AudioClip>();
            _callTable = new Dictionary<int, Queue<SeCallInfo>>();
            _seVolume = 1.0f;
            _configSeVolume = 1.0f;
        }
    }
}