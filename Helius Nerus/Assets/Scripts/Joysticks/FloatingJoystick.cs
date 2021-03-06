﻿using UnityEngine;
using UnityEngine.EventSystems;

public class FloatingJoystick : InputCanvas<FloatingJoystick>, IDragHandler, IPointerDownHandler, IPointerUpHandler, IMoveInput
{
    [SerializeField] private RectTransform _buttonTransform = null;
    [SerializeField] private RectTransform _baseTransform = null;
    [SerializeField] private float _maxDistance = 5.0f;

    private Vector2 _direction;
    public Vector2 Direction
    {
        get;
        private set;
    }

    public float Thrust
    {
        get;
        private set;
    }

	protected override string InputType
	{
		get => "FloatingJoystick";
	}

	public void OnDrag(PointerEventData eventData)
    {
        SetButtonPosition(eventData.position);
    }

    public void ReadInput()
    {
        Direction = _direction.normalized;
        Thrust = _direction.magnitude / _maxDistance;
		Thrust *= 7f;
	}

	private void SetButtonPosition(Vector2 position)
    {
        _direction = position - _baseTransform.anchoredPosition;
        _direction = _direction.ClampInBorders(_maxDistance);
        _buttonTransform.anchoredPosition = _direction;
    }

	public void OnPointerDown(PointerEventData eventData)
	{
		_baseTransform.anchoredPosition = eventData.position;
	}

	public void OnPointerUp(PointerEventData eventData)
	{
        _buttonTransform.anchoredPosition = Vector2.zero;
		_direction = Vector2.zero;
	}

	public void Tick(Transform transform, float sens)
	{
		transform.Translate((Vector3)Direction * Thrust * TimeManager.PlayerDeltaTime * sens, Space.World);
	}
}
