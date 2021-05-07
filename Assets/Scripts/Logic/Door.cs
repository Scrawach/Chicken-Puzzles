using System;
using Hero;
using UnityEngine;

namespace Logic
{
    [RequireComponent(typeof(Collider))]
    public class Door : MonoBehaviour
    {
        private Collider _activeZone;
        public event Action<Door> Opened;

        private void Awake() => 
            _activeZone = GetComponent<Collider>();

        public void Lock() => 
            _activeZone.enabled = true;

        public void Unlock() =>
            _activeZone.enabled = false;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Chicken player) == false) 
                return;
            
            Opened?.Invoke(this);
        }
    }
}
