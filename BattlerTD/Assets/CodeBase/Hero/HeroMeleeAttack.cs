using CodeBase.Data;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Logic;
using UnityEngine;
using PhysicsDebug = CodeBase.Logic.PhysicsDebug;

namespace CodeBase.Hero
{

	[RequireComponent(typeof(HeroAnimator), typeof(CharacterController))]
	public class HeroMeleeAttack : HeroAttack, ISavedProgressReader
	{
		private GameObject _impactVfx;
		private Collider[] _hits = new Collider[3];
		private Stats _stats;

		[SerializeField] private float _damage;
		[SerializeField] private ParticleSystem _impactFxPrefab;
		
		public void LoadProgress(PlayerProgress progress) =>
			_stats = progress.HeroStats;

		protected override void AttackButtonUnpressed()
		{
			if (_attackButtonPressedTimer <= _meleeAttackTimer)
				_animator.PlayAttack();

			base.AttackButtonUnpressed();
		}

		private void OnAttack()
		{
			PhysicsDebug.DrawDebug(transform.position + transform.forward, _stats.DamageRadius, 1.0f);
			for (int i = 0; i < Hit(); ++i)
			{
				_hits[i].transform.GetComponentInParent<IHealth>().TakeDamage(_damage);
				PlayTakeDamageFx(_hits[i].transform.position);
			}
		}

		private int Hit() =>
			Physics.OverlapSphereNonAlloc(transform.position + transform.forward, _stats.DamageRadius, _hits, _layerMask);

		private void PlayTakeDamageFx(Vector3 position)
		{
			_impactFxPrefab.transform.position = position;
			_impactFxPrefab.Play();
		}
	}
}