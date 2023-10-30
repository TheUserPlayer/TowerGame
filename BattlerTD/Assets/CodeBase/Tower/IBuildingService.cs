using CodeBase.Infrastructure.Services;
using UnityEngine;

namespace CodeBase.Tower
{
	public interface IBuildingService : IService
	{
		bool IsActive { get; set; }
		GridLayout GridLayout { get; }
		bool IsDraggingObject { get; }
		Vector3 GetMouseWorldPosition();
		Vector3 SnapCoordinateToGrid(Vector3 position);
		bool CanBePlaced(PlaceableObject placeableObject, bool inAnotherCollider = false);
		void Init();
		void InitTowerType(TowerType towerType);
		bool CanBeMoved(PlaceableObject placeableObject);
	}
}