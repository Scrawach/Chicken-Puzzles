using System;
using Logic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Infrastructure.Levels
{
    public class DoorSeeker : IDisposable
    {
        private readonly SceneLoader _sceneLoader;
        private Door _currentDoor;
        
        public event Action DoorReached;

        public DoorSeeker(SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
            _sceneLoader.Loaded += OnSceneLoaded;
        }

        public void Dispose() => 
            _sceneLoader.Loaded -= OnSceneLoaded;

        private void OnSceneLoaded(int buildIndex) => 
            RegisterLevelDoor();

        private void RegisterLevelDoor()
        {
            if (_currentDoor != null)
                _currentDoor.Opened -= OnDoorOpened;

            _currentDoor = DoorOnLevel();
            _currentDoor.Opened += OnDoorOpened;
            
            //Debug.Log($"Register new door: {_currentDoor}");
        }
        
        private Door DoorOnLevel() =>
            Object.FindObjectOfType<Door>() ??
            throw new Exception("DOOR NOT FOUND!");

        private void OnDoorOpened(Door door)
        {
            door.GetComponent<VictoryEffect>().PlayVictory();
            DoorReached?.Invoke();
        }
    }
}