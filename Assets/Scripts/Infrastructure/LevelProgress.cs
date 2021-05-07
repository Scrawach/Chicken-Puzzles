using System;
using System.Collections;
using Achievement;
using Infrastructure.Abstract;
using Infrastructure.Levels;
using Infrastructure.Player;
using Logic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Infrastructure
{
    public class LevelProgress : IDisposable
    {
        private readonly SceneLoader _sceneLoader;
        private readonly DoorSeeker _doorSeeker;
        private readonly Victory _victory;
        private readonly StepCounterSeeker _steps;
        private readonly EndGameSeeker _endGameSeeker;

        public event Action DoorReached;
        public event Action GameEnded;
        public event Action<int, int> Passed;

        public LevelProgress(ICoroutineRunner coroutineRunner, SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;

            _doorSeeker = new DoorSeeker(_sceneLoader);
            _victory = new Victory(coroutineRunner, _doorSeeker);
            _steps = new StepCounterSeeker(_sceneLoader);
            _endGameSeeker = new EndGameSeeker(_sceneLoader);

            _doorSeeker.DoorReached += OnDoorReached;
            _victory.Happened += OnVictoryHappened;
            _endGameSeeker.Ended += OnGameEnded;
        }

        public void Break() => 
            _victory.Break();

        public void Dispose()
        {
            _doorSeeker.DoorReached -= OnDoorReached;
            _victory.Happened -= OnVictoryHappened;
            _endGameSeeker.Ended -= OnGameEnded;
            
            _doorSeeker.Dispose();
            _victory.Dispose();
            _endGameSeeker.Dispose();
        }

        private void OnDoorReached() => 
            DoorReached?.Invoke();

        private void OnVictoryHappened() => 
            Passed?.Invoke(_sceneLoader.CurrentSceneBuild(), _steps.Counter.Score);

        private void OnGameEnded() => 
            GameEnded?.Invoke();
    }
}