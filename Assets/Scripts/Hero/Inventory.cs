using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Hero
{
    [RequireComponent(typeof(Weapon))]
    public class Inventory : MonoBehaviour
    {
        private const float SlotHeight = .25f;
        
        [SerializeField] private Transform _container;
        [SerializeField] private Transform _inventoryPoint;
        [SerializeField] private GameObject _ammoTemplate;

        private readonly List<GameObject> _slots = new List<GameObject>();
        private Weapon _weapon;

        private void Awake()
        {
            _weapon = GetComponent<Weapon>();
        }

        private void OnEnable()
        {
            _weapon.AmmoAdded += OnAmmoAdded;
            _weapon.Fired += OnAmmoRemoved;
        }

        private void OnDisable()
        {
            _weapon.AmmoAdded -= OnAmmoAdded;
            _weapon.Fired -= OnAmmoRemoved;
        }

        private void OnAmmoAdded()
        {
            AddSlot();
            MoveSlotUp();
        }
        
        private void OnAmmoRemoved()
        {
            RemoveSlot();
            MoveSlotDown();
        }
        
        private void AddSlot() => 
            _slots.Add(Instantiate(_ammoTemplate, _inventoryPoint.position, transform.rotation, _container));

        private void RemoveSlot()
        {
            var item = _slots.LastOrDefault();
            _slots.Remove(item);
            
            if (item != null)
                Destroy(item.gameObject);
        }

        private void MoveSlotUp() => 
            _inventoryPoint.position += SlotHeight * Vector3.up;

        private void MoveSlotDown() => 
            _inventoryPoint.position += SlotHeight * Vector3.down;
    }
}
