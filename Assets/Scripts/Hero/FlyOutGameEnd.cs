using System;
using FireBase;
using UnityEngine;

namespace Hero
{
    [RequireComponent(typeof(ChickenAnimator))]
    public class FlyOutGameEnd : MonoBehaviour
    {
        [SerializeField] private GameObject _jetpackPrefab;
        [SerializeField] private Transform _point;
        
        private ChickenAnimator _animator;

        private void Awake() => 
            _animator = GetComponent<ChickenAnimator>();
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Jetpack jetpack) == false)
                return;

            Instantiate(_jetpackPrefab, _point.position, Quaternion.identity, _point.parent);
            FireBaseInit.Instance.GameEnd();
            _animator.PlayFlyOut();
        }
    }
}