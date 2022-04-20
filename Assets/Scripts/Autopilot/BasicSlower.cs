using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//basic autopilot that should slow down the rocket, expecting
//the ground to be at 0 y coordinate.
public class BasicSlower : MonoBehaviour
{
	[SerializeField]
	private float idealLandingSpeed = 0.5f;
	[SerializeField]
	private GameObject engineObject;

	private IRocketEngine engine;
	private float DistanceToGround => transform.position.y;
	private void Start()
	{
		engine = engineObject.GetComponent<IRocketEngine>();
	}

	private void Update()
	{
		if(DistanceToGround != 0)
		{

		}
	}
}
