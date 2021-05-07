using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.HUD
{
    public class RewardWindow : Window
    {
        [SerializeField] private Button _agree;
        [SerializeField] private Window _ad;

        private void OnEnable() => 
            _agree.onClick.AddListener(OnAgreeClicked);

        private void OnDisable() => 
            _agree.onClick.RemoveListener(OnAgreeClicked);

        private void OnAgreeClicked()
        {
            Debug.Log("AD!"); 
            _ad.Open();
        }
    }
}