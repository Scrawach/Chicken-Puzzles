using System;
using Hero;
using UnityEngine;

namespace Logic.Platforms
{
    [RequireComponent(typeof(Collider))]
    public class FloorButton : MonoBehaviour
    {
        public event Action Pressed;
        public event Action Realized;
        
        private void OnTriggerEnter(Collider other)
        {
            if (IsNotChicken(other))
                return;
            
            Pressed?.Invoke();
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (IsNotChicken(other))
                return;
            
            Realized?.Invoke();
        }
        
        private static bool IsNotChicken(Component other) => 
            other.TryGetComponent(out Chicken chicken) == false;
    }
}