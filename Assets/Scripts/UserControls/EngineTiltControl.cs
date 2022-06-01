using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineTiltControl : MonoBehaviour, ITiltController
{

	[SerializeField]
	MonoBehaviour engineTilt;
	ITiltableParameters EngineTilt => (ITiltableParameters)engineTilt;

	[Header("Tilt Keys")]
	[SerializeField]
	KeyCode left;
	[SerializeField]
	KeyCode right;
	[SerializeField]
	KeyCode front;
	[SerializeField]
	KeyCode back;


	public Vector2 ControlSignal =>
		(Vector2.zero +
		(Input.GetKey(right) ? Vector2.right : Vector2.zero) +
		(Input.GetKey(left) ? Vector2.left : Vector2.zero) +
		(Input.GetKey(front) ? Vector2.down : Vector2.zero) +
		(Input.GetKey(back) ? Vector2.up : Vector2.zero)) *
		EngineTilt.MaxAngle;


	private void OnValidate()
	{
		engineTilt = 
			InterfaceValidationHelper.
			GetInterfaceAsMonoBehaviour<ITiltableParameters>(engineTilt);
	}
}