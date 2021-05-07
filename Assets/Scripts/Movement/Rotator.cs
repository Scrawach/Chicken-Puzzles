using System.Collections;
using UnityEngine;

namespace Movement
{
    public class Rotator : MonoBehaviour
    {
        [SerializeField] private float _speed;

        private Coroutine _rotating;
    
        public void StartRotate(Vector3 direction)
        {
            Stop();
            StartCoroutine(Rotating(direction, _speed));
        }
        
        public void Stop()
        {
            if (_rotating != null)
                StopCoroutine(_rotating);

            _rotating = null;
        }

        private IEnumerator Rotating(Vector3 direction, float speed)
        {
            var startRot = transform.rotation;
            var targetRot = Quaternion.LookRotation(direction);

            if (startRot == targetRot)
            {
                yield break;
            }

            _rotating = StartCoroutine(Smooth.Change(startRot, targetRot, speed, SmoothRotationChange));
            yield return _rotating;
        }
    
        private void SmoothRotationChange(Quaternion start, Quaternion end, float t) => 
            transform.rotation = Quaternion.Slerp(start, end, t);
    }
}