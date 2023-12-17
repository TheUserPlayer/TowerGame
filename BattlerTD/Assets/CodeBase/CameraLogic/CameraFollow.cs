using UnityEngine;

namespace CodeBase.CameraLogic
{
	[ExecuteInEditMode]
	public class CameraFollow : MonoBehaviour
	{
		[SerializeField] private float _rotationAngleX;
		[SerializeField] private float _rotationAngleY;
		[SerializeField] private float _distance;
		[SerializeField] private float _offsetY;
		[SerializeField] private float _offsetX;
		[SerializeField] private float _offsetZ;

		public Transform _following;

		private void LateUpdate()
		{
			if (_following == null)
			{
				return;
			}

			Quaternion rotation = Quaternion.Euler(_rotationAngleX, _rotationAngleY, 0);
			Vector3 position = rotation * new Vector3(0, 0, -_distance) + FollowingPointPosition();

			transform.rotation = rotation;
			transform.position = position;
		}

		public void Follow(GameObject following) =>
			_following = following.transform;

		private Vector3 FollowingPointPosition()
		{
			Vector3 followingPosition = _following.position;
			followingPosition.y += _offsetY;
			followingPosition.x += _offsetX;
			followingPosition.z += _offsetZ;

			return followingPosition;
		}
	}
}