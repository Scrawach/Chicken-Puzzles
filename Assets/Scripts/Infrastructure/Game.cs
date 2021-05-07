using System;
using Data;
using Infrastructure.Abstract;
using Infrastructure.Player;
using Scene;
using UnityEngine;

namespace Infrastructure
{
    public class Game : IDisposable
    {
        public SceneLoader SceneLoader { get; }
        public LevelProgress LevelProgress { get; }
        public PlayerAccount PlayerAccount { get; }

        public Game(ICoroutineRunner coroutineRunner, LevelStepRequirement requirement)
        {
            SceneLoader = new SceneLoader(coroutineRunner);
            LevelProgress = new LevelProgress(coroutineRunner, SceneLoader);
            PlayerAccount = new PlayerAccount(LevelProgress, requirement);
        }

        public void SaveProgress()
        {
            PlayerAccount.Save();
        }

        public void Dispose()
        {
            SceneLoader?.Dispose();
            LevelProgress?.Dispose();
            PlayerAccount?.Dispose();
        }
    }
}
