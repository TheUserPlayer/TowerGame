namespace CodeBase.UI.Menu
{
	public class MirrorHitIconButton : ProgressIconButton
	{
		public override void UpdateTalent()
		{
			base.UpdateTalent();
			IncreaseMirrorHit();
		}

		private void IncreaseMirrorHit() =>
			ProgressService.Progress.HeroStats.MirrorHitChance += 2;
	}
}