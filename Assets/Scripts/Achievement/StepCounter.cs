using System;
using Hero;
using Movement;
using UnityEngine;

namespace Achievement
{
    [RequireComponent(typeof(Chicken))]
    public class StepCounter : MonoBehaviour
    {
        private Chicken _mover;
        
        public int Score { get; private set; }

        private void Awake() => 
            _mover = GetComponent<Chicken>();

        private void OnEnable() => 
            _mover.Moved += OnMoveEnded;

        private void OnDisable() => 
            _mover.Moved -= OnMoveEnded;

        private void OnMoveEnded()
        {
            Score++;
        }
    }
}