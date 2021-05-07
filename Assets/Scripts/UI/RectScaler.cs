using System;
using CameraLogic;
using UnityEngine;

namespace UI
{
    public class RectScaler : MonoBehaviour
    {
        [SerializeField] private RectTransform _rect;
        private AspectChecker _camera;

        public void Scale(Vector3 target) => 
            _rect.localScale = target;
    }
}
