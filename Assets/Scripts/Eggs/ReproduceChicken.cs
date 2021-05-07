using System;
using Hero;
using UnityEngine;

namespace Eggs
{
    public class ReproduceChicken : MonoBehaviour
    {
        private const float BackFloorValidDistance = .1f;

        [SerializeField] private Chicken _chickenTemplate;
        [SerializeField] private LayerMask _triggeredLayer;

        public event Action<Chicken> Spawned;

        private void OnTriggerEnter(Collider other)
        {
            if (IsLayersNotEquals(other.gameObject.layer))
                return;
            
            if (other.TryGetComponent(out Chicken player))
                return;
            
            TrySpawnChicken();
        }
        
        private bool IsLayersNotEquals(int target) => 
            (LayerValue(target) & _triggeredLayer.value) == 0;

        private static int LayerValue(int layerNumber) => 
            1 << layerNumber;
        
        private void TrySpawnChicken()
        {
            var spawnPositionFloat = transform.position - BackFloorValidDistance * transform.forward;

            var spawnPositionInt = new Vector3(
                Mathf.Round(spawnPositionFloat.x), 
                Mathf.Floor(spawnPositionFloat.y), 
                Mathf.Round(spawnPositionFloat.z));

            SpawnChicken(spawnPositionInt);
        }
        
        private void SpawnChicken(Vector3 position)
        {
            var chicken = Instantiate(_chickenTemplate, position, transform.rotation);

            if (chicken.TryGetComponent(out RandomCrestColor crest))
                crest.Generate();

            Spawned?.Invoke(chicken);
        }
    }
}