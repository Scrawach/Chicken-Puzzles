using Infrastructure;
using TMPro;
using UnityEngine;

namespace UI
{
    public class LevelCounter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _counter;

        private SceneLoader _sceneLoader;
        
        public void Construct(SceneLoader sceneLoader)
        {
            Unsubscribe();
            
            _sceneLoader = sceneLoader;
            _sceneLoader.Loaded += OnLevelLoaded;
        }

        private void OnDestroy() => 
            Unsubscribe();

        private void Unsubscribe()
        {
            if (_sceneLoader != null)
                _sceneLoader.Loaded -= OnLevelLoaded;
        }

        private void OnLevelLoaded(int buildIndex) => 
            _counter.text = buildIndex.ToString();
    }
}