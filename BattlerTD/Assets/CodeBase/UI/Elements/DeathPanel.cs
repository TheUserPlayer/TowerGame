using System;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.States;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Elements
{
	public class DeathPanel : MonoBehaviour
	{
		public TextMeshProUGUI DeathText;
		public Button MainMenuButton;

		private IGameStateMachine _stateMachine;
		
		private void Start()
		{
			_stateMachine = AllServices.Container.Single<IGameStateMachine>();
			MainMenuButton.onClick.AddListener(GoToMainMenu);
			Time.timeScale = 0;
		}

		private void OnDestroy() =>
			Time.timeScale = 1;

		private void GoToMainMenu() =>
			_stateMachine.Enter<LoadProgressState>();
	}
}