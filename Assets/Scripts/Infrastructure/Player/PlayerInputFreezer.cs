using System;
using UI.HUD;

namespace Infrastructure.Player
{
    public class PlayerInputFreezer : IDisposable
    {
        private readonly PlayerInput _playerInput;
        private readonly LevelProgress _levelProgress;
        private readonly SceneLoader _sceneLoader;
        private readonly GameMenu _gameMenu;

        private bool _forceFreeze;

        public PlayerInputFreezer(PlayerInput playerInput, LevelProgress levelProgress, SceneLoader sceneLoader, GameMenu menu)
        {
            _playerInput = playerInput;
            _levelProgress = levelProgress;
            _sceneLoader = sceneLoader;
            _gameMenu = menu;
            
            _levelProgress.DoorReached += OnDoorReached;
            _sceneLoader.Loaded += OnLevelLoaded;
            _gameMenu.Paused += OnGamePaused;
            _gameMenu.Unpaused += OnGameUnpaused;
        }

        private void OnDoorReached()
        {
            _forceFreeze = true;
            _playerInput.Disable();
        }

        private void OnLevelLoaded(int buildIndex)
        {
            _forceFreeze = false;
            _playerInput.Enable();
        }
        
        public void OnGamePaused() =>
            _playerInput.Disable();

        public void OnGameUnpaused()
        {
            if (_forceFreeze)
                return;
            
            _playerInput.Enable();
        }

        public void Dispose()
        {
            _levelProgress.DoorReached -= OnDoorReached;
            _sceneLoader.Loaded -= OnLevelLoaded;
            _gameMenu.Paused -= OnGamePaused;
            _gameMenu.Unpaused -= OnGameUnpaused;
        }
    }
}