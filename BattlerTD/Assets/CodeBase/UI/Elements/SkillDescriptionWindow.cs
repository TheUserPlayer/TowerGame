using System;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.UI.Menu;
using DG.Tweening;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Elements
{
	public class SkillDescriptionWindow : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI _skillDescription;
		[SerializeField] private Image _skillDescriptionBackground;
		[SerializeField] private Button _closeButton;
		[SerializeField] private Button _buyButton;
		[SerializeField] private float _duration;

		private ProgressIconButton _talentButton;
		private void Awake()
		{
			AllServices.Container.Single<IPersistentProgressService>();
			_closeButton.onClick.AddListener(Disappear);
			_buyButton.onClick.AddListener(BuyTalent);
		}

		private void BuyTalent()
		{
			_talentButton.UpdateTalent();
		}

		public void Appear(ProgressIconButton talentButton)
		{
			_skillDescription.DOFade(1, _duration);
			_skillDescriptionBackground.DOFade(1, _duration);
			_closeButton.targetGraphic.DOFade(1, _duration);
			_buyButton.targetGraphic.DOFade(1, _duration);
			_buyButton.enabled = true;
			_talentButton = talentButton;
		}		
		
		public void Disappear()
		{
			_buyButton.enabled = false;
			_skillDescription.DOFade(0, _duration);
			_skillDescriptionBackground.DOFade(0, _duration);
			_closeButton.targetGraphic.DOFade(0, _duration);
			_buyButton.targetGraphic.DOFade(0, _duration);
		}

		public void SetDescription(string description)
		{
			Debug.Log("yes itsPossible");
			_skillDescription.text = description;
		}
	}
}