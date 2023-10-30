using System.Linq;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Enemy
{
	[RequireComponent(typeof(EnemyAnimator))]
	public class EnemyMeleeAttack : Attack
	{
		public float Cleavage = 0.5f;
		public float EffectiveDistance = 0.5f;
		public float Damage = 10;
		
		private Collider[] _hits = new Collider[1];

		[SerializeField] private ParticleSystem ImpactFxPrefab;

		private void OnAttack()
		{
			if (Hit(out Collider hit))
			{
				//PhysicsDebug.DrawDebugCross(StartPoint(), Cleavage, 1.0f);
				hit.transform.GetComponent<IHealth>().TakeDamage(Damage);
				PlayTakeDamageFx(hit.transform.position);
			}
		}

		private void OnAttackEnded()
		{
			MoveToPlayer.enabled = true;
			_attackCooldown = AttackCooldown;
			_isAttacking = false;
		}
		
		private bool Hit(out Collider hit)
		{
			int hitAmount = Physics.OverlapSphereNonAlloc(StartPoint(), Cleavage, _hits, _layerMask);

			hit = _hits.FirstOrDefault();

			return hitAmount > 0;
		}

		private void OnDrawGizmos() =>
			Gizmos.DrawWireSphere(StartPoint(), Cleavage);

		private Vector3 StartPoint() =>
			new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z) +
			transform.forward * EffectiveDistance;



		private void PlayTakeDamageFx(Vector3 position)
		{
			ImpactFxPrefab.transform.position = position;
			ImpactFxPrefab.Play();
		}


	}
}