using CodeBase.Data;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Logic;
using UnityEngine;
using PhysicsDebug = CodeBase.Logic.PhysicsDebug;

namespace CodeBase.Hero
{

	[RequireComponent(typeof(HeroAnimator), typeof(CharacterController))]
	public class HeroMeleeAttack : HeroAttack
	{
		private GameObject _impactVfx;
		private Collider[] _hits = new Collider[3];

		[SerializeField] private float _damage;
		[SerializeField] private ParticleSystem _impactFxPrefab;
		
		protected override void AttackButtonUnpressed()
		{
			if (_attackButtonPressedTimer <= _meleeAttackTimer)
			{
				Debug.Log(this);
				_animator.PlayAttack();
			}

			base.AttackButtonUnpressed();
		}

		private void OnAttack()
		{
			PhysicsDebug.DrawDebug(transform.position + transform.forward, _stats.SwordRadius, 1.0f);
			for (int i = 0; i < Hit(); ++i)
			{
				_hits[i].transform.GetComponentInParent<IHealth>().TakeDamage(_stats.SwordDamage * _stats.DamageMultiplier);
				PlayTakeDamageFx(_hits[i].transform.position);
			}
		}

		private int Hit() =>
			Physics.OverlapSphereNonAlloc(transform.position + transform.forward, _stats.SwordRadius, _hits, _layerMask);

		private void PlayTakeDamageFx(Vector3 position)
		{
			_impactFxPrefab.transform.position = position;
			_impactFxPrefab.Play();
		}
	}
}