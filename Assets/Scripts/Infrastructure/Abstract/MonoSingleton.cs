using UnityEngine;

namespace Infrastructure.Abstract
{
    public abstract class MonoSingleton<T> : MonoBehaviour where T : Component
    {
        public static T Instance;

        protected virtual void Awake()
        {
            if (IsSingletonExist())
                Destroy(gameObject);
            else
                InitInstance();
        }
        
        private static bool IsSingletonExist() => 
            Instance != null;
        
        private void InitInstance() =>
            Instance = TryGetComponent(out T component) ? component : gameObject.AddComponent<T>();
    }
}