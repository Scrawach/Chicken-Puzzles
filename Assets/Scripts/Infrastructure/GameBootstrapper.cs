using System;
using AppodealAds.Unity.Api;
using Data;
using Infrastructure.Abstract;
using Infrastructure.Factory;
using Infrastructure.Player;
using Scene;
using UI.HUD;
using UnityEngine;

namespace Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private PlayerInput _inputPrefab;
        [SerializeField] private GameOverZone _gameOverPrefab;
        [SerializeField] private GameMenu _hudPrefab;

        private Game _game;
        private PlayerInputFreezer _freezer;
        
        private void Awake()
        {
            var requirement = Resources.Load<LevelStepRequirement>(AssetPath.Requirement);
            
            InitGame(requirement);
            DontDestroyOnLoad(this);
        }
        
        private void OnDestroy()
        {
            _game.Dispose();
            _freezer.Dispose();
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            _game.SaveProgress();
        }

        private void OnApplicationQuit()
        {
            _game.SaveProgress();
        }

        private void InitGame(LevelStepRequirement requirement)
        {
            _game = new Game(this, requirement);
            CreateGameOverZone();
            
            var input = CreatePlayerInput();
            var menu = CreateHud();

            _freezer = new PlayerInputFreezer(input, _game.LevelProgress, _game.SceneLoader, menu);
        }
        
        private GameMenu CreateHud()
        {
            var menu = Instantiate(_hudPrefab);
            menu.Construct(_game.SceneLoader, _game.LevelProgress, _game.PlayerAccount);
            return menu;
        }
        
        private PlayerInput CreatePlayerInput() => 
            Instantiate(_inputPrefab, transform);

        private void CreateGameOverZone()
        {
            var gameOverZone = Instantiate(_gameOverPrefab, transform);
            gameOverZone.Construct(_game.SceneLoader, _game.LevelProgress);
        }
    }
}
