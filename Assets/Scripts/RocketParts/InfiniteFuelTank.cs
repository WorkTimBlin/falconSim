using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteFuelTank : MonoBehaviour, IFuelProvider
{
	public float FuelFeed
	{
		get => fuelFeed;
		set => fuelFeed = value;
	} 
	[SerializeField] float fuelFeed = 19.62f;
	[SerializeField] bool isControlledByPlayer;

	public float ConsumeFuel(IFuelConsumer consumer)
	{
		return fuelFeed;
	}
}
