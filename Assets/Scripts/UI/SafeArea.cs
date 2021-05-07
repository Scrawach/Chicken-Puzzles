using System;
using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(Canvas))]
    [RequireComponent(typeof(RectTransform))]
    public class SafeArea : MonoBehaviour
    {
        private Canvas _canvas;
        private RectTransform _area;
        private Rect _lastSafeArea;
        
        private void Awake()
        {
            _canvas = GetComponent<Canvas>();
            _area = GetComponent<RectTransform>();
            
            ApplySafeArea();
        }

        private void Update()
        {
            if (_lastSafeArea == Screen.safeArea)
                return;

            _lastSafeArea = Screen.safeArea;
            ApplySafeArea();
        }

        private void ApplySafeArea()
        {
            var safeArea = Screen.safeArea;

            var anchorMin = safeArea.position;
            var anchorMax = safeArea.position + safeArea.size;

            var pixelRect = _canvas.pixelRect;
            anchorMin = AlignCenter(anchorMin, pixelRect);
            anchorMax = AlignCenter(anchorMax, pixelRect);

            UpdateSafeArea(anchorMin, anchorMax);
        }

        private static Vector2 AlignCenter(Vector2 current, Rect pixel) => 
            new Vector2(current.x / pixel.width, current.y / pixel.height);

        private void UpdateSafeArea(Vector2 min, Vector2 max)
        {
            _area.anchorMin = min;
            _area.anchorMax = max;
        }
    }
}