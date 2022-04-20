using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineControl : MonoBehaviour
{
	[SerializeField]
	GameObject engineObject;
	[SerializeField]
	GameObject engineTiltObject;
	[SerializeField]
	float propulsionChangeSpeed = 10;

	[Header("Tilt Keys")]
	[SerializeField]
	KeyCode left;
	[SerializeField]
	KeyCode right;
	[SerializeField]
	KeyCode front;
	[SerializeField]
	KeyCode back;

	[Header("Propulsion Keys")]
	[SerializeField]
	KeyCode higher;
	[SerializeField]
	KeyCode lower;

	IRocketEngine engine;
	ITiltable engineTilt;

	// Start is called before the first frame update
	void Start()
	{
		engine = engineObject.GetComponent<IRocketEngine>();
		engineTilt = engineTiltObject.GetComponent<ITiltable>();
		
	}

	// Update is called once per frame
	void Update()
	{
		UpdateTilt();
		UpdatePropulsion();
	}

	void UpdatePropulsion()
	{
		if (Input.GetKey(higher))
			engine.Consumption += propulsionChangeSpeed * Time.deltaTime;
		if (Input.GetKey(lower))
			engine.Consumption -= propulsionChangeSpeed * Time.deltaTime; 
	}

	

	void UpdateTilt()
	{
		Vector2 desiredPos = new Vector2();
		if (Input.GetKey(right))
			desiredPos.x += engineTilt.MaxAngle;
		if (Input.GetKey(left)) 
			desiredPos.x -= engineTilt.MaxAngle;
		if (Input.GetKey(front)) 
			desiredPos.y -= engineTilt.MaxAngle;
		if (Input.GetKey(back)) 
			desiredPos.y += engineTilt.MaxAngle;
		engineTilt.DesiredPosition = desiredPos;
	}
}