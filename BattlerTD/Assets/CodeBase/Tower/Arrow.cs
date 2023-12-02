using System;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Tower
{
	public class Arrow : MonoBehaviour
	{
		[SerializeField] private Rigidbody _rigidbody;
		[SerializeField] private GameObject _explosionVFX;
		
		private IPersistentProgressService _persistentProgress;
		public float Damage { get; set; }
		public float MoveSpeed { get; set; }

		private void Start() =>
			Destroy(gameObject, 5);

		private void Update() =>
			_rigidbody.velocity = -transform.forward * (MoveSpeed * Time.deltaTime);

		private void OnTriggerEnter(Collider other)
		{
			other.transform.GetComponentInParent<IHealth>().TakeDamage(Damage);
			Instantiate(_explosionVFX, other.transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
	}
}