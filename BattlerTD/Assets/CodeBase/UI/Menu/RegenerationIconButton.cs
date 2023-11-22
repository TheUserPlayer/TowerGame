namespace CodeBase.UI.Menu
{
	public class RegenerationIconButton : ProgressIconButton
	{
		public override void UpdateTalent()
		{
			base.UpdateTalent();
			IncreaseRegeneration();
		}

		private void IncreaseRegeneration() =>
			ProgressService.Progress.HeroState.Regeneration += 1;
	}
}