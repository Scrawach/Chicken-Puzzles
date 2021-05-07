using UnityEngine;

namespace Movement
{
    public class BezierMover : Mover
    {
        [SerializeField] private float _height = 1f;
    
        protected override void SmoothPositionChange(Vector3 start, Vector3 end, float t)
        {
            var startPosition = start;
            var endPosition = end;
            
            var x = Mathf.Lerp(startPosition.x, endPosition.x, t);
            var y = Functions.Bezier(startPosition.y, start.y + _height, endPosition.y, t);
            var z = Mathf.Lerp(startPosition.z, endPosition.z, t);
            transform.position = new Vector3(x, y, z);
        }
    }
}