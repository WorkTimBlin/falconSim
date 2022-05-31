using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


/// <summary>
/// very simple basic stabiliser, should keep the rocket vertical.
/// </summary>
public class VerticalStabiliser : MonoBehaviour, ITiltController
{
	[SerializeField]
	private float idealLandingSpeed = 0.5f;
	[SerializeField]
	private MonoBehaviour rocketEngine;
	[SerializeField]
	private MonoBehaviour engineTilt;
	[SerializeField]
	private Rigidbody rocketRigidbody;

	[Header("controlPanel")]
	[SerializeField]
	float verticalDeviationCoeficient = 1;
	[SerializeField]
	[GraphicRepresentField(50)]
	Vector2 antiDeviationVector;
	[SerializeField]
	float velocityCoeficient = 1;
	[SerializeField]
	[GraphicRepresentField(50)]
	Vector2 antiVelocityVector;

	private IRocketEngineParameters Engine => (IRocketEngineParameters)rocketEngine;
	private ITiltableParameters EngineTilt => (ITiltableParameters)engineTilt;
	private float DistanceToGround => transform.position.y;

	public Vector2 ControlSignal { get; set; }

	private void Update()
	{
		Vector3 globalVertical = 
			transform.InverseTransformDirection(Vector3.up);
		Vector3 localAngularVelocity =
			transform.InverseTransformDirection(rocketRigidbody.angularVelocity);
		
		Vector2 localBottomVelocity =
			new Vector2(localAngularVelocity.z, localAngularVelocity.x);
		antiDeviationVector = 
			new Vector2(globalVertical.x, -globalVertical.z) *
			EngineTilt.MaxAngle;
		antiVelocityVector = Vector2.ClampMagnitude(
			localBottomVelocity * 
			EngineTilt.MaxAngle, 50);

		ControlSignal =
			antiDeviationVector * verticalDeviationCoeficient + 
			antiVelocityVector * velocityCoeficient;
	}

	private void OnValidate()
	{
		rocketEngine = rocketEngine?.GetComponent<IRocketEngineParameters>() as MonoBehaviour;
		engineTilt = engineTilt?.GetComponent<ITiltableParameters>() as MonoBehaviour;
	}
}