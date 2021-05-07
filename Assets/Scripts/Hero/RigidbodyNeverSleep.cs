using UnityEngine;

namespace Hero
{
    [RequireComponent(typeof(Rigidbody))]
    public class RigidbodyNeverSleep : MonoBehaviour
    {
        private void Awake() => 
            GetComponent<Rigidbody>().sleepThreshold = 0f;
    }
}