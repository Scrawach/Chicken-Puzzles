using System;
using UnityEngine;

namespace Eggs
{
    [RequireComponent(typeof(Collider))]
    public class SpawnTriggerSound : MonoBehaviour
    {
        [SerializeField] private AudioSource _audio;

        private void OnTriggerEnter(Collider other) => 
            Instantiate(_audio, transform.position, Quaternion.identity);
    }
}