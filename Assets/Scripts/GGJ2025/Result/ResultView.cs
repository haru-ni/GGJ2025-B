using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace GGJ2025.Result
{
    public class ResultView : MonoBehaviour
    {
        
        private readonly Subject<Unit> _retrySubject = new();
        public IObservable<Unit> RetryObservable => _retrySubject;
        
        private void OnInputSpace()
        {
            _retrySubject.OnNext(Unit.Default);
        }
        
        public void OnStart()
        {
            _retrySubject.AddTo(this);
            
            this.UpdateAsObservable()
                .Select(_ => Input.GetKeyDown(KeyCode.Space))
                .SkipWhile(input => input)
                .Where(input => input)
                .Subscribe(_ => OnInputSpace()).AddTo(this);
        }
    }
}