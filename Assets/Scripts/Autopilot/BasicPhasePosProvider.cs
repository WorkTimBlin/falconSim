using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BasicPhasePosProvider : MonoBehaviour, IPhasePositionProvider
{

	public PhaseVector<Vector3> Position =>
		new PhaseVector<Vector3>()
		{
			position = transform.position,
			velocity = positionVelocity
		};
	public PhaseVector<Quaternion> Rotation =>
		new PhaseVector<Quaternion>()
		{
			position = transform.rotation,
			velocity = rotationVelocity
		};

	private Vector3 positionVelocity;
	[SerializeField]
	private Quaternion rotationVelocity;
	[SerializeField]
	private Vector3 angularVelosity;

	private Vector3 previousPosition;
	private Quaternion previousRotation;
	Rigidbody _rigidbody;

	// Start is called before the first frame update
	void Start()
	{
		_rigidbody = GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		rotationVelocity = transform.rotation * Quaternion.Inverse(previousRotation);
		previousRotation = transform.rotation;

		angularVelosity = _rigidbody.angularVelocity;
		Debug.DrawRay(transform.position, angularVelosity, Color.red);
	}
}
