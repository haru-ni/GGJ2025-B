using UnityEngine;
using UnityEngine.Assertions;

namespace Common
{
    public abstract class SingletonMonoBehaviour<T>: MonoBehaviour 
        where T : MonoBehaviour
    {
        private static T _entity;
        public static T Entity
        {
            get => _entity;
            set
            {
                if (_entity != null)
                {
                    Assert.IsNotNull(value, $"Singleton設定エラーです。{typeof(T).Name}が既に存在しています。");
                }
                _entity = value;
            }
        }
    }
}
