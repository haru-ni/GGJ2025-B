using GGJ2025.Sounds;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GGJ2025.Title
{
    public class TitleController : MonoBehaviour
    {
        [SerializeField] private TitleView titleView;
        
        private void Start()
        {
            titleView.OnStart();
            titleView.EnterObservable.Subscribe(_ =>
            {
                // シーン遷移
                SceneManager.LoadScene("MainScene");
            });
            
            SoundManager.BGM.Play((int)BGMs.Main).Forget();
        }
        
    }
}