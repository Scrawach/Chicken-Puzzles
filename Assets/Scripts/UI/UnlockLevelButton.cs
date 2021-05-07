using System;
using Ad;
using UI.Effects;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Button))]
    public class UnlockLevelButton : MonoBehaviour
    {
        [SerializeField] private AdSettings _ad;
        [SerializeField] private FadeElement _warning;

        private Button _button;

        private void Awake() => 
            _button = GetComponent<Button>();

        private void OnEnable() => 
            _button.onClick.AddListener(OnButtonClick);

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnButtonClick);
            _warning.GetComponent<CanvasGroup>().alpha = 0f;
        }

        private void OnButtonClick()
        {
            if (_ad.CanShowReward())
                _ad.ShowRewardVideo();
            else
                _warning.HideWith(1f);
        }
    }
}
