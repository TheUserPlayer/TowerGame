using CodeBase.Hero;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Factory;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.Services.StaticData;
using CodeBase.UI.Elements;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Menu
{
	public abstract class ProgressIconButton : MonoBehaviour
	{
		[SerializeField] protected SkillDescriptionWindow _skillDescription;

		[SerializeField] protected string Description;
		[SerializeField] protected TextMeshProUGUI LevelText;
		[SerializeField] protected TextMeshProUGUI PriceText;
		[SerializeField] protected int Level;
		public int MaxLevel;
		public Button TalentButton;
		public int Price;

		private float _elapsedTime;
		private bool _isAppeared;
		protected IPersistentProgressService ProgressService;
		protected IGameFactory GameFactory;
		protected IStaticDataService StaticData;
		protected HeroTierToUpgrade _heroTierToUpgrade;
		protected HeroTierToUpgrade _heroTierToUpgradePreview;

		private void Awake()
		{
			ProgressService = AllServices.Container.Single<IPersistentProgressService>();
			GameFactory = AllServices.Container.Single<IGameFactory>();
			StaticData = AllServices.Container.Single<IStaticDataService>();
			_heroTierToUpgradePreview = GameFactory.HeroesPreview.GetComponent<HeroTierToUpgrade>();
			_heroTierToUpgrade = StaticData.ForHero().ThingsToUpgrade;
			TalentButton.onClick.AddListener(OpenDescriptionWindow);
			PriceText.text = Price.ToString();
		}

		public virtual void UpdateTalent()
		{
			if (ProgressService.Progress.WorldData.LootData.CollectedGold < Price || Level >= MaxLevel)
				return;

			ProgressService.Progress.WorldData.LootData.AddGold(-Price);
			Level++;
			LevelText.text = $"{Level}/{MaxLevel}";
		}

		private void OpenDescriptionWindow()
		{
			_skillDescription.SetDescription(Description);
			_skillDescription.Appear(this);
		}
	}
}