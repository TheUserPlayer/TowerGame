namespace CodeBase.UI.Menu
{
	public class SwordDamageIconButton : ProgressIconButton
	{
		public override void UpdateTalent()
		{
			base.UpdateTalent();
			ImproveSword();
		}

		private void ImproveSword() =>
			ProgressService.Progress.HeroStats.SwordDamage *= 1.15f;
	}
}