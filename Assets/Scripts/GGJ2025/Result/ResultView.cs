using System;
using GGJ2025.UI;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace GGJ2025.Result
{
    public class ResultView : MonoBehaviour
    {
        
        [SerializeField] private GameObject gameOverGroup;
        [SerializeField] private GameObject gameClearGroup;
        [SerializeField] private Transform bubbleTransform;
        /** スコア表示 */
        [SerializeField] private TextWrapper pointText;
        [SerializeField] private TextWrapper timeText;
        [SerializeField] private TextWrapper timeRateText;
        [SerializeField] private TextWrapper scoreText;
        /** リザルト表示 */
        
        private readonly Subject<Unit> _retrySubject = new();
        public IObservable<Unit> RetryObservable => _retrySubject;
        
        private void OnInputSpace()
        {
            _retrySubject.OnNext(Unit.Default);
        }
        
        private void OnGameOver()
        {
            gameOverGroup.SetActive(true);
        }
        
        private void OnGameClear()
        {
            gameClearGroup.SetActive(true);

            Debug.Log(ScoreManager.SizeRate);
            bubbleTransform.localScale = new Vector2(ScoreManager.SizeRate, ScoreManager.SizeRate);
            
            pointText.SetText(ScoreManager.Point);
            timeText.SetText(ScoreManager.Time);
            // 0.x表示
            var timerBonus = ScoreManager.TimerBonus.ToString("F1");
            timeRateText.SetText(timerBonus);
            scoreText.SetText((int)Math.Floor(ScoreManager.Point * ScoreManager.TimerBonus));
        }
        
        public void OnStart()
        {
            _retrySubject.AddTo(this);
            
            this.UpdateAsObservable()
                .Select(_ => Input.GetKeyDown(KeyCode.Space))
                .SkipWhile(input => input)
                .Where(input => input)
                .Subscribe(_ => OnInputSpace()).AddTo(this);

            gameOverGroup.SetActive(false);
            gameClearGroup.SetActive(false);
            
            if (ScoreManager.IsClear)
            {
                OnGameClear();
            }
            else
            {
                OnGameOver();
            }
        }
    }
}