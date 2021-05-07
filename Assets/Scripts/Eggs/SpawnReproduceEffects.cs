using System;
using Hero;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Eggs
{
    [RequireComponent(typeof(ReproduceChicken))]
    public class SpawnReproduceEffects : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioPrefab;
        [SerializeField] private ParticleSystem _effectPrefab;

        private ReproduceChicken _reproduceChicken;

        private void Awake() =>
            _reproduceChicken = GetComponent<ReproduceChicken>();

        private void OnEnable() => 
            _reproduceChicken.Spawned += OnChickenSpawned;

        private void OnDisable() => 
            _reproduceChicken.Spawned -= OnChickenSpawned;

        private void OnChickenSpawned(Chicken chicken)
        {
            var position = chicken.transform.position;
            Instantiate(_audioPrefab, position, Quaternion.identity);
            Instantiate(_effectPrefab, position, Quaternion.identity);
        }
    }
}