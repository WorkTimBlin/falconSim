using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketEngine : MonoBehaviour, IRocketEngineParameters, IFuelConsumer
{

	[SerializeReference]
	MonoBehaviour fuelProvider;

	IFuelProvider FuelProvider => fuelProvider as IFuelProvider;

	public float Consumption
	{
		get => currentConsumption;
		set => 
			currentConsumption = Mathf.Clamp(value, minConsumption, maxConsumption);
	}
	public float MaxConsumption => maxConsumption;
	public float MaxPropulsion => maxPropultion;
	
	public float Propultion => Consumption * maxPropultion / maxConsumption;

	public float FuelConsumedThisFrame => Consumption;

	[Space]
	[SerializeField]
	float currentConsumption;
	
	[SerializeField]
	float maxPropultion = 20;
	[SerializeField]
	float minConsumption = 0;
	[SerializeField]
	float maxConsumption = 50;


	Rigidbody rocketBody;

	

	// Start is called before the first frame update
	void Start()
	{
		rocketBody = 
			transform.parent.gameObject.GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		Consumption = FuelProvider.ConsumeFuel(this);
		Propulse();
	}

	void Propulse()
	{
		rocketBody.AddForceAtPosition(
			transform.up * Propultion, 
			transform.position);
	}

	private void OnValidate()
	{
		fuelProvider =
			InterfaceValidationHelper.
			GetInterfaceAsMonoBehaviour
			<IFuelProvider>(fuelProvider);
	}

}
