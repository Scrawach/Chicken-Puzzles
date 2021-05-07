using System;
using UnityEngine;

namespace Logic
{
    public class Flapper : MonoBehaviour
    {
        [SerializeField] private Door _door;
        [SerializeField] private ParticleSystem _particle;

        private void OnEnable()
        {
            _door.Opened += OnDoorOpened;
        }

        private void OnDisable()
        {
            _door.Opened -= OnDoorOpened;
        }

        private void OnDoorOpened(Door door)
        {
            _particle.Play();
        }
    }
}
