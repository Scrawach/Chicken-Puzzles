using System;
using Hero;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace Infrastructure.Levels
{
    public class EndGameSeeker : IDisposable
    {
        private readonly SceneLoader _sceneLoader;
        private FlyOutScreen _flyOutScreen;
        public event Action Ended;

        public EndGameSeeker(SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
            _sceneLoader.Loaded += OnSceneLoaded;
        }

        private void OnSceneLoaded(int buildIndex)
        {
            if (_sceneLoader.IsLastScene(buildIndex) == false) 
                return;
            
            _flyOutScreen = FlyOutComponent();
            _flyOutScreen.Happened += OnFlyOutHappened;
        }

        public void Dispose()
        {
            if (_flyOutScreen != null)
                _flyOutScreen.Happened -= OnFlyOutHappened;
            
            _sceneLoader.Loaded -= OnSceneLoaded;
        }

        private void OnFlyOutHappened()
        {
            _flyOutScreen.Happened -= OnFlyOutHappened;
            Ended?.Invoke();
        }

        private static FlyOutScreen FlyOutComponent() =>
            Object.FindObjectOfType<FlyOutScreen>() ??
            throw new Exception("WHERE IS FLY COMPONENT FOR GAME OVER?!");
    }
}