using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GGJ2025.Result
{
    public class ResultController : MonoBehaviour
    {
        [SerializeField] private ResultView resultView;
        
        private void Start()
        {
            resultView.OnStart();
            resultView.RetryObservable.Subscribe(_ =>
            {
                // シーン遷移
                SceneManager.LoadScene("TitleScene");
            });
        }
        
    }
}