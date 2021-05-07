using Hero;
using UnityEngine;

namespace Eggs
{
    public class Ammo : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Weapon playerWeapon) == false) 
                return;
            
            playerWeapon.AddAmmo();
        }
    }
}
