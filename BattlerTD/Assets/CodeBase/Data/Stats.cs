using System;

namespace CodeBase.Data
{
	[Serializable]
	public class Stats
	{
		public float SwordDamage;
		public float SwordRadius;
		public float ArrowDamage;
		public float ArrowSpeed;
		public float CriticalChance;
		public float DamageMultiplier;
		public float CriticalMultiplier;
		public float MirrorHitChance;
		public float MirrorHitMultiplier;
		public float MoveSpeed;
		public float MagicShieldChance = 15;
	}
}