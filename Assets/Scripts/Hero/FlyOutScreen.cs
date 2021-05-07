using System;
using JetBrains.Annotations;
using UnityEngine;

namespace Hero
{
    [RequireComponent(typeof(ChickenAnimator))]
    public class FlyOutScreen : MonoBehaviour
    {
        public event Action Happened;
        
        [UsedImplicitly]
        public void OnFlyOutAnimationEnded() => 
            Happened?.Invoke();
    }
}