namespace CodeBase.UI.Menu
{
	public class HeroHpIconButton : ProgressIconButton
	{
		public override void UpdateTalent()
		{
			base.UpdateTalent();
			IncreaseHp();
		}

		private void IncreaseHp() =>
			ProgressService.Progress.HeroState.MaxHP *= 1.05f;
	}
}