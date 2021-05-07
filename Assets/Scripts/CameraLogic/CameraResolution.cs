using System;
using UnityEngine;

namespace CameraLogic
{
    [RequireComponent(typeof(AspectChecker))]
    public class CameraResolution : MonoBehaviour
    {
        [SerializeField] private Vector2 _sizeRange;
        
        private Camera _camera;
        private AspectChecker _aspectChecker;

        private void Awake()
        {
            _camera = Camera.main;
            _aspectChecker = GetComponent<AspectChecker>();
        }

        private void OnEnable() => 
            _aspectChecker.Changed += OnAspectChanged;

        private void OnDisable() => 
            _aspectChecker.Changed -= OnAspectChanged;

        private void OnAspectChanged(float aspect)
        {
            var targetSize = aspect > 1 ? _sizeRange.x : _sizeRange.y;
            _camera.orthographicSize = targetSize;
        }
    }
}
