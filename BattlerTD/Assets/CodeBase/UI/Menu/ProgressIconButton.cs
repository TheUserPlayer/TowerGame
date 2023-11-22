using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.PersistentProgress;
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

		private void Awake()
		{
			ProgressService = AllServices.Container.Single<IPersistentProgressService>();
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