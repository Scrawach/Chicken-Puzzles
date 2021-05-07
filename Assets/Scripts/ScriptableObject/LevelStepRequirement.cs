using System.Collections.Generic;
using Infrastructure.Player;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "Level steps requirement", menuName = "Chicken/Level Steps", order = 0)]
    public class LevelStepRequirement : ScriptableObject
    {
        public List<Requirement> Requirements;
    }
}