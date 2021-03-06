﻿using UnityEngine;

public class VerticalMoveCommand : MoveCommand
{
	private float _timeElapsed = 0.0f;

    public VerticalMoveCommand()
    {

    }

    public override bool IsEnded()
	{
		return _timeElapsed > CommandData.EndParameter;
	}

	public override void Reset()
	{
		_timeElapsed = 0.0f;
	}

	public override void Tick(Transform ship)
	{
		_timeElapsed += TimeManager.EnemyDeltaTime * CommandData.TimeScale;
		ship.Translate(Vector3.down * CommandData.MovementMultiplier * TimeManager.EnemyDeltaTime
            * CommandData.TimeScale, Space.World);
	}
}
