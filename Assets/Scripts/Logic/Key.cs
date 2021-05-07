using System;
using Hero;
using UnityEngine;

namespace Logic
{
    public class Key : MonoBehaviour
    {
        public event Action<Key> Picked;
        
        private void OnTriggerEnter(Collider other)
        {
            Picked?.Invoke(this);
        }
    }
}
