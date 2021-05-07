using UnityEngine;

namespace Eggs
{
    [RequireComponent(typeof(Rigidbody))]
    public class MoveForward : MonoBehaviour
    {
        [SerializeField] private float _speed;

        private Rigidbody _body;
        
        private void Awake()
        {
            _body = GetComponent<Rigidbody>();
            SetMovement(transform.forward);
        }

        private void SetMovement(Vector3 direction) =>
            _body.velocity = _speed * Time.fixedDeltaTime * direction;
    }
}