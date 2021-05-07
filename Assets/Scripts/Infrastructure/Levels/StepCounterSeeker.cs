using System;
using Achievement;
using Object = UnityEngine.Object;

namespace Infrastructure.Levels
{
    public class StepCounterSeeker : IDisposable
    {
        private readonly SceneLoader _sceneLoader;
        private StepCounter _counter;

        public StepCounter Counter => _counter;

        public StepCounterSeeker(SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
            _sceneLoader.Loaded += OnSceneLoaded;
        }

        public void Dispose() => 
            _sceneLoader.Loaded -= OnSceneLoaded;

        private void OnSceneLoaded(int buildIndex) => 
            _counter = CounterOnLevel();

        private StepCounter CounterOnLevel() =>
            Object.FindObjectOfType<StepCounter>() ??
            throw new Exception("WHERE IS STEP COUNTER, LEBOWSKI?!");
    }
}