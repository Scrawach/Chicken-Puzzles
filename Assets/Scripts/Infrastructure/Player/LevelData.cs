using System;

namespace Infrastructure.Player
{
    [Serializable]
    public class LevelData
    {
        public int BuildIndex;
        public bool StepAchievementUnlock;
        public bool PassedAchievementUnlock;

        public LevelData(int index, bool passed, bool stepDone)
        {
            BuildIndex = index;
            StepAchievementUnlock = stepDone;
            PassedAchievementUnlock = passed;
        }
    }
}