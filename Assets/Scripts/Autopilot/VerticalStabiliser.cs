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

	public Vector2 ControlSignal =>
		AntiDeviationVector * verticalDeviationCoeficient + 
		AntiVelocityVector * velocityCoeficient;

	Vector2 AntiDeviationVector
	{
		get
		{
			Vector3 globalVertical =
			   transform.InverseTransformDirection(Vector3.up);
			return antiDeviationVector =
				new Vector2(globalVertical.x, -globalVertical.z) *
				EngineTilt.MaxAngle;
		}
	}

	Vector2 AntiVelocityVector
	{
		get
		{
			Vector3 localAngularVelocity =
			transform.InverseTransformDirection(rocketRigidbody.angularVelocity);
			Vector2 localBottomVelocity =
			new Vector2(localAngularVelocity.z, localAngularVelocity.x);
			return antiVelocityVector = 
				Vector2.ClampMagnitude(
				localBottomVelocity *
				EngineTilt.MaxAngle, 50);
		}
	}

	private void OnValidate()
	{
		rocketEngine = 
			InterfaceValidationHelper.
			GetInterfaceAsMonoBehaviour
			<IRocketEngineParameters>(rocketEngine);
		engineTilt = 
			InterfaceValidationHelper.
			GetInterfaceAsMonoBehaviour
			<ITiltableParameters>(engineTilt);
	}
}