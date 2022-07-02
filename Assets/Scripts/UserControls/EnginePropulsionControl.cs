using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnginePropulsionControl : MonoBehaviour
{
	[SerializeReference]
	InfiniteFuelTank fuelTank;
	[SerializeField]
	float propulsionChangeSpeed = 20;

	[Header("Propulsion Keys")]
	[SerializeField]
	KeyCode higher;
	[SerializeField]
	KeyCode lower;
	[SerializeField]
	float fuelFeed = 20;


	// Update is called once per frame
	void Update()
	{
		UpdatePropulsion();
	}

	void UpdatePropulsion()
	{
		if (Input.GetKey(higher))
			fuelTank.FuelFeed += propulsionChangeSpeed * Time.deltaTime;
		if (Input.GetKey(lower))
			fuelTank.FuelFeed -= propulsionChangeSpeed * Time.deltaTime;
		fuelFeed = fuelTank.FuelFeed;
	}
}