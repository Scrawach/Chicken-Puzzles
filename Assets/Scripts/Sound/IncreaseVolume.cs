using System.Collections;
using UnityEngine;

namespace Sound
{
    [RequireComponent(typeof(AudioSource))]
    public class IncreaseVolume : MonoBehaviour
    {
        [SerializeField] private Vector2 _volumeRange;
        [SerializeField] private float _time;
        private AudioSource _source;

        private void Awake()
        {
            _source = GetComponent<AudioSource>();
            StartCoroutine(Increase());
        }

        private IEnumerator Increase()
        {
            var step = Time.fixedDeltaTime / _time;
            var t = 0f;

            while (t <= 1f)
            {
                t += step;
                _source.volume = Mathf.Lerp(_volumeRange.x, _volumeRange.y, t);
                yield return new WaitForFixedUpdate();
            }
        }
    }
}