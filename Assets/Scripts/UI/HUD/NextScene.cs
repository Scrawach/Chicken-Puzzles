using System;
using Infrastructure;
using UI.Effects;
using UnityEngine;
using UnityEngine.UI;

namespace UI.HUD
{
    [RequireComponent(typeof(Button))]
    public class NextScene : FadeElement
    {
        private SceneLoader _sceneLoader;
        private Button _nextButton;
        
        public void Construct(SceneLoader sceneLoader) => 
            _sceneLoader = sceneLoader;

        protected override void Awake()
        {
            base.Awake();
            _nextButton = GetComponent<Button>();
        }

        private void OnEnable() => 
            _nextButton.onClick.AddListener(OnClicked);

        private void OnDisable() => 
            _nextButton.onClick.RemoveListener(OnClicked);

        private void OnClicked()
        {
            _sceneLoader.LoadNextScene();
            LockButton();
        }

        public void UnlockButton() =>
            _nextButton.interactable = true;

        public void LockButton() => 
            _nextButton.interactable = false;
    }
}