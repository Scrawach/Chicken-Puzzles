using System;
using UnityEngine;

namespace Eggs
{
    public class DestroyWhenTriggered : MonoBehaviour
    {
        [SerializeField] private LayerMask _triggeredLayer;

        private void OnTriggerEnter(Collider other)
        {
            if (IsLayersNotEquals(other.gameObject.layer))
                return;
            
            Destroy(gameObject);
        }

        private bool IsLayersNotEquals(int target) => 
            (LayerValue(target) & _triggeredLayer.value) == 0;

        private static int LayerValue(int layerNumber) => 
            1 << layerNumber;
    }
}