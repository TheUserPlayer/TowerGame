using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.States;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Elements
{
	public class TowerPanel : MonoBehaviour
	{
		[SerializeField] private Button _nextStageButton;

		private IGameStateMachine _stateMachine;
		private bool _isClicked;
		public bool IsClicked
		{
			get
			{
				return _isClicked;
			}
			set
			{
				_isClicked = value;
			}
		}

		private void Awake()
		{
			_isClicked = false;
			_stateMachine = AllServices.Container.Single<IGameStateMachine>();
		}

		private void Start()
		{
			_nextStageButton.onClick.AddListener(NextWave);
		}

		private void NextWave()
		{
			if (IsClicked)
				return;

			IsClicked = true;
			_stateMachine.Enter<GameLoopAttackState>();
		}
	}
}