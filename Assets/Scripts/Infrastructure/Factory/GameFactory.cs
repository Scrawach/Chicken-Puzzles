using Data;
using Hero;
using UnityEngine;

namespace Infrastructure.Factory
{
    public class GameFactory
    {
        public Chicken CreateChicken(Vector3 position)
        {
            var chicken = Resources.Load<Chicken>(AssetPath.Chicken);
            return Object.Instantiate(chicken, position, Quaternion.identity);
        }
    }
}