using Logic.Platforms;
using UnityEngine;

namespace Hero
{
    public class StuckToMovingPlatform : MonoBehaviour
    {
        private void OnCollisionEnter(Collision other)
        {
            if (other.collider.TryGetComponent(out MovingPlatform moving))
                transform.parent = other.transform;
        }

        private void OnCollisionExit(Collision other)
        {
            if (other.collider.TryGetComponent(out MovingPlatform moving))
                transform.parent = null;
        }
    }
}