using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace GGJ2025.Title
{
    public class TitleView : MonoBehaviour
    {
        
        private readonly Subject<Unit> _enterSubject = new();
        public IObservable<Unit> EnterObservable => _enterSubject;
        
        private void OnInputSpace()
        {
            Debug.Log("Space");
            _enterSubject.OnNext(Unit.Default);
        }
        
        public void OnStart()
        {
            _enterSubject.AddTo(this);
            
            this.UpdateAsObservable()
                .Select(_ => Input.GetKeyDown(KeyCode.Space))
                .SkipWhile(input => input)
                .Where(input => input)
                .Subscribe(_ => OnInputSpace()).AddTo(this);
        }
    }
}