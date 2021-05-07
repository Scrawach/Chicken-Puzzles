using System;
using Infrastructure;
using Infrastructure.Player;
using TMPro;
using UI.Effects;
using UI.HUD;
using UnityEngine;

namespace UI
{
    public class AchievementWindow : FadeElement
    {
        [SerializeField] private TextMeshProUGUI _score;
        [SerializeField] private Color _successColor;
        [SerializeField] private Color _loseColor;
        
        public void SetRequiredSteps(int steps, int requirementSteps)
        {
            _score.text = $"{steps} / {requirementSteps}";
            _score.color = steps <= requirementSteps ? _successColor : _loseColor;
        }
    }
}