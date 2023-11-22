namespace CodeBase.UI.Menu
{
	public class ArrowDamageIconButton : ProgressIconButton
	{
		public override void UpdateTalent()
		{
			base.UpdateTalent();
			ImproveBow();
		}

		private void ImproveBow() =>
			ProgressService.Progress.HeroStats.ArrowDamage *= 1.15f;

	}
}