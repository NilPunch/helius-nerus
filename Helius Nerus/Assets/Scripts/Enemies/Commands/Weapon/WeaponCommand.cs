﻿using UnityEngine;

public abstract class WeaponCommand : IEnemyCommand
{
	protected WeaponCommandData CommandData;

	public bool WorkOnce => CommandData.WorkOnce;

	public void SetParametrs(WeaponCommandData commandData)
	{
		CommandData = commandData.Clone();
		CommandData.StoreData();
	}

	public abstract void Tick(Transform transform);
	public abstract bool IsEnded();
	public abstract void Reset();

	public WeaponCommand Clone()
	{
		return (WeaponCommand)this.MemberwiseClone();
	}

	public virtual void Shoot(WeaponCommandData commandData)
	{
		float _halfBulletAmount = 0f;
		float _angleStep = 0f;

		if (commandData.BulletAmount > 1)
		{
			_halfBulletAmount = (commandData.BulletAmount - 1) / 2f;
			_angleStep = commandData.SpreadAngle / _halfBulletAmount / 2f;
		}
		else
		{
			_halfBulletAmount = 0f;
			_angleStep = 0f;
		}

		for (int i = 0; i < commandData.BulletAmount; ++i)
		{
            GameObject bullet = BulletPoolsContainer.Instance.GetObjectFromPool(commandData.BulletType);
            (bullet.GetComponent(typeof(IBulletMovement)) as IBulletMovement).SpeedMultiplier = commandData.BulletSpeed;
			bullet.transform.position = commandData.Position;
            bullet.transform.localScale = commandData.BulletSize * Vector3.one;
			bullet.transform.localEulerAngles = new Vector3(0f, 0f, Vector2.Angle(Vector2.up, commandData.Direction) + (_angleStep * (i - _halfBulletAmount)));
		} 
	}
}