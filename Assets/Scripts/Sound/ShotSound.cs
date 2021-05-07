using System;
using Hero;
using UnityEngine;

namespace Sound
{
    [RequireComponent(typeof(Weapon))]
    public class ShotSound : MonoBehaviour
    {
        [SerializeField] private AudioSource _shotSound;
        private Weapon _weapon;

        private void Awake() => 
            _weapon = GetComponent<Weapon>();

        private void OnEnable() => 
            _weapon.Fired += OnWeaponFired;

        private void OnDisable() => 
            _weapon.Fired -= OnWeaponFired;

        private void OnWeaponFired() => 
            Instantiate(_shotSound, transform.position, Quaternion.identity);
    }
}