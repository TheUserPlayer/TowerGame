using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Inputs;
using UnityEngine;

namespace CodeBase.Hero
{
	public abstract class HeroAttack : MonoBehaviour
	{
		private const string Hittable = "Hittable";
		
		[SerializeField] protected GameObject _sword;
		[SerializeField] protected GameObject _bow;
		[SerializeField] protected HeroAnimator _animator;
		[SerializeField] protected float _meleeAttackTimer;

		protected IInputService _inputService;

		protected float _attackButtonPressedTimer;
		protected static int _layerMask;

		private void Awake()
		{
			_inputService = AllServices.Container.Single<IInputService>();
			_inputService.AttackButtonUnpressed += AttackButtonUnpressed;

			_layerMask = 1 << LayerMask.NameToLayer(Hittable);
		}

		private void Update()
		{
			if (!_inputService.IsAttackButton())
				return;

			if(_inputService.Tap)
				Debug.Log("da");
			OnUpdate();
			UpdateTimer();
		}

		protected virtual void OnUpdate() { }

		private void UpdateTimer() =>
			_attackButtonPressedTimer += Time.deltaTime;

		protected virtual void AttackButtonUnpressed() =>
			_attackButtonPressedTimer = 0;
	}
}