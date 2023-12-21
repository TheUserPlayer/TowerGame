using System;
using System.Collections;
using System.Collections.Generic;
using CodeBase.Data;
using CodeBase.Enemy;
using CodeBase.Infrastructure.Services.Factory;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.Services.Timers;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Logic.EnemySpawners
{

	public class SpawnPoint : MonoBehaviour
	{
		[SerializeField] private float _delayBetweenSpawn = 7;
		public MonsterTypeId MeleeMonsterTypeId;
		public MonsterTypeId RangeMonsterTypeId;

		public Action<SpawnPoint> DestroySpawner;
		public string Id { get; set; }

		private IGameFactory _factory;

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

		public void Construct(IGameFactory gameFactory)
		{
			_factory = gameFactory;
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

		public void StartSpawnMeleeMob(float times)
		{
			_isActive = true;
			StartCoroutine(SpawnMeleeMob(times));
		}

		public void StopSpawn(float times)
		{
			_isActive = false;
			StopCoroutine(SpawnMeleeMob(times));
		}

		private IEnumerator SpawnMeleeMob(float times)
		{
			while (_isActive)
			{
				for (int i = 0; i < times; i++)
				{
					CreateMob(MeleeMonsterTypeId);

					yield return new WaitForSeconds(1);
				}

				yield return new WaitForSeconds(_delayBetweenSpawn);
			}
		}

		private GameObject CreateMob(MonsterTypeId monsterTypeId)
		{
			GameObject monster = _factory.CreateMonster(monsterTypeId, transform);
			return monster;
		}
	}
}