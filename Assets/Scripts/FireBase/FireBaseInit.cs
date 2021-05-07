using System;
using Firebase;
using Firebase.Analytics;
using Firebase.Extensions;
using Infrastructure.Abstract;
using UnityEngine.SceneManagement;

namespace FireBase
{
    public class FireBaseInit : MonoSingleton<FireBaseInit>
    {
        private DependencyStatus _dependencyStatus = DependencyStatus.UnavailableOther;
        private bool _firebaseInitialized;
        
        private void Start()
        {
            FirebaseApp.CheckDependenciesAsync().ContinueWithOnMainThread(task =>
            {
                _dependencyStatus = task.Result;

                if (_dependencyStatus == DependencyStatus.Available)
                {
                    Init();
                }
            });
        }

        private void Init()
        {
            FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
            FirebaseAnalytics.SetUserProperty(FirebaseAnalytics.UserPropertySignUpMethod, "Google");
            FirebaseAnalytics.SetUserId(Guid.NewGuid().ToString());
            _firebaseInitialized = true;
        }
        
        public void LevelStart(int index)
        {
            if (_firebaseInitialized == false)
                return;

            var message = $"level_start_{index}";
            FirebaseAnalytics.LogEvent(message);
        }

        public void LevelDone(int index, int moves)
        {
            if (_firebaseInitialized == false)
                return;
            
            var message = $"level_done_{index}_{moves}";
            FirebaseAnalytics.LogEvent(message);
            
            message = $"level_end_{index}";
            FirebaseAnalytics.LogEvent(message);
        }
        
        public void RestartLevel(int current)
        {
            if (_firebaseInitialized == false)
                return;

            var message = $"level_restart_{current}";
            FirebaseAnalytics.LogEvent(message);
        }
        
        public void GameEnd()
        {
            if (_firebaseInitialized == false)
                return;
            
            FirebaseAnalytics.LogEvent("game_end");
        }

        public void Reward()
        {
            if (_firebaseInitialized == false)
                return;
            
            var message = $"reward_{SceneManager.GetActiveScene().buildIndex}";
            FirebaseAnalytics.LogEvent(message);
        }
    }
}
