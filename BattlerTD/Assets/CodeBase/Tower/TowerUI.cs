using System;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Inputs;
using SimpleInputNamespace;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Tower
{
	public class TowerUI : MonoBehaviour
	{
		[SerializeField] private TowerType _towerType;
		[SerializeField] private ButtonInputUI _button;

		private IBuildingService _buildingService;
		private IInputService _inputService;

		private void Awake()
		{
			_buildingService = AllServices.Container.Single<IBuildingService>();
			_inputService = AllServices.Container.Single<IInputService>();
			_inputService.TowerButtonPressed += InitTower;
			Debug.Log(_buildingService);
			Debug.Log(_button);
		}

		private void OnDestroy()
		{
			_inputService.TowerButtonPressed -= InitTower;
		}

		private void InitTower()
		{
			if (_button.button.value)
			{
				_buildingService.InitTowerType(_towerType);
			}
		}
	}
}