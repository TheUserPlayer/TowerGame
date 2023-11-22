using System;
using CodeBase.Infrastructure.Services.Inputs;
using CodeBase.Infrastructure.Services.StaticData;
using DG.Tweening;
using UnityEngine.Events;

namespace CodeBase.UI.Menu
{
	public class HeroAttackIconButton : ProgressIconButton
	{
		public override void UpdateTalent()
		{
			base.UpdateTalent();
			IncreaseDamage();
		}

		private void IncreaseDamage() =>
			ProgressService.Progress.HeroStats.DamageMultiplier += 0.05f;
	}


}