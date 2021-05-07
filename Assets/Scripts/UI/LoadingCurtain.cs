using System;
using Ad;
using Infrastructure;
using Infrastructure.Player;
using Scene;
using UI.Effects;
using UI.HUD;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI
{
    public class LoadingCurtain : FadeElement
    {
        [SerializeField] private AchievementWindow _achievement;
        [SerializeField] private NextScene _nextSceneButton;
        [SerializeField] private AdSettings _adSettings;

        private PlayerAccount _playerAccount;
        private SceneLoader _sceneLoader;
        private LevelProgress _levelProgress;

        public void Construct(SceneLoader sceneLoader, LevelProgress levelProgress, PlayerAccount account)
        {
            Unsubscribe();

            _playerAccount = account;
            _levelProgress = levelProgress;
            _sceneLoader = sceneLoader;
            
            _levelProgress.Passed += OnLevelPassed;
            _sceneLoader.Loaded += OnLevelLoaded;
            
            _nextSceneButton.Construct(_sceneLoader);
        }

        private void OnDestroy() => 
            Unsubscribe();

        private void Unsubscribe()
        {
            if (_levelProgress != null)
                _levelProgress.Passed -= OnLevelPassed;

            if (_sceneLoader != null)
                _sceneLoader.Loaded -= OnLevelLoaded;
        }

        private void OnEnable()
        {
            FadeInEnded += OnFadeIntEnded;
            _achievement.FadeInEnded += OnAchievementShowed;
        }
        
        private void OnDisable()
        {
            FadeInEnded -= OnFadeIntEnded;
            _achievement.FadeInEnded -= OnAchievementShowed;
        }
        
        private void OnAchievementShowed()
        {
            _nextSceneButton.Show();
            _nextSceneButton.UnlockButton();
        }

        private void OnLevelPassed(int buildIndex, int score)
        {
            Canvas.blocksRaycasts = true;
            
            Show();
            _adSettings.ShowBanner();
            _achievement.SetRequiredSteps(score, _playerAccount.RequirementStepsForLevel(buildIndex));
        }

        private void OnLevelLoaded(int buildIndex)
        {
            Canvas.blocksRaycasts = false;
            
            Hide();
            _adSettings.HideBanner();
            _achievement.Hide();
            _nextSceneButton.Hide();
        }

        private void OnFadeIntEnded()
        {
            _achievement.Show();
        }
    }
}