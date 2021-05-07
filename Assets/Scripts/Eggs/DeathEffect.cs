using System;
using UnityEngine;

namespace Eggs
{
    public class DeathEffect : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particlePrefab;

        private void OnTriggerEnter(Collider other) => 
            Instantiate(_particlePrefab, transform.position, Quaternion.identity);
    }
}