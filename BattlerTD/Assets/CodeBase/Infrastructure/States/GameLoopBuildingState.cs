using CodeBase.Hero;
using CodeBase.Infrastructure.Services.Audio;
using CodeBase.Infrastructure.Services.Factory;
using CodeBase.Logic;

namespace CodeBase.Infrastructure.States
{
	public class GameLoopBuildingState : IState
	{
		private readonly LoadingCurtain _loadingCurtain;
		private readonly IGameFactory _gameFactory;
		private readonly IAudioService _audioService;
		private readonly GameStateMachine _stateMachine;
		private IHealth _heroHealth;
		private IHealth _kingHealth;

		public GameLoopBuildingState(IGameFactory gameFactory, IAudioService audioService,
			GameStateMachine stateMachine)
		{
			_gameFactory = gameFactory;
			_audioService = audioService;
			_stateMachine = stateMachine;
		}

		public void Exit() =>
			_gameFactory.HUD.DisappearTowerPanel();

		public void Update() { }

		public void Enter()
		{
			if (_heroHealth == null)
			{
				_heroHealth = _gameFactory.HeroGameObject.GetComponent<IHealth>();
				_kingHealth = _gameFactory.KingGameObject.GetComponent<IHealth>();
			}
			
			_kingHealth.Current += 0.1f * _kingHealth.Max;
			_audioService.PlayMainMenuMusic();

			_gameFactory.HUD.AppearTowerPanel();
		}

		private void SubscribeHeroDeath()
		{
			_gameFactory.HeroGameObject.TryGetComponent(out HeroDeath heroDeath);
			heroDeath.Restart += Restart;
			_gameFactory.KingGameObject.TryGetComponent(out KingHealth health);
			health.Restart += Restart;
		}


		private void Restart()
		{
			_loadingCurtain.Show();

			_stateMachine.Enter<RestartLevelState>();
		}
	}
}