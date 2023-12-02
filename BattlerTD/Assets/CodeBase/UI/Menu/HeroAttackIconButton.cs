using CodeBase.Infrastructure.Services.Inputs;
using CodeBase.Infrastructure.Services.StaticData;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace CodeBase.UI.Menu
{
	public class HeroAttackIconButton : ProgressIconButton
	{
		public override void UpdateTalent()
		{
			base.UpdateTalent();
			switch (Level)
			{
				case 1:
					IncreaseDamage();
					_heroTierToUpgrade.SwordMist.gameObject.SetActive(true);
					_heroTierToUpgradePreview.SwordMist.gameObject.SetActive(true);
					break;
				case 2:
					IncreaseDamage();
					break;
				case 3:
					IncreaseDamage();
					IncreaseDamage();
					_heroTierToUpgrade.SwordFlares.gameObject.SetActive(true);
					_heroTierToUpgrade.SwordTrail.gameObject.SetActive(true);
					_heroTierToUpgradePreview.SwordFlares.gameObject.SetActive(true);
					_heroTierToUpgradePreview.SwordTrail.gameObject.SetActive(true);
					break;
				case 4:
					IncreaseDamage();
					IncreaseDamage();
					break;
				case 5:
					IncreaseDamage();
					IncreaseDamage();
					IncreaseDamage();
					ChangeMistColorToRed();
					break;
				default:
					break;
			}
		}

		private void ChangeMistColorToRed()
		{
			ParticleSystem.MainModule swordFlaresMain = _heroTierToUpgrade.SwordFlares.main;
			swordFlaresMain.startColor = new Color(0.5f, 0.0f, 0.1f);
		}

		private void IncreaseDamage() =>
			ProgressService.Progress.HeroStats.DamageMultiplier += 0.05f;
	}


}