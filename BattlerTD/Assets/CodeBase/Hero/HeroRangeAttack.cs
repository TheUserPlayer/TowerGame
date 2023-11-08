using System;
using System.Collections;
using CodeBase.Hero;
using CodeBase.Tower;
using CodeBase.Weapon;
using UnityEngine;

namespace CodeBase.Hero
{


	public class HeroRangeAttack : HeroAttack
	{
		[SerializeField] private RotateJoystick _rotate;
		[SerializeField] private Bullet _arrow;
		[SerializeField] private LineRenderer _attackLine;
		[SerializeField] private Transform _heroView;
		[SerializeField] private Transform _shootPosition;
		[SerializeField] private CheckClosestTarget _checkClosestTarget;

		private bool _isRotating;
		private bool _shootMode;
		private Vector3 _initialMousePosition;

		public Action OnStartAiming;
		public Action OnFinishAiming;
		private Vector3 _aimRotation;
		private Vector3 _baseRotation;
		public bool ShootMode
		{
			get
			{
				return _shootMode;
			}
		}

		private void Start()
		{
			_attackLine.gameObject.SetActive(false);
			_aimRotation = new Vector3(0, 90, 0);
			_baseRotation = new Vector3(0, 0, 0);
		}

		public void OnHideSword() =>
			_sword.SetActive(false);

		public void OnDrawSword()
		{
			_shootMode = false;
			_sword.SetActive(true);
		}

		public void OnDrawBow()
		{
			_rotate.enabled = true;
			OnStartAiming?.Invoke();
			_bow.SetActive(true);
			_heroView.localEulerAngles = _aimRotation;
			_attackLine.transform.forward = transform.forward;
			_attackLine.gameObject.SetActive(true);
		}
		
		public void OnHideBow()
		{
			_heroView.localEulerAngles = _baseRotation;
			_rotate.enabled = false;
			OnFinishAiming?.Invoke();
			_bow.SetActive(false);
		}

		public void Shoot()
		{
			Bullet bullet = Instantiate(_arrow, _shootPosition.position, Quaternion.identity);
			bullet.transform.forward = -_attackLine.transform.forward;
			_attackLine.gameObject.SetActive(false);
		}

		protected override void OnUpdate()
		{
			base.OnUpdate();
			
			if (!ShootMode && _attackButtonPressedTimer >= _meleeAttackTimer)
			{
				_shootMode = true;
				_animator.PlayHideSword();
			}
		}
		
		protected override void AttackButtonUnpressed()
		{
			if (_attackButtonPressedTimer >= _meleeAttackTimer)
				_animator.PlayBowShoot();

			_attackLine.gameObject.SetActive(false);
			base.AttackButtonUnpressed();
		}

	}
}