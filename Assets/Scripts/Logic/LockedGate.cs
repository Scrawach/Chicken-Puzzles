using UnityEngine;

namespace Logic
{
    public class LockedGate : MonoBehaviour
    {
        [SerializeField] private Key[] _keys;
        [SerializeField] private Door _blockedDoor;
        [SerializeField] private GateAnimator _animator;

        private int _keyCount;
        private int _targetCount;
        
        private void Awake() => 
            _targetCount = _keys.Length;

        private void Start() => 
            _blockedDoor.Unlock();

        private void OnEnable()
        {
            foreach (var key in _keys)
                key.Picked += OnPicked;
        }

        private void OnDisable()
        {
            foreach (var key in _keys)
                key.Picked -= OnPicked;
        }

        private void OnPicked(Key key)
        {
            //Debug.Log("Picked Key");
            key.Picked -= OnPicked;
            _keyCount++;

            if (_keyCount >= _targetCount)
                Open();
        }

        private void Open()
        {
            _animator.PlayOpen();
            _blockedDoor.Lock();
        }
    }
}
