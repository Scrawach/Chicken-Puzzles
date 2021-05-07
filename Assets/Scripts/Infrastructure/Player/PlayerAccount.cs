using System;
using System.Collections.Generic;
using Data;
using FireBase;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure.Player
{
    public class PlayerAccount : IDisposable
    {
        private readonly LevelProgress _levelProgress;

        public ProgressData Data { get; private set; }

        public LevelStepRequirement Requirement { get; }

        public event Action<LevelData> DataChanged;

        public PlayerAccount(LevelProgress levelProgress, LevelStepRequirement requirement)
        {
            _levelProgress = levelProgress;
            Requirement = requirement;
            
            _levelProgress.Passed += OnLevelProgressPassed;
            _levelProgress.GameEnded += OnGameEnded;
            
            DataInit();
        }
        
        public void UnlockNextLevel()
        {
            if (Data.LastLevel + 1 >= Data.Levels.Count)
                return;
            
            Data.LastLevel++;
            DataChanged?.Invoke(Data.Levels[Data.LastLevel]);
        }

        public int RequirementStepsForLevel(int index)
        {
            var levels = Requirement.Requirements;
            return levels.Count > index ? Requirement.Requirements[index].StepCount : 0;
        }

        public void Save()
        {
            SaveLoad.Save(Data);
        }

        private void OnGameEnded()
        {
            Debug.Log("DONE");
            OnLevelProgressPassed(Data.Levels.Count - 1, 0);
        }

        private void OnLevelProgressPassed(int index, int stepCount)
        {
            if (index >= Data.LastLevel)
                Data.LastLevel = index + 1;
            
            Data.Levels[index].PassedAchievementUnlock = true;
            Data.Levels[index].StepAchievementUnlock |= stepCount <= RequirementStepsForLevel(index);
                        
            DataChanged?.Invoke(Data.Levels[index]);
            
            if (Data.Levels.Count > Data.LastLevel)
                DataChanged?.Invoke(Data.Levels[Data.LastLevel]);
            
            if (FireBaseInit.Instance != null)
                FireBaseInit.Instance.LevelDone(index, stepCount);
        }

        private void DataInit() => 
            Data = SaveLoad.Load() ?? InitialNewProgress();

        private static ProgressData InitialNewProgress()
        {
            var data = ProgressData.Empty;
            data.Levels.Clear();

            for (var i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
            {
                data.Levels.Add(new LevelData(i, false, false));
            }
            
            return data;
        }

        public void Dispose()
        {
            _levelProgress.Passed -= OnLevelProgressPassed;
        }
    }
}