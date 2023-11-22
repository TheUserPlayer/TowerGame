namespace CodeBase.UI.Menu
{
	public class MoveSpeedIconButton : ProgressIconButton
	{
		public override void UpdateTalent()
		{
			base.UpdateTalent();
			IncreaseMoveSpeed();
		}

		private void IncreaseMoveSpeed() =>
			ProgressService.Progress.HeroStats.MoveSpeed += 1;
	}
}