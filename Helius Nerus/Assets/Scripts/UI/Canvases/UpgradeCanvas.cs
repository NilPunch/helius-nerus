﻿using TMPro;
using UnityEngine;

namespace HNUI
{
	class UpgradeCanvas : MonoBehaviour
	{
		[SerializeField] private UpgradeController _upgradeController = null;
		[SerializeField] private TextMeshProUGUI _upgradeName = null; 
		[SerializeField] private TextMeshProUGUI _upgradeDesc = null;
		[SerializeField] private TextMeshProUGUI _bigChoseHint = null;

		private void Awake()
		{
			PlayerLevelStartAnimation.AnimationEnded += PlayerLevelStartAnimation_AnimationEnded;
			UpgradeController.ModifierApplyed += UpgradeController_ModifierApplyed;
			DragAndDropModifier.SelectionChanged += DragModifier_SelectionChanged;
			WeaponDropTarget.WeaponSelected += WeaponDropTarget_WeaponSelected;
			gameObject.SetActive(false);
		}

		private void WeaponDropTarget_WeaponSelected(PlayerWeapon obj)
		{
			if (obj == null)
			{
				_bigChoseHint.gameObject.SetActive(true);
				_upgradeName.gameObject.SetActive(false);
				_upgradeDesc.gameObject.SetActive(false);
			}
			else
			{
				_bigChoseHint.gameObject.SetActive(false);
				_upgradeName.gameObject.SetActive(true);
				_upgradeDesc.gameObject.SetActive(true);

				_upgradeName.text = "Selected Weapon Modifiers";
				_upgradeDesc.text = PlayerWeapon.GetDescription(obj);
			}
		}

		private void DragModifier_SelectionChanged(DragAndDropModifier obj)
		{
			if (obj == null)
			{
				_bigChoseHint.gameObject.SetActive(true);
				_upgradeName.gameObject.SetActive(false);
				_upgradeDesc.gameObject.SetActive(false);
			}
			else
			{
				_bigChoseHint.gameObject.SetActive(false);
				_upgradeName.gameObject.SetActive(true);
				_upgradeDesc.gameObject.SetActive(true);

				_upgradeName.text = obj.RelatedUpgradePair.WeaponModifier.MyEnumValue.ToString();
				_upgradeDesc.text = obj.RelatedUpgradePair.Description;
			}
		}

		private void UpgradeController_ModifierApplyed()
		{
			gameObject.SetActive(false);
		}

		private void PlayerLevelStartAnimation_AnimationEnded()
		{
			gameObject.SetActive(true);
			_bigChoseHint.gameObject.SetActive(true);
			_upgradeName.gameObject.SetActive(false);
			_upgradeDesc.gameObject.SetActive(false);
			_upgradeController.ActivateModifierSelection();
		}

		private void OnDestroy()
		{
			UpgradeController.ModifierApplyed -= UpgradeController_ModifierApplyed;
			PlayerLevelStartAnimation.AnimationEnded -= PlayerLevelStartAnimation_AnimationEnded;
		}
	}
}