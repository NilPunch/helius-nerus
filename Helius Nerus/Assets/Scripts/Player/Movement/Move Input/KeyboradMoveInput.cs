﻿using UnityEngine;

class KeyboradMoveInput : IMoveInput
{
	public Vector2 Direction { get; private set; }
	public float Thrust { get; private set; }

	public KeyboradMoveInput()
	{
		Thrust = 1;
	}

	public void ReadInput()
	{
		Vector2 axis = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
		Direction = axis.normalized;
		Thrust = Mathf.Clamp01(axis.magnitude);
		Thrust *= 7f;
	}

	public void Tick(Transform transform, float sens)
	{
		transform.Translate((Vector3)Direction * Thrust * TimeManager.PlayerDeltaTime  * sens, Space.World);
	}
}

