using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnginePropulsionControl : MonoBehaviour
{
	[SerializeField]
	MonoBehaviour engine;
	[SerializeField]
	float propulsionChangeSpeed = 20;

	[Header("Propulsion Keys")]
	[SerializeField]
	KeyCode higher;
	[SerializeField]
	KeyCode lower;
	[SerializeField]
	float fuelFeed = 20;


	IRocketEngineParameters Engine => (IRocketEngineParameters)engine;

	// Update is called once per frame
	void Update()
	{
		UpdatePropulsion();
	}

	void UpdatePropulsion()
	{
		Engine.Consumption = fuelFeed;
		if (Input.GetKey(higher))
			Engine.Consumption += propulsionChangeSpeed * Time.deltaTime;
		if (Input.GetKey(lower))
			Engine.Consumption -= propulsionChangeSpeed * Time.deltaTime;
		fuelFeed = Engine.Consumption;
	}

	private void OnValidate()
	{
		engine = engine?.GetComponent<IRocketEngineParameters>() as MonoBehaviour;
	}
}