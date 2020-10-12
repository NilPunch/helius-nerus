﻿using UnityEngine;

public class TransformMover
{
	private readonly IMoveInput _moveInput;
	private readonly Transform _transform;
	private readonly MoveParameters _moveSettings;

	public TransformMover(IMoveInput moveInput, Transform transform, MoveParameters moveSettings)
	{
		_moveInput = moveInput;
		_transform = transform;
		_moveSettings = moveSettings;
	}

	public void Tick()
	{
		_transform.position += (Vector3)_moveInput.Direction * _moveInput.Thrust * _moveSettings.MoveSpeed * Time.deltaTime;
	}
}