using System;
using UI;
using UnityEngine;

namespace CameraLogic
{
    [RequireComponent(typeof(Camera))]
    [RequireComponent(typeof(AspectChecker))]
    public class WindowsScaler : MonoBehaviour
    {
        private RectScaler[] _rects;
        private AspectChecker _aspectChecker;

        private void Awake()
        {
            _aspectChecker = GetComponent<AspectChecker>();
            _rects = FindObjectsOfType<RectScaler>();
        }

        private void OnEnable() => 
            _aspectChecker.Changed += OnAspectChanged;

        private void OnDisable() =>
            _aspectChecker.Changed -= OnAspectChanged;

        private void OnAspectChanged(float aspect)
        {
            if (_rects.Length == 0)
                return;

            var newScale = TargetScale(aspect);
            
            foreach (var rect in _rects)
                rect.Scale(newScale);
        }
        
        private static Vector3 TargetScale(float aspect)
        {
            return IsHorizontalScreen() ? ScaleForHorizontal() : Vector3.one;

            Vector3 ScaleForHorizontal() => 
                1 / aspect * Vector3.one;

            bool IsHorizontalScreen() => 
                aspect > 1;
        }
    }
}