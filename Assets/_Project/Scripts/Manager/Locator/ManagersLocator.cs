using System.Linq;
using UnityEngine;

namespace AE.Manager.Locator
{
    public class ManagersLocator : MonoBehaviour
    {
        public static ManagersLocator Instance => _instance;
        private static ManagersLocator _instance;

        private IManager[] _cachedManagers;

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Debug.LogError("Multiple ManagersLocator instances found. Destroying duplicate.", this);
                Destroy(gameObject);
                return;
            }

            _instance = this;
            LoadManagers();
        }


        public T GetManager<T>() where T : MonoBehaviour, IManager
        {
            foreach (var manager in _cachedManagers)
            {
                if (manager is T typedManager) return typedManager;
            }

            Debug.LogError($"Could not find manager of type {typeof(T).Name}", this);
            return null;
        }

        private void LoadManagers()
        {
            _cachedManagers = GetComponentsInChildren<IManager>(true);

            if (_cachedManagers == null || _cachedManagers.Length == 0)
            {
                Debug.LogWarning("No managers were found on this locator", this);
            }

            if (_cachedManagers?.Distinct().Count() != _cachedManagers?.Length)
            {
                Debug.LogError("There are duplicate managers found, this may cause further unexpected errors", this);
            }
        }
    }
}