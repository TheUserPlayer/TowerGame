using System.Collections;
using CodeBase.Hero;
using CodeBase.Logic;
using CodeBase.Weapon;
using UnityEngine;

namespace CodeBase.Enemy
{

	[RequireComponent(typeof(EnemyAnimator))]
	public class EnemyRangeAttack : Attack
	{
		public CheckClosestEnemy ClosestEnemy;

		[SerializeField] private Transform _shootPosition;
		[SerializeField] private GameObject _bullet;
		[SerializeField] private GameObject _shootVFX;

		private bool _isShooting;


		public override void EnableAttack()
		{
			base.EnableAttack();
			MoveToPlayer.StopMoving();
		}

		public override void DisableAttack()
		{
			base.DisableAttack();
			MoveToPlayer.StartMoving();
		}

		private void OnAttack()
		{
			if (ClosestEnemy.ClosestEnemy)
			{
				StartCoroutine(Shoot());
			}
		}
		
		private void OnAttackEnded()
		{
			MoveToPlayer.enabled = true;
			_attackCooldown = AttackCooldown;
			_isAttacking = false;
		}

		private IEnumerator Shoot()
		{
			_isShooting = true;

			BulletEnemy bulletEnemy = Instantiate(_bullet, _shootPosition.position, Quaternion.identity).GetComponent<BulletEnemy>();
			bulletEnemy.transform.LookAt(ClosestEnemy.ClosestEnemy);
			//Instantiate(_shootVFX, transform.position, Quaternion.identity);
			yield return new WaitForSeconds(AttackCooldown);

			_isShooting = false;
		}
	}

}