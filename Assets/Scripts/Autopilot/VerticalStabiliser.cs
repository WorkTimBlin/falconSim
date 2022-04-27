using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


/// <summary>
/// very simple basic stabiliser, should keep the rocket vertical.
/// </summary>
public class VerticalStabiliser : MonoBehaviour
{
	[SerializeField]
	private float idealLandingSpeed = 0.5f;
	[SerializeField]
	private GameObject engineObject;
	[SerializeField]
	private Rigidbody rocketRigidbody;
	[Header("controlPanel")]
	[SerializeField]
	float verticalDeviationCoeficient = 1;
	[SerializeField]
	float velocityCoeficient = 1;
	[SerializeField]
	[GraphicRepresentField(50)]
	Vector2 antiDeviationVector;
	[SerializeField]
	[GraphicRepresentField(50)]
	Vector2 antiVelocityVector;
	[SerializeField]
	Texture2D rect;

	private IRocketEngine engine;
	private ITiltable engineTilt;
	private float DistanceToGround => transform.position.y;
	private void Start()
	{
		engine = engineObject.GetComponent<IRocketEngine>();
		engineTilt = engineObject.GetComponent<ITiltable>();
	}

	private void Update()
	{
		Vector3 globalVertical = 
			transform.InverseTransformDirection(Vector3.up);
		Vector3 localAngularVelocity = 
			transform.TransformDirection(rocketRigidbody.angularVelocity);
		Vector2 localBottomVelocity =
			new Vector2(-localAngularVelocity.z, localAngularVelocity.x);

		antiDeviationVector = 
			new Vector2(globalVertical.x, -globalVertical.z) *
			engineTilt.MaxAngle;
		antiVelocityVector = 
			localBottomVelocity * 
			engineTilt.MaxAngle;

		engineTilt.DesiredPosition =
			antiDeviationVector * verticalDeviationCoeficient + 
			antiVelocityVector * velocityCoeficient;
		Debug.DrawRay(transform.position, globalVertical, Color.green);
	}
}

public class VerticalStabilizerEditor : Editor
{
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();


	}
}