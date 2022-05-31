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


	public Vector2 ControlSignal
	{
		get;
		private set;
	}



	// Update is called once per frame
	void Update()
	{
		UpdateTilt();
	}

	void UpdateTilt()
	{
		Vector2 ControlSignal = Vector2.zero;
		if (Input.GetKey(right))
			ControlSignal.x += EngineTilt.MaxAngle;
		if (Input.GetKey(left))
			ControlSignal.x -= EngineTilt.MaxAngle;
		if (Input.GetKey(front))
			ControlSignal.y -= EngineTilt.MaxAngle;
		if (Input.GetKey(back))
			ControlSignal.y += EngineTilt.MaxAngle;
	}
	private void OnValidate()
	{
		engineTilt = 
			InterfaceValidationHelper.
			GetInterfaceAsMonoBehaviour<ITiltableParameters>(engineTilt);
	}
}
