using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketEngine : MonoBehaviour, IRocketEngine
{
	public float Consumption
	{
		get => currentConsumtion;
		set => 
			currentConsumtion = Mathf.Clamp(value, minConsumption, maxConsumption);
	}
	public float MaxConsumption => maxConsumption;
	public float MaxPropulsion => maxPropultion;
	
	public float Propultion => currentConsumtion * maxPropultion / maxConsumption;

	
	[Space]
	[SerializeField]
	float currentConsumtion;
	
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
	void Update()
	{
		Propulse();
	}

	void Propulse()
	{
		rocketBody.AddForceAtPosition(
			transform.up * Propultion, 
			transform.position);
	}
}
