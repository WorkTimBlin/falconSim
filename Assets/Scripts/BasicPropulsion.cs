using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPropulsion : MonoBehaviour
{
	[SerializeField]
	List<HingeJoint> legs;

	[Space]
	[SerializeField]
	[Range(0, 100)]
	float force;

	new Rigidbody rigidbody;

	// Start is called before the first frame update
	void Start()
	{
		rigidbody = GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update()
	{
	}
	private void FixedUpdate()
	{
		AddPropulsionForce(Vector3.up * force);
		
	}
	void AddPropulsionForce(Vector3 force)
	{
		AddRelativeForceAtPosition(force, Vector3.down);
	}
	void AddRelativeForceAtPosition(Vector3 force, Vector3 position)
	{
		rigidbody.AddForceAtPosition(
			transform.TransformDirection(force), 
			transform.TransformPoint(position));
	}
}
