using System;
using Infrastructure;
using UI.Effects;
using UI.HUD;
using UnityEngine;

namespace UI
{
    public class LastWord : FadeElement
    {
        [SerializeField] private NextScene _restartGameButton;
        private SceneLoader _sceneLoader;
        private LevelProgress _levelProgress;

        public void Construct(SceneLoader sceneLoader, LevelProgress levelProgress)
        {
            Unsubscribe();
            
            _sceneLoader = sceneLoader;
            _levelProgress = levelProgress;
            
            _sceneLoader.Loaded += OnLevelLoaded;
            _levelProgress.GameEnded += OnGameEnded;
            
            _restartGameButton.Construct(_sceneLoader);
        }
        
        private void OnEnable() => 
            FadeInEnded += OnFadeIntEnded;

        private void OnDisable() => 
            FadeInEnded -= OnFadeIntEnded;

        private void OnDestroy() => 
            Unsubscribe();

        private void Unsubscribe()
        {
            if (_levelProgress != null)
                _levelProgress.GameEnded -= OnGameEnded;

            if (_sceneLoader != null)
                _sceneLoader.Loaded -= OnLevelLoaded;
        }

        private void OnFadeIntEnded()
        {
            _restartGameButton.Show();
            _restartGameButton.UnlockButton();
        }

        private void OnGameEnded()
        {
            Canvas.blocksRaycasts = true;
            
            Show();
        }

        private void OnLevelLoaded(int buildIndex)
        {
            Canvas.blocksRaycasts = false;
            
            Hide();
            _restartGameButton.Hide();
            _restartGameButton.LockButton();
        }
    }
}