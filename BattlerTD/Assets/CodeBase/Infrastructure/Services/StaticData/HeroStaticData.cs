using UnityEngine;

namespace CodeBase.Infrastructure.Services.StaticData
{
	[CreateAssetMenu(fileName = "HeroData", menuName = "StaticData/Hero")]
	public class HeroStaticData : ScriptableObject
	{
		[Header("HP")]
		[Range(1, 10000)]
		public int Hp;
		[Range(1, 10000)]
		public int KingHp;

		[Header("Sword")]
		[Range(1, 5000)]
		public float SwordDamage;
		[Range(0.5f, 50)]
		public float Cleavage;	
		
		[Header("Bow")]
		[Range(1, 500)]
		public float ArrowDamage;
		[Range(0.5f, 50)]
		public float ArrowSpeed;

		[Header("Movement")]
		[Range(1, 100)]
		public float MoveSpeed;
		
		public GameObject Prefab;
	}
}