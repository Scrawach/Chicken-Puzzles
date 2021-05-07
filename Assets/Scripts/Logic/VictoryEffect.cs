using UnityEngine;

namespace Logic
{
    public class VictoryEffect : MonoBehaviour
    {
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private AudioSource _audioPrefab;
        [SerializeField] private ParticleSystem _particlePrefab;

        public void PlayVictory()
        {
            Instantiate(_audioPrefab, _spawnPoint.position, Quaternion.identity);
            Instantiate(_particlePrefab, _spawnPoint.position, _particlePrefab.transform.rotation);
        }
    }
}