using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketEngine : MonoBehaviour
{
	public float MaxAngle
	{
		get => maxAngle;
		private set => maxAngle = value;
	}
	// vector 2 with x, y within maxAngle-circle, representing the thrust vector
	public Vector2 CurrentPosition
	{
		get
		{
			Vector3 angles = transform.transform.localRotation.eulerAngles;
			return new Vector2(
				angles.z <= MaxAngle + qEpsilon ? angles.z : -360f + angles.z,
				angles.x <= MaxAngle + qEpsilon ? angles.x : -360f + angles.x);
		}
		set
		{
			if (value.sqrMagnitude > MaxAngle * MaxAngle) 
				value = value.normalized * MaxAngle;
			value.x = value.x >= 0 ? value.x : 360f + value.x;
			value.y = value.y >= 0 ? value.y : 360f + value.y;
			transform.localEulerAngles = new Vector3(value.y, 0, value.x);
		}
	}
	public Vector2 DesiredPosition
	{
		get => desiredPosition;
		set
		{
			if (value.sqrMagnitude > MaxAngle * MaxAngle)
				value = value.normalized * MaxAngle;
			desiredPosition = value;
		}
	}
	
	[SerializeField]
	float maxAngle = 50;
	[SerializeField]
	float tangentSpeed = 50;
	[SerializeField]
	Vector2 currentPosition;
	Vector2 desiredPosition;
	const float qEpsilon = 1E-3f;

	// Start is called before the first frame update
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		currentPosition = CurrentPosition;
		Vector2 deltaPos = desiredPosition - currentPosition;
		Vector2 move = 
			deltaPos.normalized * 
			tangentSpeed * Time.deltaTime;
		CurrentPosition = move.sqrMagnitude < deltaPos.sqrMagnitude ?
			currentPosition + move : desiredPosition;
	}
}
