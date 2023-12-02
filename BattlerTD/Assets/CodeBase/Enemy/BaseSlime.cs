using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Factory;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Enemy
{
	public class BaseSlime : MonoBehaviour
	{
		[SerializeField] protected EnemyDeath _enemyDeath;
		[SerializeField] protected SlimeDividerCounter _divideCounter;

		private IGameFactory _gameFactory;

		private void Start()
		{
			_enemyDeath.Happened += DeathHappened;
			_gameFactory = AllServices.Container.Single<IGameFactory>();
		}

		private void OnDestroy()
		{
			_enemyDeath.Happened -= DeathHappened;
		}

		protected void DeathHappened()
		{
			if (_divideCounter.DivideCounter < 3)
				DivideSlime(transform);
		}

		protected virtual void DivideSlime(Transform spot)
		{
			DividedSlime monster1 = _gameFactory.CreateMonster(MonsterTypeId.SlimeDivided, spot, 0.5f).GetComponent<DividedSlime>();
			DividedSlime monster2 = _gameFactory.CreateMonster(MonsterTypeId.SlimeDivided, spot, 0.5f).GetComponent<DividedSlime>();

			monster1.transform.localScale *= 0.7f;
			monster2.transform.localScale *= 0.7f;
			monster1.Construct(_divideCounter);
			monster2.Construct(_divideCounter);
			if (_divideCounter.DivideCounter > 0)
			{
				monster1.transform.localScale *= 0.7f;
				monster2.transform.localScale *= 0.7f;
				monster1.DescribeDeath();
				monster2.DescribeDeath();
			}
			_divideCounter.DivideCounter++;
		}
	}
}