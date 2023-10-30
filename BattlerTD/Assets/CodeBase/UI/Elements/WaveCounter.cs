using System;
using CodeBase.Data;
using TMPro;
using UnityEngine;

namespace CodeBase.UI.Elements
{
	public class WaveCounter : MonoBehaviour
	{
		public TextMeshProUGUI Counter;
		private KillData _killData;

		public void Construct(KillData killData)
		{
			_killData = killData;
			_killData.WaveChanged += UpdateCounter;
		}

		private void OnDestroy()
		{
			_killData.WaveChanged -= UpdateCounter;
		}

		private void Start() =>
			UpdateCounter();

		private void UpdateCounter() => 
			Counter.text = $"{_killData.CurrentMonsterWaves} / {_killData.MonsterWavesForFinish}";
	}
}