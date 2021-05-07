using System;
using System.Collections;
using FireBase;
using Infrastructure.Abstract;
using UI;
using UnityEngine.SceneManagement;

namespace Infrastructure
{
    public class SceneLoader : IDisposable
    {
        private readonly ICoroutineRunner _coroutineRunner;
        private int _loadingBuildIndex;
        
        public event Action<int> Loaded;
        
        public SceneLoader(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        
        private void OnSceneLoaded(UnityEngine.SceneManagement.Scene arg0, LoadSceneMode arg1) => 
            Loaded?.Invoke(_loadingBuildIndex);

        public void RestartScene()
        {
            var current = SceneManager.GetActiveScene().buildIndex;
            Load(current);
            FireBaseInit.Instance.RestartLevel(current);
        }

        public void LoadNextScene()
        {
            Load(IsValidScene(CurrentSceneBuild() + 1) ? CurrentSceneBuild() + 1 : 0);
            FireBaseInit.Instance.LevelStart(CurrentSceneBuild() + 1);
        }

        public void Load(int sceneIndex) => 
            _coroutineRunner.StartCoroutine(AsyncLoading(sceneIndex));

        public bool IsLastScene(int index) => 
            index == SceneManager.sceneCountInBuildSettings - 1;

        public int CurrentSceneBuild() => 
            SceneManager.GetActiveScene().buildIndex;

        private IEnumerator AsyncLoading(int sceneIndex)
        {
            _loadingBuildIndex = IsValidScene(sceneIndex) ? sceneIndex : 0;
            var waitNextScene = SceneManager.LoadSceneAsync(_loadingBuildIndex);
            
            if (waitNextScene.isDone == false)
                yield return null;
        }

        private static bool IsValidScene(int index)
        {
            try
            {
                SceneManager.GetSceneByBuildIndex(index);
            }
            catch (Exception error)
            {
                return false;
            }
            
            return true;
        }

        public void Dispose()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }
}