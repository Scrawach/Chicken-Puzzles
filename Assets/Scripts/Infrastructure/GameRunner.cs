using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Infrastructure
{
    public class GameRunner : MonoBehaviour
    {
        [SerializeField] private GameBootstrapper _gameBootstrapperPrefab;

        private void Awake()
        {
            if (BootstrapperNotExist())
                CreateBootstrapper();
        }

        private void CreateBootstrapper() => 
            Instantiate(_gameBootstrapperPrefab);

        private bool BootstrapperNotExist() => 
            FindObjectOfType<GameBootstrapper>() == null;
    }
}