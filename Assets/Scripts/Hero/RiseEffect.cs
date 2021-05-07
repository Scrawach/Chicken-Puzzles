using System;
using UnityEngine;

namespace Hero
{
    public class RiseEffect : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioPrefab;
        [SerializeField] private ParticleSystem _effectPrefab;

        public void Play()
        {
            var position = transform.position;
            Instantiate(_audioPrefab, position, Quaternion.identity);
            Instantiate(_effectPrefab, position, Quaternion.identity);
        }
    }
}
