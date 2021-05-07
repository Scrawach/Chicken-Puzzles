using System;
using System.Collections;
using FireBase;
using Hero;
using Infrastructure;
using UnityEngine;

namespace Scene
{
    [RequireComponent(typeof(Collider))]
    public class GameOverZone : MonoBehaviour
    {
        [SerializeField] private float _pauseBeforeLose = 0f;
        
        private SceneLoader _sceneLoader;
        private LevelProgress _levelProgress;

        public void Construct(SceneLoader sceneLoader, LevelProgress levelProgress)
        {
            _sceneLoader = sceneLoader;
            _levelProgress = levelProgress;
        }
    
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Chicken player) == false)
                return;
                
            StartCoroutine(Lose(with: _pauseBeforeLose));
            player.Kill();
        }

        private void RestartLevel()
        {
            _levelProgress.Break();
            _sceneLoader.RestartScene();
        }

        private IEnumerator Lose(float with = 0f)
        {
            yield return new WaitForSeconds(with);
            RestartLevel();
        }
    }
}
