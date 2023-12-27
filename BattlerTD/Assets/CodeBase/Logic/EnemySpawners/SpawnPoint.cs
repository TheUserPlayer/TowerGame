using System;
using System.Collections;
using System.Collections.Generic;
using CodeBase.Data;
using CodeBase.Enemy;
using CodeBase.Infrastructure.Services.Factory;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.Services.Randomizer;
using CodeBase.Infrastructure.Services.Timers;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Logic.EnemySpawners
{

	public class SpawnPoint : MonoBehaviour
	{
		[SerializeField] private float _delayBetweenSpawn = 7;
		[SerializeField] private float _progressMultiplier;
		public List<MonsterTypeId> MeleeMonsterTypeId;

		public Action<SpawnPoint> DestroySpawner;
		public string Id { get; set; }

		private IGameFactory _factory;
		private IRandomService _randomService;

		private EnemyDeath _enemyDeath;

		private bool _isActive;
		private float _levelStage = 1;
		public bool IsActive
		{
			get
			{
				return _isActive;
			}
			set
			{
				_isActive = value;
			}
		}
		public float DelayBetweenSpawn
		{
			get
			{
				return _delayBetweenSpawn;
			}
			set
			{
				_delayBetweenSpawn = value;
			}
		}

		public void Construct(IGameFactory gameFactory, IRandomService randomService)
		{
			_factory = gameFactory;
			_randomService = randomService;
		}

		private void Start()
		{
			IsActive = true;
		}

		private void OnDestroy()
		{
			IsActive = false;
			DestroySpawner?.Invoke(this);
		}

		public void StartSpawnMeleeMob(float mobsPacks, float mobsInPack)
		{
			_isActive = true;
			StartCoroutine(SpawnMeleeMob(mobsInPack, mobsPacks));
		}

		public void StopSpawn()
		{
			_isActive = false;
			StopCoroutine(SpawnMeleeMob());
		}

		private IEnumerator SpawnMeleeMob(float mobsInPack = 0, float mobsPacks = 0)
		{
			for (int j = 0; j < mobsPacks; j++)
			{
				for (int i = 0; i < mobsInPack; i++)
				{
					CreateMob(MeleeMonsterTypeId[Next()]);

					yield return new WaitForSeconds(1);
				}

				yield return new WaitForSeconds(_delayBetweenSpawn);
			}

			_progressMultiplier *= 1.1f;
		}

		private GameObject CreateMob(MonsterTypeId monsterTypeId)
		{
			GameObject monster = _factory.CreateMonster(monsterTypeId, transform, _progressMultiplier);
			return monster;
		}
		
		private int Next()
		{
			int next = _randomService.Next(0, MeleeMonsterTypeId.Count);
			return next;
		}
	}
}