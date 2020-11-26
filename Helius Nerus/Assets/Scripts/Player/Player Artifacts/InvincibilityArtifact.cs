﻿using System.Collections;
using UnityEngine;

class InvincibilityArtifact : PlayerArtifact
{
	private const float EFFECT_TIME_SCALE = 1.1f;

	private SpriteRenderer _renderer;

    public override string MyEnumName => "InvincibilityArtifact";

    public override ArtifactType MyEnum => ArtifactType.InvincibilityArtifact;

    public override PlayerArtifact Clone()
	{
		return (InvincibilityArtifact)this.MemberwiseClone();
	}

	public override void OnPick()
	{
		Player.PlayerTookDamage += Player_PlayerTookDamage;

        _renderer = Player.SpriteRenderer;
	}

	public override void OnDrop()
	{
		Player.PlayerTookDamage -= Player_PlayerTookDamage;
	}

	private void Player_PlayerTookDamage()
	{
		CoroutineProcessor.Instance.StartCoroutine(OnProc());
	}

	public IEnumerator OnProc()
	{
		float invinsibilityLeft = Player.PlayerParameters.InvinsibilityTime;
		Player.CollideWithDamageDealers = false;
		float effectToggleTime = invinsibilityLeft / EFFECT_TIME_SCALE;
		_renderer.enabled = false;
		
		while (invinsibilityLeft > 0f)
		{
			invinsibilityLeft -= TimeManager.PlayerDeltaTime;
			if (invinsibilityLeft < effectToggleTime)
			{
				effectToggleTime = invinsibilityLeft / EFFECT_TIME_SCALE;
				_renderer.enabled = !_renderer.enabled;
			}
			yield return null;
		}

		Player.CollideWithDamageDealers = true;
		_renderer.enabled = true;
	}
}
