using System;
using UniRx;

namespace GGJ2025
{
    public class FloatWrapper
    {
        public float Value { get; set; }
        
        public FloatWrapper(float value)
        {
            Value = value;
        }
    }
    
    public static class RxUtils
    {
        
        /** RPを使用した一定のタイマーObservable生成 */
        public static IObservable<float> RepeatTimerObservable(this IReadOnlyReactiveProperty<float> timerRP, float waitTime)
        {
            return Observable.CreateWithState<float, float>(waitTime, (fireTime, observer) =>
            {
                return timerRP
                    .Where(time => time > fireTime)
                    .Skip(1)
                    .Subscribe(time =>
                    {
                        observer.OnNext(time);
                        fireTime = time + waitTime;
                    });
            });
        }
        
        public static IObservable<float> RepeatTimerObservable(this IReadOnlyReactiveProperty<float> timerRP, FloatWrapper waitTime)
        {
            return Observable.CreateWithState<float, float>(waitTime.Value, (fireTime, observer) =>
            {
                return timerRP
                    .Where(time => time > fireTime)
                    .Skip(1)
                    .Subscribe(time =>
                    {
                        observer.OnNext(time);
                        fireTime = time + waitTime.Value;
                    });
            });
        }
    }
}