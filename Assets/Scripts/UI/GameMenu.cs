using System;
using Ad;
using Infrastructure;
using Infrastructure.Player;
using Sound;
using UnityEngine;
using UnityEngine.UI;

namespace UI.HUD
{
    public class GameMenu : MonoBehaviour
    {
        [Header("Buttons")]
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _selectLevelButton;
        [SerializeField] private ToggleButton _soundMuteToggle;
        
        [Header("Data")]
        [SerializeField] private MixerLevels _audioMixer;

        [Header("UI Windows")]
        [SerializeField] private SelectLevelMenu _selectLevelMenuMenu;
        [SerializeField] private LoadingCurtain _loadingCurtain;
        [SerializeField] private LastWord _lastWordWindow;
        [SerializeField] private LevelCounter _levelCounter;

        private SceneLoader _sceneLoader;
        private LevelProgress _levelProgress;
      
        public event Action Paused;
        public event Action Unpaused;

        public void Construct(SceneLoader sceneLoader, LevelProgress progress, PlayerAccount account)
        {
            _sceneLoader = sceneLoader;
            _levelProgress = progress;
            
            _loadingCurtain.Construct(sceneLoader, progress, account);
            _levelCounter.Construct(sceneLoader);
            _selectLevelMenuMenu.Construct(account, sceneLoader);
            _lastWordWindow.Construct(sceneLoader, progress);

            _selectLevelMenuMenu.Closed += OnSelectLevelClosed;
        }
        
        private void Awake() => 
            DontDestroyOnLoad(this);

        private void OnEnable()
        {
            _restartButton.onClick.AddListener(OnRestartClicked);
            _selectLevelButton.onClick.AddListener(OnSelectLevelClicked);
            _soundMuteToggle.Toggled += OnSoundMuteToggled;
        }
        
        private void OnDisable()
        {
            _restartButton.onClick.RemoveListener(OnRestartClicked);
            _selectLevelButton.onClick.RemoveListener(OnSelectLevelClicked);
            _soundMuteToggle.Toggled -= OnSoundMuteToggled;
        }
        
        private void OnDestroy()
        {
            _sceneLoader.Dispose();
            _levelProgress.Dispose();
            _selectLevelMenuMenu.Closed -= OnSelectLevelClosed;
        }

        private void OnSelectLevelClosed() => 
            Unpaused?.Invoke();
        
        private void OnRestartClicked()
        {
            _levelProgress.Break();
            _sceneLoader.RestartScene();
        }

        private void OnSelectLevelClicked()
        {
            _selectLevelMenuMenu.Open();
            Paused?.Invoke();
        }

        private void OnSoundMuteToggled(bool unmute)
        {
            if (unmute)
                _audioMixer.Unmute();
            else
                _audioMixer.Mute();
        }
    }
}