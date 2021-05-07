using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Sound
{
    [RequireComponent(typeof(AudioSource))]
    public class RandomPitch : MonoBehaviour
    {
        [SerializeField] private Vector2 _pitchRange;
        private AudioSource _source;

        private void Awake()
        {
            _source = GetComponent<AudioSource>();
            _source.pitch = Random.Range(_pitchRange.x, _pitchRange.y);
        }
    }
}