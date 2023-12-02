using System;
using CodeBase.Data;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.Services.Randomizer;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Hero
{

	public class HeroHealth : MonoBehaviour, IHealth, ISavedProgress
	{
		[SerializeField] private MagicShield _magicShield;
		
		public HeroAnimator Animator;
		public AudioSource LevelUpSound;

		private IRandomService _randomService;
		private IPersistentProgressService _progressService;
		private State _state;
		private float _regenerationTimer = 7;
		private float _elapsedTime;

		public event Action HealthChanged;

		public float Current
		{
			get => _state.CurrentHP;
			set
			{
				if (value != _state.CurrentHP)
				{
					_state.CurrentHP = value;

					HealthChanged?.Invoke();
				}
			}
		}

		public float Max
		{
			get => _state.MaxHP;
			set => _state.MaxHP = value;
		}

		public void Construct(IRandomService randomService, IPersistentProgressService progressService)
		{
			_randomService = randomService;
			_progressService = progressService;
		}

		public void LoadProgress(PlayerProgress progress)
		{
			_state = progress.HeroState;
			HealthChanged?.Invoke();
		}

		public void UpdateProgress(PlayerProgress progress)
		{
			progress.HeroState.CurrentHP = Current;
			progress.HeroState.MaxHP = Max;
		}

		private void Update()
		{
			_elapsedTime += Time.deltaTime;
			if (_progressService != null && _regenerationTimer <= _elapsedTime)
			{
				_elapsedTime = 0;
				Current += _progressService.Progress.KingState.Regeneration;
			}
		}

		public void TakeDamage(float damage, IHealth invoker)
		{
			if (Current <= 0)
				return;

			float mirrorHit = _randomService.Next(1, 100);
			if (mirrorHit < _progressService.Progress.HeroStats.MirrorHitChance)
			{
				invoker.TakeDamage(_progressService.Progress.HeroStats.MirrorHitMultiplier);
			}
			else
			{
				float magicShield = _randomService.Next(1, 100);
			
				if (magicShield < _progressService.Progress.HeroStats.MagicShieldChance && !_magicShield.IsActive)
					_magicShield.AppearShield();

				if (_magicShield.IsActive)
					damage = 0;

				Current -= damage;
				Animator.PlayHit();
			}
		}

		public void LevelUp()
		{
			if (LevelUpSound != null)
				LevelUpSound.Play();

			Max *= 1.15f;
			Current = Max;
		}

	}
}