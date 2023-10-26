using UnityEngine;

namespace CodeBase.Tower
{
	public class OrbTransform : MonoBehaviour
	{
		[SerializeField] private float _rayLengthUp = 5.0f;
		[SerializeField] private float _rayLengthDown = 5.0f;
		[SerializeField] private LayerMask _layerMask;
		[SerializeField] private Transform _visual;
		[SerializeField] private float _offset;
		[SerializeField] private float _sideMultiplier;
		
		private Ray _upRay;
		private Ray _downwRay;
		private float _cachedHeightPosition;

		private void Start()
		{
			_cachedHeightPosition = _visual.position.y;
		}

		private void Update()
		{
			Vector3 rayOrigin = transform.position;

			_upRay = new Ray(rayOrigin, transform.up);
			RaycastHit upHit;
			RaycastHit downHit;

			bool raycastUp = Physics.Raycast(_upRay, out upHit, _rayLengthUp * _sideMultiplier, _layerMask);
			bool raycastDown = Physics.Raycast(_upRay, out downHit, _rayLengthDown, _layerMask);
			
			if (raycastUp)
			{
				_visual.localPosition = new Vector3(_visual.localPosition.x, upHit.point.y + _offset, _visual.localPosition.z);
			}

			if (!raycastUp && !raycastDown)
			{
				_visual.localPosition = new Vector3(_visual.localPosition.x, _cachedHeightPosition, _visual.localPosition.z);
			}
		}
	}
}