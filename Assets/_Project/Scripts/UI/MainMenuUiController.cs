using AE.Manager;
using AE.Manager.Locator;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AE.UI
{
    public class MainMenuUiController : MonoBehaviour
    {
        [SerializeField] private int fadeInDurationMiliseconds = 1000;
        
        private FaderManager _faderManager;
        private SceneManager _sceneManager;
        private async void Awake()
        {
            await UniTask.Delay(fadeInDurationMiliseconds);
            await UniTask.WaitUntil(() => Keyboard.current.anyKey.isPressed);
            _faderManager = ManagersLocator.Instance.GetManager<FaderManager>();
            _sceneManager = ManagersLocator.Instance.GetManager<SceneManager>();
            
            _faderManager.FadeOut(LoadMainScene);
        }

        private void LoadMainScene()
        {
            _sceneManager.LoadMainScene();
        }
    }
}
