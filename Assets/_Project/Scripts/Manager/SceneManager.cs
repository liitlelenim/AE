using AE.Manager.Locator;
using UnityEngine;

namespace AE.Manager
{
    public class SceneManager : MonoBehaviour, IManager
    {
        private const int MainMenuSceneIndex = 0;
        private const int MainSceneIndex = 1;

        public void LoadMainMenuScene() => UnityEngine.SceneManagement.SceneManager.LoadScene(MainMenuSceneIndex);
        public void LoadMainScene() => UnityEngine.SceneManagement.SceneManager.LoadScene(MainSceneIndex);
    }
}