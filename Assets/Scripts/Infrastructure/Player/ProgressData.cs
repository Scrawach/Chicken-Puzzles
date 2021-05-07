using System;
using System.Collections.Generic;

namespace Infrastructure.Player
{
    [Serializable]
    public class ProgressData
    {
        public int LastLevel;
        public List<LevelData> Levels;

        public static ProgressData Empty =>
            new ProgressData(0, 0);
        
        public ProgressData(int index, int levelsCount)
        {
            LastLevel = index;
            Levels = new List<LevelData>(levelsCount);
        }
    }
}