using System;
using CodeBase.Data;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.Services.SaveLoad;
using CodeBase.Infrastructure.Services.StaticData;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
  public class LoadProgressState : IState
  {
    private const string InitialLevel = "Main";
    private readonly GameStateMachine _gameStateMachine;
    private readonly IPersistentProgressService _progressService;
    private readonly ISaveLoadService _saveLoadProgress;
    private readonly IStaticDataService _staticDataService;

    public LoadProgressState(GameStateMachine gameStateMachine, IPersistentProgressService progressService, ISaveLoadService saveLoadProgress, IStaticDataService staticDataService)
    {
      _gameStateMachine = gameStateMachine;
      _progressService = progressService;
      _saveLoadProgress = saveLoadProgress;
      _staticDataService = staticDataService;
    }

    public void Enter()
    {
      LoadProgressOrInitNew();

      _gameStateMachine.Enter<MainMenuState>();
    }

    public void Exit()
    {
    }

    public void Update()
    {
      
    }

    private void LoadProgressOrInitNew()
    {
      _progressService.Progress = 
        _saveLoadProgress.LoadProgress() 
        ?? NewProgress();
    }

    private PlayerProgress NewProgress()
    {
      PlayerProgress progress =  new PlayerProgress(InitialLevel);

      HeroStaticData heroData = _staticDataService.ForHero();

      progress.HeroState.MaxHP = heroData.Hp;
      progress.HeroState.Regeneration = 0;
      progress.HeroStats.SwordDamage = heroData.SwordDamage;
      progress.HeroStats.ArrowDamage = heroData.ArrowDamage;
      progress.HeroStats.ArrowSpeed = heroData.ArrowSpeed;
      progress.HeroStats.SwordRadius = heroData.Cleavage;
      progress.HeroStats.DamageMultiplier = 1;
      progress.HeroStats.CriticalChance = 0;
      progress.HeroStats.CriticalMultiplier = 2;
      progress.HeroState.ResetHp();
      progress.KingState.MaxHP = heroData.KingHp;
      progress.KingState.ResetHp();
      return progress;
    }
  }
}