using System.Collections;
using UnityEngine;

namespace Sound
{
    [RequireComponent(typeof(AudioSource))]
    public class IncreasePitch : MonoBehaviour
    {
        [SerializeField] private Vector2 _pitchRange;
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
                _source.pitch = Mathf.Lerp(_pitchRange.x, _pitchRange.y, t);
                yield return new WaitForFixedUpdate();
            }
        }
    }
}