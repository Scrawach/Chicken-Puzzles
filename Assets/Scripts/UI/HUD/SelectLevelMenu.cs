using System;
using System.Collections.Generic;
using Infrastructure;
using Infrastructure.Player;
using JetBrains.Annotations;
using Scene;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.HUD
{
    public class SelectLevelMenu : Window
    {
        [SerializeField] private RewardWindow _rewardWindow;
        [SerializeField] private LevelButton _buttonTemplate;
        [SerializeField] private Transform _content;
        [SerializeField] private Button _closeButton;

        private PlayerAccount _player;
        private SceneLoader _sceneLoader;
        private List<LevelButton> _buttons;

        public event Action Closed;

        public void Construct(PlayerAccount player, SceneLoader sceneLoader)
        {
            if (_player != null)
                _player.DataChanged -= OnDataChanged;
            
            _player = player;
            _player.DataChanged += OnDataChanged;
            
            _sceneLoader = sceneLoader;

            CreateButtons(_player.Data);
        }

        private void OnEnable()
        {
            _closeButton.onClick.AddListener(OnClosedClicked);

            foreach (var button in _buttons)
                button.Clicked += OnLevelButtonClicked;
        }
        
        private void OnDisable()
        {
            _closeButton.onClick.RemoveListener(OnClosedClicked);
            
            foreach (var button in _buttons)
                button.Clicked -= OnLevelButtonClicked;
        }

        private void OnDestroy()
        {
            if (_player != null)
                _player.DataChanged -= OnDataChanged;
        }
        
        public void UnlockNextLevel() => 
            _player.UnlockNextLevel();
        
        private void OnClosedClicked()
        {
            Closed?.Invoke();
            gameObject.SetActive(false);
        }
        
        private void OnLevelButtonClicked(int buildIndex)
        {
            if (_buttons[buildIndex].IsLocked)
                return;
                
            LoadScene(with: buildIndex);
        }
        
        private void LoadScene(int with)
        {
            Close();
            _sceneLoader.Load(with);
        }

        private void OnDataChanged(LevelData data)
        {
            if (data.PassedAchievementUnlock)
                _buttons[data.BuildIndex].UnlockPassedStar();
            
            if (data.StepAchievementUnlock)
                _buttons[data.BuildIndex].UnlockStepStar();
            
            _buttons[data.BuildIndex].Unlock();
        }

        private void CreateButtons(ProgressData data)
        {
            var lastOpenedLevel = _player.Data.LastLevel;
            _buttons = new List<LevelButton>(data.Levels.Count);

            for (var i = 0; i < data.Levels.Count; i++)
            {
                var button = CreateEmptyButton();
                button.Construct(i, i > lastOpenedLevel);
                
                if (data.Levels[i].StepAchievementUnlock)
                    button.UnlockStepStar();
                
                _buttons.Add(button);
            }
        }
        
        private LevelButton CreateEmptyButton() =>
            Instantiate(_buttonTemplate, Vector3.zero, Quaternion.identity, _content);
    }
}