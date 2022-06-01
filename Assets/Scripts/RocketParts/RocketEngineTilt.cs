using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketEngineTilt : MonoBehaviour, ITiltableParameters
{

	public float MaxAngle => maxAngle;
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
		set => desiredPosition = Vector2.ClampMagnitude(value, maxAngle);
	}

	[SerializeField]
	MonoBehaviour tiltController;
	ITiltController TiltController => (ITiltController)tiltController;

	[SerializeField]
	[ReadOnlyField]
	Vector2 currentPosition;
	[Space]
	[SerializeField]
	float maxAngle = 50;
	[SerializeField]
	float tangentSpeed = 50;

	Vector2 desiredPosition;

	const float qEpsilon = 1E-3f;

	// Update is called once per frame
	void Update()
    {
		DesiredPosition = TiltController?.ControlSignal ?? Vector2.zero;
		MoveThrustVector();
    }

	void MoveThrustVector()
	{
		currentPosition = CurrentPosition;
		Vector2 deltaPos = desiredPosition - currentPosition;
		Vector2 move =
			deltaPos.normalized *
			tangentSpeed * Time.deltaTime;
		CurrentPosition = 
			move.sqrMagnitude < deltaPos.sqrMagnitude ?
			currentPosition + move : desiredPosition;
	}

	private void OnValidate()
	{
		tiltController = 
			InterfaceValidationHelper.
			GetInterfaceAsMonoBehaviour<ITiltController>(tiltController);
	}
}

public static class InterfaceValidationHelper
{
	public static InterfaceT 
		GetInterface<InterfaceT>(MonoBehaviour monoBehaviour) 
		where InterfaceT : class
	{
		return monoBehaviour as InterfaceT ??
			monoBehaviour?.GetComponent<InterfaceT>();
	}

	public static MonoBehaviour
		GetInterfaceAsMonoBehaviour<InterfaceT>(MonoBehaviour monoBehaviour) 
		where InterfaceT : class =>
		GetInterface<InterfaceT>(monoBehaviour) as MonoBehaviour;
}