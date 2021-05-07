using System;
using UI;
using UnityEngine;

namespace CameraLogic
{
    [RequireComponent(typeof(Camera))]
    public class AspectChecker : MonoBehaviour
    {
        private Camera _camera;
        private float _lastAspect;

        public event Action<float> Changed;

        private void Awake()
        {
            _camera = GetComponent<Camera>();
            _lastAspect = _camera.aspect;
            Changed?.Invoke(_lastAspect);
        }
        
        private void Update()
        {
            if (IsAspectChanged() == false) 
                return;

            _lastAspect = _camera.aspect;
            Changed?.Invoke(_lastAspect);
        }
        
        private bool IsAspectChanged() => 
            Mathf.Abs(_camera.aspect - _lastAspect) > Mathf.Epsilon;
    }
}
