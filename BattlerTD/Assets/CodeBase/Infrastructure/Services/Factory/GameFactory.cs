using System;
using System.Collections.Generic;
using CodeBase.AssetManagement;
using CodeBase.Enemy;
using CodeBase.Hero;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Services.Inputs;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.Services.Randomizer;
using CodeBase.Infrastructure.Services.StaticData;
using CodeBase.Infrastructure.Services.Timers;
using CodeBase.Logic;
using CodeBase.Logic.EnemySpawners;
using CodeBase.StaticData;
using CodeBase.Tower;
using CodeBase.UI.Elements;
using CodeBase.UI.Services.Windows;
using UnityEngine;
using UnityEngine.AI;
using Object = UnityEngine.Object;

namespace CodeBase.Infrastructure.Services.Factory
{
	public class GameFactory : IGameFactory
	{
		public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
		public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();

		private readonly IAssetProvider _assets;
		private readonly IStaticDataService _staticData;
		private readonly IRandomService _randomService;
		private readonly IPersistentProgressService _persistentProgressService;
		private readonly IWindowService _windowService;
		private readonly IInputService _inputService;
		private readonly IBuildingService _buildingServie;
		private readonly List<SpawnPoint> _spawners = new List<SpawnPoint>();
		private GameObject _heroGameObject;

		private List<GameObject> _monsters = new List<GameObject>();
		private BossSpawnPoint _bossSpawner;
		private ITimerService _timerService;
		private GameObject _mainPumpkin;
		private Hud _hud;
		private TowerPanel _towerPanel;

		public GameObject MainPumpkinGameObject => _mainPumpkin;
		public GameObject HeroGameObject => _heroGameObject;
		public BossSpawnPoint BossSpawner => _bossSpawner;
		public List<SpawnPoint> Spawners => _spawners;
		public Action<GameObject> MonsterCreated { get; set; }
		public List<GameObject> Monsters
		{
			get
			{
				return _monsters;
			}
			set
			{
				_monsters = value;
			}
		}
		public Hud HUD
		{
			get
			{
				return _hud;
			}
		}
		public TowerPanel Panel
		{
			get
			{
				return _towerPanel;
			}
		}


		public GameFactory(IAssetProvider assets, IStaticDataService staticData, IRandomService randomService, IPersistentProgressService persistentProgressService,
			IWindowService windowService, ITimerService timerService, IInputService inputService)
		{
			_assets = assets;
			_staticData = staticData;
			_randomService = randomService;
			_persistentProgressService = persistentProgressService;
			_windowService = windowService;
			_timerService = timerService;
			_inputService = inputService;
		}

		public GameObject CreateHero(Vector3 at)
		{
			_heroGameObject = InstantiateRegistered(AssetPath.HeroPath, at);
			_heroGameObject.GetComponent<HeroMove>().Construct(_timerService, _inputService);
			return _heroGameObject;
		}

		public Hud CreateHud()
		{
			_hud = InstantiateRegistered(AssetPath.HudPath).GetComponent<Hud>();
			_hud.GetComponentInChildren<LootCounter>()
				.Construct(_persistentProgressService.Progress.WorldData);

			_towerPanel = _hud.GetComponentInChildren<TowerPanel>();
			foreach (OpenWindowButton openWindowButton in HUD.GetComponentsInChildren<OpenWindowButton>())
				openWindowButton.Init(_windowService);

			return _hud;
		}

		public LootPiece CreateLoot()
		{
			LootPiece lootPiece = InstantiateRegistered(AssetPath.Loot)
				.GetComponent<LootPiece>();

			lootPiece.Construct(_persistentProgressService.Progress.WorldData);

			return lootPiece;
		}

		public GameObject CreateBoss(MonsterTypeId typeId, Transform parent, float progressMultiplier)
		{
			MonsterStaticData monsterData = _staticData.ForMonster(typeId);
			Vector3 spawnPosition = new Vector3(parent.position.x + 17, parent.position.y);
			GameObject monster = Object.Instantiate(monsterData.Prefab, spawnPosition, Quaternion.identity);

			IHealth health = monster.GetComponent<IHealth>();
			health.Current = monsterData.Hp;
			health.Max = monsterData.Hp;

			monster.GetComponent<ActorUI>()?.Construct(health, _persistentProgressService);
			monster.GetComponent<NavMeshAgent>().speed = monsterData.MoveSpeed;

			EnemyAttack enemyAttack = monster.GetComponentInChildren<EnemyAttack>();
			enemyAttack.Construct(_heroGameObject.transform);
			enemyAttack.Damage = monsterData.Damage * progressMultiplier;
			enemyAttack.Cleavage = monsterData.Cleavage;
			enemyAttack.EffectiveDistance = monsterData.EffectiveDistance;

			monster.GetComponent<AgentMoveToPlayer>()?.Construct(_heroGameObject.transform);
			monster.GetComponent<RotateToHero>()?.Construct(_heroGameObject.transform);

			LootSpawner lootSpawner = monster.GetComponentInChildren<LootSpawner>();
			Debug.Log(lootSpawner);
			lootSpawner.Construct(this, _randomService);
			lootSpawner.SetLootValue(monsterData.MinLootValue, monsterData.MaxLootValue);

			//	Monsters.Add(monster);
			return monster;
		}

		public GameObject CreateTower(TowerType towerType, Vector3 position, Quaternion rotation)
		{
			TowerStaticData towerData = _staticData.ForTower(towerType);
			GameObject tower = Object.Instantiate(towerData.Prefab, position, rotation);
			return tower;
		}

		public GameObject CreateMonster(MonsterTypeId typeId, Transform parent, float progressMultiplier)
		{
			MonsterStaticData monsterData = _staticData.ForMonster(typeId);
			GameObject monster = Object.Instantiate(monsterData.Prefab, parent.position, Quaternion.identity);

			IHealth health = monster.GetComponent<IHealth>();
			health.Current = monsterData.Hp;
			health.Max = monsterData.Hp;

			monster.GetComponent<ActorUI>()?.Construct(health, _persistentProgressService);
			monster.GetComponent<NavMeshAgent>().speed = monsterData.MoveSpeed;

			EnemyAttack enemyAttack = monster.GetComponentInChildren<EnemyAttack>();
			enemyAttack.Construct(_mainPumpkin.transform);
			enemyAttack.Damage = monsterData.Damage * progressMultiplier;
			enemyAttack.Cleavage = monsterData.Cleavage;
			enemyAttack.EffectiveDistance = monsterData.EffectiveDistance;

			monster.GetComponent<Aggro>()?.Construct(_heroGameObject.transform);
			monster.GetComponent<AgentMoveToPlayer>()?.Construct(_mainPumpkin.transform);
			monster.GetComponent<RotateToHero>()?.Construct(_mainPumpkin.transform);

			LootSpawner lootSpawner = monster.GetComponentInChildren<LootSpawner>();
			lootSpawner.Construct(this, _randomService);
			lootSpawner.SetLootValue(monsterData.MinLootValue, monsterData.MaxLootValue);

			Monsters.Add(monster);
			MonsterCreated?.Invoke(monster);
			return monster;
		}

		public void CreateSpawner(string spawnerId, Vector3 at, Quaternion rotation, MonsterTypeId monsterTypeId)
		{
			SpawnPoint spawner = InstantiateRegistered(AssetPath.Spawner, at).GetComponent<SpawnPoint>();
			spawner.transform.rotation = rotation;
			spawner.Construct(this);
			spawner.MonsterTypeId = monsterTypeId;
			spawner.Id = spawnerId;
			Spawners.Add(spawner);
		}

		public void CreateBossSpawner(Vector3 at, MonsterTypeId monsterTypeId, Transform parent)
		{
			_bossSpawner = InstantiateRegistered(AssetPath.BossSpawner, at).GetComponent<BossSpawnPoint>();
			_bossSpawner.transform.parent = parent;
			_bossSpawner.Construct(this, _timerService);
			_bossSpawner.MonsterTypeId = monsterTypeId;
		}

		public GameObject CreateKing(Vector3 at)
		{
			_mainPumpkin = InstantiateRegistered(AssetPath.KingPath, at);
			_mainPumpkin.transform.Rotate(0, 180, 0);
			return _mainPumpkin;
		}

		public Grid CreateGrid()
		{
			Grid grid = InstantiateRegistered(AssetPath.Grid).GetComponent<Grid>();
			return grid;
		}

		public void Dispose()
		{
			Object.Destroy(_heroGameObject);

			foreach (GameObject monster in Monsters)
			{
				Object.Destroy(monster.gameObject);
			}

			_monsters.Clear();
		}

		private void Register(ISavedProgressReader progressReader)
		{
			if (progressReader is ISavedProgress progressWriter)
				ProgressWriters.Add(progressWriter);

			ProgressReaders.Add(progressReader);
		}

		public void Cleanup()
		{
			ProgressReaders.Clear();
			ProgressWriters.Clear();
		}

		private GameObject InstantiateRegistered(string prefabPath, Vector3 at)
		{
			GameObject gameObject = _assets.Instantiate(path: prefabPath, at: at);
			RegisterProgressWatchers(gameObject);

			return gameObject;
		}

		private GameObject InstantiateRegistered(string prefabPath)
		{
			GameObject gameObject = _assets.Instantiate(path: prefabPath);
			RegisterProgressWatchers(gameObject);

			return gameObject;
		}

		private void RegisterProgressWatchers(GameObject gameObject)
		{
			foreach (ISavedProgressReader progressReader in gameObject.GetComponentsInChildren<ISavedProgressReader>())
				Register(progressReader);
		}

	}
}