using UnityEngine;

namespace CodeBase.Enemy
{
	public class DividedSlime : BaseSlime
	{
		public void Construct(SlimeDividerCounter slimeDividerCounter) =>
			_divideCounter = slimeDividerCounter;

		protected override void DivideSlime(Transform spot)
		{
			base.DivideSlime(transform);

			DescribeDeath();
		}

		public void DescribeDeath() =>
			_enemyDeath.Happened -= DeathHappened;
	}
}