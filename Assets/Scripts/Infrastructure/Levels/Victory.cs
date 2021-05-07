using System;
using System.Collections;
using Infrastructure.Abstract;
using UnityEngine;

namespace Infrastructure.Levels
{
    public class Victory : IDisposable
    {
        private const float PauseBefore = 2f;
        
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly DoorSeeker _doorSeeker;

        private Coroutine _victoryProcess;
        public event Action Happened;

        public Victory(ICoroutineRunner coroutineRunner, DoorSeeker doorSeeker)
        {
            _coroutineRunner = coroutineRunner;
            _doorSeeker = doorSeeker;

            _doorSeeker.DoorReached += OnDoorReached;
        }

        public void Break()
        {
            if (_victoryProcess == null)
                return;

            _coroutineRunner.StopCoroutine(_victoryProcess);
            _victoryProcess = null;
        }

        public void Dispose() => 
            _doorSeeker.DoorReached -= OnDoorReached;

        private void OnDoorReached() => 
            _victoryProcess = _coroutineRunner.StartCoroutine(Winning(PauseBefore));

        private IEnumerator Winning(float pauseInSeconds)
        {
            yield return new WaitForSeconds(pauseInSeconds);
            Happened?.Invoke();
        }
    }
}