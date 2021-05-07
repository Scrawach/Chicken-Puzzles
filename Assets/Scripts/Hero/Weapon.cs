using System;
using Eggs;
using UnityEngine;

namespace Hero
{
    public class Weapon : MonoBehaviour
    {
        private const float CheckDistance = 1f;
        [SerializeField] private Transform _firePoint;
        [SerializeField] private Egg _eggTemplate;
        [SerializeField] private float _cooldown;
        
        [SerializeField] private LayerMask _wallLayerMask;
        [SerializeField] private AudioSource _shotAudio;
        [SerializeField] private AudioSource _ammoAudio;

        private float _currentTime;
        private int _ammoCount;

        public event Action AmmoAdded;
        public event Action Fired;

        private void Update() => 
            UpdateCooldown();

        private void UpdateCooldown()
        {
            if (CooldownUp() == false)
                _currentTime += Time.deltaTime;
        }

        public void Shot()
        {
            _currentTime = 0f;
            Instantiate(_shotAudio, _firePoint.position, Quaternion.identity);
            Instantiate(_eggTemplate, _firePoint.position, transform.rotation);
            
            _ammoCount--;
            Fired?.Invoke();
        }

        public bool CanShot() =>
            HasAmmo() && CooldownUp() && !IsAheadWall();

        private bool IsAheadWall()
        {
            var weaponPoint = _firePoint;
            var forward = weaponPoint.forward;
            
            var origin = weaponPoint.position - forward / 2;
            var checkWall = new Ray(origin, forward);
            
            return Physics.Raycast(checkWall, CheckDistance, _wallLayerMask);
        }

        private bool CooldownUp() => 
            _currentTime >= _cooldown;

        private bool HasAmmo() =>
            _ammoCount > 0;

        public void AddAmmo()
        {
            _ammoCount++;
            Instantiate(_ammoAudio, transform.position, Quaternion.identity);
            AmmoAdded?.Invoke();
        }
    }
}
