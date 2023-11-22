namespace CodeBase.UI.Menu
{
	public class CriticalHitIconButton : ProgressIconButton
	{
		public override void UpdateTalent()
		{
			base.UpdateTalent();
			IncreaseCriticalHit();
		}

		private void IncreaseCriticalHit()
		{
			ProgressService.Progress.HeroStats.CriticalChance += 1;
			ProgressService.Progress.HeroStats.CriticalMultiplier += 0.25f;
		}
	}
}